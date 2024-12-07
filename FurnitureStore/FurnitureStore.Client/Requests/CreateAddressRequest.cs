using FurnitureStore.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.Requests
{
    public class CreateAddressRequest
    {
        public CreateAddressRequest()
        {
        }

        public UserAddressDTO UserAddress {  get; set; }
    }
}
