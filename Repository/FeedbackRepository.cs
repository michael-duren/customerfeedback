using AutoMapper;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CustomerFeedback.Repository;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly AppDbContext _context;

    public FeedbackRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
    }

    public async Task<ICollection<Feedback>> GetAllAsync()
    {
        return await _context.Feedbacks.ToListAsync();
    }

    public async Task<Feedback> GetSingleAsync(int id)
    {
        return await _context.Feedbacks.FindAsync(id)!;
    }

    public async Task CreateAsync(Feedback feedback)
    {
          await _context.Feedbacks.AddAsync(feedback);
    }

    public async Task UpdateAsync(Feedback feedback)
    {
         _context.Feedbacks.Update(feedback);
    }

    public async Task RemoveAsync(Feedback feedback)
    {
        _context.Feedbacks.Remove(feedback);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}