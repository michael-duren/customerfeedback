namespace CustomerFeedback.Models.DTOs;

public class AppUserDisplayDto
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public IList<string> Roles { get; set; } = null!;
}