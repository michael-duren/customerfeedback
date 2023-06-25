namespace CustomerFeedback.Models.DTOs
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime DateReviewed { get; set; }

        public string UserName { get; set; } = null!;
    }
}
