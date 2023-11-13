using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.WebApi.ViewModels.DoctorActivityModule;

namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public class DoctorActivityProfile : Profile
    {
        public DoctorActivityProfile()
        {
            CreateMap<FormsDoctorActivityViewModel, DoctorActivity>();
            CreateMap<DoctorActivity, ListDoctorActivityViewModel>();

            CreateMap<DoctorActivity, FormsDoctorActivityViewModel>()
                .ForMember(destination => destination.SelectedDoctors,
                opt => opt.MapFrom(origin => origin.Doctors.Select(x => x.Id)));

            CreateMap<DoctorActivity, CompleteDoctorActivityViewModel>();
        }
    }

    public class ConfigureDoctorMappingAction : IMappingAction<FormsDoctorActivityViewModel, DoctorActivity>
    {
        private readonly IDoctorRepository _doctorRepository;

        public ConfigureDoctorMappingAction(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async void Process(FormsDoctorActivityViewModel source, DoctorActivity destination, ResolutionContext context)
        {
            destination.Doctors = await _doctorRepository.RetrieveManyAsync(source.SelectedDoctors);
        }
    }
}