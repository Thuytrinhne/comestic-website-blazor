using Microsoft.AspNetCore.Components.Forms;

namespace FurnitureStore.Client.Requests
{
    public class UpdateUserRequest
    {
        public string? Name { get; set; }
        public DateTime? DOB { get; set; } = null;
        public int? Gender { get; set; } = null;
    }
}
