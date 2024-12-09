using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
