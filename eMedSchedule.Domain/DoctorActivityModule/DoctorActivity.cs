using eMedSchedule.Domain.DoctorModule;
using System.ComponentModel;

namespace eMedSchedule.Domain.DoctorActivityModule
{
    public class DoctorActivity : Entity
    {
        public string Title { get; set; }
        public List<Doctor> Doctors { get; set; }
        public ActivityType ActivityType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan RecoveryTime => ActivityType == ActivityType.Appointment ? TimeSpan.FromMinutes(20) : TimeSpan.FromHours(4);

        public DoctorActivity()
        {
        }

        public DoctorActivity(string title, List<Doctor> doctors, ActivityType activityType, DateTime startTime, DateTime endTime)
        {
            Title = title;
            Doctors = doctors;
            ActivityType = activityType;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override bool Equals(object? obj)
        {
            return obj is DoctorActivity activity &&
                   Title == activity.Title &&
                   EqualityComparer<List<Doctor>>.Default.Equals(Doctors, activity.Doctors) &&
                   ActivityType == activity.ActivityType &&
                   StartTime == activity.StartTime &&
                   EndTime == activity.EndTime &&
                   RecoveryTime.Equals(activity.RecoveryTime);
        }
    }

    public enum ActivityType
    {
        [Description("Appointment")]
        Appointment,

        [Description("Surgery")]
        Surgery
    }
}