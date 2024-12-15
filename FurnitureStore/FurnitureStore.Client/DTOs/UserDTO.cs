using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.DTOs
{
    public  class UserDTO
    {
            public Guid Id { get; set; }
            public string Email { get; set; } = "";
            public string Name { get; set; } = "";
            public string? ImageUrl { get; set; }
            public int? Gender { get; set; } = null;
            public DateTime? DOB { get; set; } = null;
            public List<string> Roles { get; set; } = new();

            public ClaimsPrincipal ToClaimsPrincipal()=>
    
                new(new ClaimsIdentity(new Claim[]
            {
                new (JwtRegisteredClaimNames.Sub, Id.ToString()) ,
                new (JwtRegisteredClaimNames.Email, Email),
                new (JwtRegisteredClaimNames.Name, Name)
            }.Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()),
            "Blazor School"));
            public static UserDTO FromClaimsPrincipal(ClaimsPrincipal principal) => new()
            {
                Id= new  Guid (principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? "" ),
                Name  = principal.FindFirst(JwtRegisteredClaimNames.Name)?.Value ?? "",
                Email = principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? "",
                Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
            };
      }
    
}
