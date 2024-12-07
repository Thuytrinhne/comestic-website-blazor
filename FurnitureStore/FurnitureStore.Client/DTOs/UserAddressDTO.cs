using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.DTOs
{
    public class UserAddressDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Default { get; set; } = 0;
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string DetailAddress { get; set; }
    }
}
