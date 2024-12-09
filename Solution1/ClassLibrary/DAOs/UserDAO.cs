using ClassLibrary.DTOs;
using ClassLibrary.models;
using ClassLibrary.DAOs.Interfaces;
using Dapper;
using MySqlConnector;

namespace ClassLibrary.DAOs
{

    public class UserDAO : IUserDAO 
    {
        private string connectionString = "Server=127.0.0.1;Port=3307;Database=apiExtradosDB;User Id=root;Password=root1234;";

        
        public UserDAO() { }
        


        public IEnumerable<User> GetAllUsers()
        {
            string query = "select id, email, name, lastName, age from users where active=true";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var users = connection.Query<User>(query);

                if (users == null)
                {
                    throw new Exception("No se encontraron usuarios");
                }

                return users;
            }
        }

        public User GetById(int id)
        {
            string query = "select id, email, name, lastName, age, active from users where id = @id and active = true";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var user = connection.QueryFirstOrDefault<User>(query, new { id });

                if (user == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                return new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    LastName = user.LastName,
                    Age = user.Age,
                    Active = user.Active,
                };
            }

        }

        public int Create(User user)
        {

            string query = "insert into users (email, hashedPassword, name, lastName, age) values (@email, @password, @name, @lastName, @age)";

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

            string query = "update users set name = @name, lastName = @lastName, age = @age " +
                "where id = @id";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(query, new
                {
                    name = user.Name,
                    lastName = user.LastName,
                    age = user.Age,
                    id = user.Id
                });
                return affectedRows;
            }
        }

        public int DeleteById(int id)
        {
            string query = "update users set active = @active where id = @id";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Execute(query, new { id = id, active = false });
            }
        }

        public User GetByEmail(string email)
        {
            string query = "select id, email, hashedPassword, name, lastName, age from users " +
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
                    Password = user.hashedPassword,
                    Name = user.name,
                    LastName = user.lastName,
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
