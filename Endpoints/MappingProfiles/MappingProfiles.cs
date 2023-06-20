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
            CreateMap<Models.Feedback, FeedbackDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
