namespace eMedSchedule.WebApi.ViewModels.DoctorModule
{
    public class FormsDoctorViewModel
    {
        public string Name { get; set; }
        public string CRM { get; set; }
        public byte[]? ProfilePicture { get; set; }

        public FormsDoctorViewModel()
        {
        }

        public FormsDoctorViewModel(string name, string crm, byte[]? profilePicture)
        {
            Name = name;
            CRM = crm;
            ProfilePicture = profilePicture;
        }
    }
}