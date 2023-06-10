namespace CustomerFeedback.Models.DTOs
{
    public class AppUserDto
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
