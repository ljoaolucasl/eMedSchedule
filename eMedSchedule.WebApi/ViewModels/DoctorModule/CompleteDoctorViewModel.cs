using eMedSchedule.WebApi.ViewModels.DoctorActivityModule;

namespace eMedSchedule.WebApi.ViewModels.DoctorModule
{
    public class CompleteDoctorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CRM { get; set; }
        public string? ProfilePicture { get; set; }
        public List<ListDoctorActivityViewModel> Activities { get; set; }

        public CompleteDoctorViewModel()
        {
        }

        public CompleteDoctorViewModel(Guid id, string name, string crm,
            string? profilePicture, List<ListDoctorActivityViewModel> activities)
        {
            Id = id;
            Name = name;
            CRM = crm;
            ProfilePicture = profilePicture;
            Activities = activities;
        }
    }
}