namespace CustomerFeedback.Models.DTOs
{
    public class AppUserDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
