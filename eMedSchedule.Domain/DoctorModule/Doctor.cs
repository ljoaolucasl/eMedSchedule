using eMedSchedule.Domain.DoctorActivityModule;

namespace eMedSchedule.Domain.DoctorModule
{
    public class Doctor : Entity
    {
        public string Name { get; set; }
        public string CRM { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public List<DoctorActivity> Activities { get; set; }

        public Doctor()
        {
        }

        public Doctor(string name, string crm, byte[]? profilePicture)
        {
            Name = name;
            CRM = crm;
            ProfilePicture = profilePicture;
        }

        public override bool Equals(object? obj)
        {
            return obj is Doctor doctor &&
                   Id.Equals(doctor.Id) &&
                   Name == doctor.Name &&
                   CRM == doctor.CRM &&
                   EqualityComparer<byte[]?>.Default.Equals(ProfilePicture, doctor.ProfilePicture);
        }
    }
}