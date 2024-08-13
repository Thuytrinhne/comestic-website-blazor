using FurnitureStore.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.Responses
{
    public class UserAddressResponse
    {
            [JsonProperty("listAddress")]
            public List<UserAddressDTO> UserAddresses { get; set; }
    }
}
