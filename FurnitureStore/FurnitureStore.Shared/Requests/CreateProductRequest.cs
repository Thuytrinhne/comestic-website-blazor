using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public IBrowserFile Image { get; set; }
        public decimal Price { get; set; }
        public string Tags { get; set; }
    }

}
