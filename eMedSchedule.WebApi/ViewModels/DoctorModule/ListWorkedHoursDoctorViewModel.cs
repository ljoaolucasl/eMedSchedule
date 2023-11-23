namespace eMedSchedule.WebApi.ViewModels.DoctorModule
{
    public class ListWorkedHoursDoctorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ProfilePictureBase64 { get; set; }
        public string WorkedHours { get; set; }

        public ListWorkedHoursDoctorViewModel()
        {
        }

        public ListWorkedHoursDoctorViewModel(Guid id, string name, string? profilePictureBase64, string workedHours)
        {
            Id = id;
            Name = name;
            ProfilePictureBase64 = profilePictureBase64;
            WorkedHours = workedHours;
        }
    }
}