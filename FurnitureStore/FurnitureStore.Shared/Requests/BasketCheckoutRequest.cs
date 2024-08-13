using FurnitureStore.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.Requests
{
    public class BasketCheckoutRequest
    {
        public BasketCheckoutRequest(BasketCheckoutDTO basketCheckoutDTO)
        {
            BasketCheckoutDto = basketCheckoutDTO;
        }

        public BasketCheckoutDTO BasketCheckoutDto { get; set; }    
    }
}
