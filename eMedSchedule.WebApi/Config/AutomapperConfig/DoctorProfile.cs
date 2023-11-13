using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.WebApi.ViewModels.DoctorModule;

namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<FormsDoctorViewModel, Doctor>();
            CreateMap<Doctor, ListDoctorViewModel>();
            CreateMap<Doctor, FormsDoctorViewModel>();
            CreateMap<Doctor, CompleteDoctorViewModel>();
        }
    }
}