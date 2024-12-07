namespace Ordering.Application.Dtos
{
 
    public class AddressDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public string DetailAddress { get; set; } = string.Empty;

        public AddressDto() { }

    }
}
