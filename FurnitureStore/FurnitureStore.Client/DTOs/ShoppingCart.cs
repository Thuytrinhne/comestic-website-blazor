using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.DTOs
{
    public  class ShoppingCart
    {
        public Guid UserId { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
}
