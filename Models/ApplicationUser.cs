using Microsoft.AspNetCore.Identity;

namespace AspNetOAuthLogin.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom properties if needed
        public string? Role { get; set; }
    }
}