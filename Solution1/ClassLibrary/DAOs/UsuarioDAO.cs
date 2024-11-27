using ClassLibrary.DTOs;
using ClassLibrary.models;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary.DAOs
{

    public class UsuarioDAO
    {
        private static UsuarioDAO _instance;

        private string connectionString = "Server=127.0.0.1;Port=3307;Database=apiExtradosDB;User Id=root;Password=root1234;";

        // constructor privado
        private UsuarioDAO()
        {
            
        }

        public static UsuarioDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UsuarioDAO();
                }
                return _instance;
            }
        }


        public IEnumerable<Usuario> GetUsuarios()
        {
            string query = "select id, email, name, last_name, age from usuarios where active=true";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var usuarios = connection.Query<Usuario>(query);
                return usuarios;
            }
        }

        public Usuario GetById(int id)
        {
            string query = "select id, email, name, last_name, age from usuarios where id = @id and active = true";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var usuario = connection.QueryFirstOrDefault<Usuario>(query, new { id });
                return usuario;
            }

        }

        public int Create(Usuario usuario)
        {

            string query = "insert into usuarios (email, password, name, last_name, age) values (@email, @password, @name, @lastName, @age)";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(query, new { email = usuario.Email, password = hashedPassword,
                    name = usuario.Name, lastName = usuario.LastName, age = usuario.Age });
                return affectedRows;
            }
        }

        public int Update(Usuario usuario)
        {

            string query = "update usuarios set name = @name, last_name = @last_name, age = @age" +
                "where id = @id";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(query, new
                {
                    name = usuario.Name,
                    last_name = usuario.LastName,
                    age = usuario.Age,
                    id = usuario.Id
                });
                return affectedRows;
            }
        }

        public int DeleteById(Guid id)
        {
            string query = "update usuarios set activo = @active where id = @id";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Execute(query, new { id = id, active = false });
            }
        }

        public Usuario GetByEmail(string email)
        {
            string query = "select id, email, password, name, last_name, age from usuarios " +
                "where email = @email and active = true";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var usuario = connection.QueryFirstOrDefault<Usuario>(query, new { email });
                if(usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                return usuario;
            }
        }

        public Guid Login(LoginDTO loginDTO)
        {
            Usuario usuario = GetByEmail(loginDTO.Email);
            if (usuario == null)
            {
                // aca deberia retornar un NotFound
                throw new Exception("Usuario no encontrado");
            }
            bool isMatch = BCrypt.Net.BCrypt.Verify(loginDTO.Password, usuario.Password);
            if (!isMatch)
            {
                // aca deberia retornar un Error de contraseña
                throw new Exception("Email o contraseña incorrectos");
            }
            return usuario.Id;
        }
    }
}
