namespace eMedSchedule.WebApi.ViewModels.DoctorModule
{
    public class ListDoctorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[]? ProfilePicture { get; set; }

        public ListDoctorViewModel()
        {
        }

        public ListDoctorViewModel(Guid id, string name, byte[]? profilePicture)
        {
            Id = id;
            Name = name;
            ProfilePicture = profilePicture;
        }
    }
}