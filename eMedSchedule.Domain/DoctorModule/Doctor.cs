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
            Activities = new List<DoctorActivity>();
        }

        public bool ValidateDoctorSchedule(DoctorActivity activityToValidate)
        {
            var newActivityStart = activityToValidate.Date + activityToValidate.StartTime;
            var newActivityEnd = activityToValidate.Date + activityToValidate.EndTime + activityToValidate.RecoveryTime;

            if ((activityToValidate.EndTime + activityToValidate.RecoveryTime) + activityToValidate.StartTime > TimeSpan.FromHours(24))
            {
                newActivityEnd = newActivityEnd.AddDays(1);
            }

            foreach (var existingActivity in Activities)
            {
                var existingActivityStart = existingActivity.Date + existingActivity.StartTime;
                var existingActivityEnd = existingActivity.Date + existingActivity.EndTime + existingActivity.RecoveryTime;

                if ((existingActivity.EndTime + existingActivity.RecoveryTime) + existingActivity.StartTime > TimeSpan.FromHours(24))
                {
                    existingActivityEnd = existingActivityEnd.AddDays(1);
                }

                if (!(existingActivityEnd <= newActivityStart || existingActivityStart >= newActivityEnd))
                {
                    return false;
                }
            }

            return true;
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