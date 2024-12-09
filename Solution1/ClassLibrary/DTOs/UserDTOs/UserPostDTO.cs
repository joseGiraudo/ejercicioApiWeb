using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTOs.UserDTOs
{
    public class UserPostDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public UserPostDTO() { }
        public UserPostDTO(string email, string password, string name, string lastName, int age)
        {
            Email = email;
            Password = password;
            Name = name;
            LastName = lastName;
            Age = age;
        }

    }
}
