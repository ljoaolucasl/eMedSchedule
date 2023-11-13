using eMedSchedule.Domain.DoctorModule;
using System.ComponentModel;

namespace eMedSchedule.Domain.DoctorActivityModule
{
    public class DoctorActivity : Entity
    {
        public string Title { get; set; }
        public List<Doctor> Doctors { get; set; }
        public ActivityTypeEnum ActivityType { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public TimeSpan RecoveryTime => ActivityType == ActivityTypeEnum.Appointment ?
            TimeSpan.FromMinutes(20) : TimeSpan.FromHours(4);

        public DoctorActivity()
        {
        }

        public DoctorActivity(string title, List<Doctor> doctors, ActivityTypeEnum activityType,
            DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            Title = title;
            Doctors = doctors;
            ActivityType = activityType;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override bool Equals(object? obj)
        {
            return obj is DoctorActivity activity &&
                   Id.Equals(activity.Id) &&
                   Title == activity.Title &&
                   EqualityComparer<List<Doctor>>.Default.Equals(Doctors, activity.Doctors) &&
                   ActivityType == activity.ActivityType &&
                   Date == activity.Date &&
                   StartTime.Equals(activity.StartTime) &&
                   EndTime.Equals(activity.EndTime) &&
                   RecoveryTime.Equals(activity.RecoveryTime);
        }
    }

    public enum ActivityTypeEnum
    {
        [Description("Appointment")]
        Appointment,

        [Description("Surgery")]
        Surgery
    }
}