using Microsoft.AspNetCore.Identity;

namespace CustomerFeedback.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } = null!;
    }
}
