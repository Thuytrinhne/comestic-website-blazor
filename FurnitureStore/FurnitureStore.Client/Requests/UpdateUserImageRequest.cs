using Microsoft.AspNetCore.Components.Forms;

namespace FurnitureStore.Client.Requests
{
    public class UpdateUserImageRequest
    {
        public IBrowserFile? Image { get; set; }
    }
}
