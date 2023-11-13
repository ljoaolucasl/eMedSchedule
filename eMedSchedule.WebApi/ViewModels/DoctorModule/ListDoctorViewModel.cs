namespace eMedSchedule.WebApi.ViewModels.DoctorModule
{
    public class ListDoctorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }

        public ListDoctorViewModel()
        {
        }

        public ListDoctorViewModel(Guid id, string name, string? profilePicture)
        {
            Id = id;
            Name = name;
            ProfilePicture = profilePicture;
        }
    }
}