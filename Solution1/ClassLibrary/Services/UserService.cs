using ClassLibrary.DAOs.Interfaces;
using ClassLibrary.DTOs;
using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.models;
using ClassLibrary.Response;
using ClassLibrary.Services.Interfaz;
using Configuracion;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class UserService : IUserService
    {

        private readonly IUserDAO _userDAO;

        private JwtConfiguration _jwtConfiguration;

        public UserService(IUserDAO userDAO, IOptions<JwtConfiguration> jwtConfiguration)
        {
            _userDAO = userDAO;
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public async Task<ApiResponse<User>> Create(UserPostDTO userDTO)
        {
            var response = new ApiResponse<User>();

            User user = new User(userDTO.Email, userDTO.Password, userDTO.Name, userDTO.LastName, userDTO.Age);

            var affRows = _userDAO.Create(user);

            if (affRows > 0)
            {
                response.Data = user;
                return response;
            }
            response.SetError("No se pudo crear el usuario ", System.Net.HttpStatusCode.InternalServerError);
            return response;


        }

        public async Task<ApiResponse<string>> DeleteById(int id)
        {
            var response = new ApiResponse<string>();

            try
            {
                _userDAO.DeleteById(id);
                response.Data = "Se elimino el usuario con id: " + id;
                return response;
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message, System.Net.HttpStatusCode.InternalServerError);
                return response;
            }
        }

        public async Task<ApiResponse<List<User>>> GetAllUsers()
        {
            var response = new ApiResponse<List<User>>();

            try
            {
                var users = _userDAO.GetAllUsers();

                response.Data = (List<User>)users;
                return response;
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message, System.Net.HttpStatusCode.NotFound);
                return response;
            }
        }

        public async Task<ApiResponse<User>> GetByEmail(string email)
        {
            var response = new ApiResponse<User>();

            try
            {
                var user = _userDAO.GetByEmail(email);

                response.Data = user;
                return response;
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message, System.Net.HttpStatusCode.NotFound);
                return response;
            }
        }

        public async Task<ApiResponse<User>> GetById(int id)
        {
            var response = new ApiResponse<User>();

            try
            {
                var user = _userDAO.GetById(id);

                response.Data = user;
                return response;
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message, System.Net.HttpStatusCode.NotFound);
                return response;
            }
        }

        public async Task<ApiResponse<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            var response = new ApiResponse<LoginResponseDTO>();
            try
            {
                var user = _userDAO.Login(loginDTO);

                if (user == null)
                {
                    response.SetError("Credenciales inválidas", System.Net.HttpStatusCode.BadRequest);
                }

                var token = GenerateToken(user);

                var responseData = new LoginResponseDTO();
                responseData.Token = token;
                responseData.User = user;

                response.Data = responseData;
                return response;
            }
            catch (Exception ex)
            {
                response.SetError("Error al loguearse. " + ex.Message, System.Net.HttpStatusCode.BadRequest);
                return response;
            }
        }

        public async Task<ApiResponse<User>> Update(int id, UserPostDTO userDTO)
        {
            var response = new ApiResponse<User>();

            User user = new User(userDTO.Email, userDTO.Password, userDTO.Name, userDTO.LastName, userDTO.Age);
            user.Id = id;

            var affRows = _userDAO.Update(user);

            if (affRows > 0)
            {
                response.Data = user;
                return response;
            }
            response.SetError("No se pudo actualizar el usuario", System.Net.HttpStatusCode.InternalServerError);
            return response;
        }

        private string GenerateToken(User user)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken
                (
                    _jwtConfiguration.Issuer,
                    _jwtConfiguration.Audience,
                    claims: claim,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
