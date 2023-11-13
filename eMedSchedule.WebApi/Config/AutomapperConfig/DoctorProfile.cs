using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.WebApi.ViewModels.DoctorModule;

namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<FormsDoctorViewModel, Doctor>()
                .ForMember(destination => destination.ProfilePicture, opt => opt.MapFrom(origin => Convert.FromBase64String(origin.ProfilePicture)));

            CreateMap<Doctor, ListDoctorViewModel>()
                .ForMember(destination => destination.ProfilePicture, opt => opt.MapFrom(origin => Convert.ToBase64String(origin.ProfilePicture)));

            CreateMap<Doctor, FormsDoctorViewModel>()
                .ForMember(destination => destination.ProfilePicture, opt => opt.MapFrom(origin => Convert.ToBase64String(origin.ProfilePicture)));

            CreateMap<Doctor, CompleteDoctorViewModel>()
                .ForMember(destination => destination.ProfilePicture, opt => opt.MapFrom(origin => Convert.ToBase64String(origin.ProfilePicture)));
        }
    }
}