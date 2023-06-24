using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerFeedback.Context;
using CustomerFeedback.Models;
using CustomerFeedback.Models.DTOs;
using CustomerFeedback.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerFeedback.Repository;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FeedbackRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<FeedbackDto>> GetAllAsync()
    {
        return await _context.Feedbacks.ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<FeedbackDto?> GetSingleAsync(int id)
    {
        return await _context.Feedbacks.ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id)!;
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
        _context.Feedbacks.Update(feedback);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}