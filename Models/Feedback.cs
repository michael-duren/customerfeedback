namespace CustomerFeedback.Models
{
	public class Feedback
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int Rating { get; set; }
		public DateTime DateReviewed { get; set; }

	}
}