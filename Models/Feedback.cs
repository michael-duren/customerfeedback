using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerFeedback.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime DateReviewed { get; set; }
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; } = null!;
    }
}
