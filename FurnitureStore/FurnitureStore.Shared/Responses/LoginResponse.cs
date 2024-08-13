using FurnitureStore.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.Responses
{
    public class LoginResponse
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
