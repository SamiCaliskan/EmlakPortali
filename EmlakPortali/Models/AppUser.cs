using Microsoft.AspNetCore.Identity;

namespace EmlakPortali.Models
{
    public class AppUser:IdentityUser<int>
    {
        public string FullName { get; set; } = null!;

        public DateTime DateAdded { get; set; }
    }
}
