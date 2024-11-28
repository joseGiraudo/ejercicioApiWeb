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

    public class UserDAO : IUserDAO
    {
        private static UserDAO _instance;

        private string connectionString = "Server=127.0.0.1;Port=3307;Database=apiExtradosDB;User Id=root;Password=root1234;";

        // constructor privado
        private UserDAO()
        {
            
        }

        public static UserDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserDAO();
                }
                return _instance;
            }
        }


        public IEnumerable<User> GetAllUsers()
        {
            string query = "select id, email, name, last_name, age from usuarios where active=true";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var users = connection.Query<User>(query);

                if (users == null)
                {
                    throw new Exception("No se encontraron usuarios");
                }
                // falta mapear los snake_case
                return users;
            }
        }

        public User GetById(int id)
        {
            string query = "select id, email, name, last_name, age, active from usuarios where id = @id and active = true";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var user = connection.QueryFirstOrDefault<dynamic>(query, new { id });

                if (user == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                return new User
                {
                    Id = user.id,
                    Email = user.email,
                    Name = user.name,
                    LastName = user.last_name,
                    Age = user.age,
                    Active = user.active,
                };
            }

        }

        public int Create(User user)
        {

            string query = "insert into usuarios (email, hashed_password, name, last_name, age) values (@email, @password, @name, @lastName, @age)";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(query, new { email = user.Email, password = hashedPassword,
                    name = user.Name, lastName = user.LastName, age = user.Age });
                return affectedRows;
            }
        }

        public int Update(User user)
        {

            string query = "update usuarios set name = @name, last_name = @last_name, age = @age" +
                "where id = @id";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(query, new
                {
                    name = user.Name,
                    last_name = user.LastName,
                    age = user.Age,
                    id = user.Id
                });
                return affectedRows;
            }
        }

        public int DeleteById(int id)
        {
            string query = "update usuarios set active = @active where id = @id";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Execute(query, new { id = id, active = false });
            }
        }

        public User GetByEmail(string email)
        {
            string query = "select id, email, hashed_password, name, last_name, age from usuarios " +
                "where email = @email and active = true";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var user = connection.QueryFirstOrDefault<dynamic>(query, new { email });
                if(user == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                return new User
                {
                    Id = user.id,
                    Email = user.email,
                    Password = user.hashed_password,
                    Name = user.name,
                    LastName = user.last_name,
                    Age = user.age
                };
            }
        }

        public User Login(LoginDTO loginDTO)
        {
            User user = GetByEmail(loginDTO.Email);
            if (user == null)
            {
                // aca deberia retornar un NotFound
                throw new Exception("Usuario no encontrado");
            }
            bool isMatch = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
            if (!isMatch)
            {
                // aca deberia retornar un Error de contraseña
                throw new Exception("Email o contraseña incorrectos");
            }
            return GetById(user.Id);
        }
    }
}
