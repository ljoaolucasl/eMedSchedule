using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.WebApi.ViewModels.DoctorActivityModule;

namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public class DoctorActivityProfile : Profile
    {
        public DoctorActivityProfile()
        {
            CreateMap<FormsDoctorActivityViewModel, DoctorActivity>()
                .AfterMap<ConfigureDoctorMappingAction>()
                .ForMember(destination => destination.UserId, opt => opt
                .MapFrom<UserResolver>());

            CreateMap<DoctorActivity, ListDoctorActivityViewModel>()
                .ForMember(destination => destination.StartTime, opt => opt.MapFrom(origin => origin.StartTime.ToString(@"hh\:mm")))
                .ForMember(destination => destination.EndTime, opt => opt.MapFrom(origin => origin.EndTime.ToString(@"hh\:mm")));

            CreateMap<DoctorActivity, FormsDoctorActivityViewModel>()
                .ForMember(destination => destination.SelectedDoctors,
                opt => opt.MapFrom(origin => origin.Doctors.Select(x => x.Id)));

            CreateMap<DoctorActivity, CompleteDoctorActivityViewModel>()
                .ForMember(destination => destination.StartTime, opt => opt.MapFrom(origin => origin.StartTime.ToString(@"hh\:mm")))
                .ForMember(destination => destination.EndTime, opt => opt.MapFrom(origin => origin.EndTime.ToString(@"hh\:mm")));
        }
    }

    public class ConfigureDoctorMappingAction : IMappingAction<FormsDoctorActivityViewModel, DoctorActivity>
    {
        private readonly IDoctorRepository _doctorRepository;

        public ConfigureDoctorMappingAction(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public void Process(FormsDoctorActivityViewModel source, DoctorActivity destination, ResolutionContext context)
        {
            destination.Doctors = _doctorRepository.RetrieveMany(source.SelectedDoctors);
        }
    }
}