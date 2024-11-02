using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.DTOs
{
    public class RegisterDto
    {
        public UserDto User { get; set; }
        public RegisterDto() {
            User = new UserDto();
        }
    }
   
    public class UserDto
    {
        public string Email {  get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
        public int Gender { get; set; } = default!;

        public DateTime DOB { get; set; } = default!;
    }
}
