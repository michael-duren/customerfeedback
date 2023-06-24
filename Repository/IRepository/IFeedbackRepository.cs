using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerFeedback.Repository.IRepository;

public interface IFeedbackRepository
{
     Task<ICollection<Feedback>> GetAllAsync();
     Task<Feedback?> GetSingleAsync(int id);
     Task CreateAsync(Feedback feedback);
     Task UpdateAsync(Feedback feedback);
     Task RemoveAsync(Feedback feedback);
     Task SaveAsync();
}