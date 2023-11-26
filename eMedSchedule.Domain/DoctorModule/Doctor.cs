using eMedSchedule.Domain.DoctorActivityModule;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMedSchedule.Domain.DoctorModule
{
    public class Doctor : Entity
    {
        public string Name { get; set; }
        public string CRM { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public List<DoctorActivity> Activities { get; set; }

        [NotMapped]
        public TimeSpan WorkedHours { get; set; }

        public Doctor()
        {
        }

        public Doctor(string name, string crm, byte[]? profilePicture)
        {
            Name = name;
            CRM = crm;
            ProfilePicture = profilePicture;
            Activities = new List<DoctorActivity>();
            WorkedHours = TimeSpan.Zero;
        }

        public bool ValidateDoctorPendingActivity(Doctor doctorToValidate)
        {
            if (Activities == null)
                return true;

            foreach (var existingActivity in Activities)
            {
                var existingActivityEnd = existingActivity.Date.Date + existingActivity.EndTime + existingActivity.RecoveryTime;

                if ((existingActivity.EndTime + existingActivity.RecoveryTime) < existingActivity.StartTime)
                {
                    existingActivityEnd = existingActivityEnd.AddDays(1);
                }

                if (existingActivityEnd > DateTime.Now)
                {
                    return false;
                }
            }

            return true;
        }

        public bool ValidateDoctorSchedule(DoctorActivity activityToValidate)
        {
            var newActivityStart = activityToValidate.Date.Date + activityToValidate.StartTime;
            var newActivityEnd = activityToValidate.Date.Date + activityToValidate.EndTime + activityToValidate.RecoveryTime;

            if ((activityToValidate.EndTime + activityToValidate.RecoveryTime) < activityToValidate.StartTime)
            {
                newActivityEnd = newActivityEnd.AddDays(1);
            }

            foreach (var existingActivity in Activities)
            {
                var existingActivityStart = existingActivity.Date.Date + existingActivity.StartTime;
                var existingActivityEnd = existingActivity.Date.Date + existingActivity.EndTime + existingActivity.RecoveryTime;

                if ((existingActivity.EndTime + existingActivity.RecoveryTime) < existingActivity.StartTime)
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

        public void CalculateWorkedHourDoctorsPeriod(DateTime startDate, DateTime endDate)
        {
            foreach (var activity in Activities)
            {
                var dateActivity = activity.Date.Date;

                var activityStart = activity.Date.Date + activity.StartTime;
                var activityEnd = activity.Date.Date + activity.EndTime;

                if (activityStart < DateTime.Now && (dateActivity >= startDate.Date && dateActivity <= endDate.Date))
                {
                    if (activity.EndTime < activity.StartTime)
                    {
                        activityEnd = activityEnd.AddDays(1);
                    }

                    WorkedHours += activityEnd - activityStart;
                }
            }
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