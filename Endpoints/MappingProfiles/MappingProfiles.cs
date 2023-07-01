using AutoMapper;
using CustomerFeedback.Models.DTOs;

namespace CustomerFeedback.Endpoints.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Feedback Maps
            CreateMap<Models.Feedback, Models.Feedback>();
            CreateMap<Models.Feedback, FeedbackDto>();
            CreateMap<Models.Feedback, UserFeedbackDto>();
        }
    }
}