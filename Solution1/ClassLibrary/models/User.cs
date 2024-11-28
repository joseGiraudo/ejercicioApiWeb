using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.models
{
    public class User
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age {  get; set; }
        public bool Active { get; set; }

        public User() { }
        public User(string email, string password, string name, string lastName, int age)
        {
            Email = email;
            Password = password;
            Name = name;
            LastName = lastName;
            Age = age;
            Active = true;
        }
    }
}
