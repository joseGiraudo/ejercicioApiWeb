using ClassLibrary.DTOs;
using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.models;
using ClassLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.Interfaz
{
    public interface IUserService
    {
        public Task<ApiResponse<List<User>>> GetAllUsers();

        public Task<ApiResponse<User>> GetById(int id);

        public Task<ApiResponse<User>> Create(UserPostDTO userDTO);

        public Task<ApiResponse<User>> Update(int id, UserPostDTO userDTO);

        public Task<ApiResponse<string>> DeleteById(int id);

        public Task<ApiResponse<User>> GetByEmail(string email);

        public Task<ApiResponse<LoginResponseDTO>> Login(LoginDTO loginDTO);
    }
}
