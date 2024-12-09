using ClassLibrary.DTOs;
using ClassLibrary.models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAOs.Interfaces
{
    public interface IUserDAO
    {
        public IEnumerable<User> GetAllUsers();

        public User GetById(int id);

        public int Create(User user);

        public int Update(User user);

        public int DeleteById(int id);

        public User GetByEmail(string email);

        public User Login(LoginDTO loginDTO);
    }
}
