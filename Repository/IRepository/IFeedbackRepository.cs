using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerFeedback.Repository.IRepository;

public interface IFeedbackRepository
{
     Task<ICollection<FeedbackDto>> GetAllAsync();
     Task<FeedbackDto?> GetSingleAsync(int id);
     Task CreateAsync(Feedback feedback);
     Task UpdateAsync(Feedback feedback);
     Task RemoveAsync(Feedback feedback);
     Task SaveAsync();
}