using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace CustomerFeedback.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
