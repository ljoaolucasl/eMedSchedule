using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.WebApi.ViewModels.DoctorModule;

namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<FormsDoctorViewModel, Doctor>()
                .ForMember(destination => destination.ProfilePicture, opt => opt
                .MapFrom(origin => ConvertProfilePicture(origin.ProfilePictureBase64)))
                .ForMember(destination => destination.UserId, opt => opt
                .MapFrom<UserResolver>());

            CreateMap<Doctor, ListDoctorViewModel>()
                .ForMember(destination => destination.ProfilePictureBase64, opt => opt.MapFrom(origin => ConvertProfilePicture(origin.ProfilePicture)));

            CreateMap<Doctor, FormsDoctorViewModel>()
                .ForMember(destination => destination.ProfilePictureBase64, opt => opt.MapFrom(origin => ConvertProfilePicture(origin.ProfilePicture)));

            CreateMap<Doctor, CompleteDoctorViewModel>()
                .ForMember(destination => destination.ProfilePictureBase64, opt => opt.MapFrom(origin => ConvertProfilePicture(origin.ProfilePicture)));

            CreateMap<Doctor, ListWorkedHoursDoctorViewModel>()
                .ForMember(destination => destination.ProfilePictureBase64, opt => opt.MapFrom(origin => ConvertProfilePicture(origin.ProfilePicture)))
                .ForMember(destination => destination.WorkedHours, opt => opt.MapFrom(origin => origin.WorkedHours.ToString(@"hh\:mm")));
        }

        public byte[]? ConvertProfilePicture(string base64String)
        {
            return !string.IsNullOrEmpty(base64String) ? Convert.FromBase64String(base64String) : null;
        }

        public string? ConvertProfilePicture(byte[] byteArray)
        {
            return byteArray != null ? Convert.ToBase64String(byteArray) : null;
        }
    }
}