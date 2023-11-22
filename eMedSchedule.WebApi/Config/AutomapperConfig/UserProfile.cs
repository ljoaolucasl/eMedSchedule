using eMedSchedule.Domain.AuthenticationModule;
using eMedSchedule.WebApi.ViewModels.AuthenticationModule;

namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserViewModel, User>();
        }
    }
}