using eMedSchedule.Domain.DoctorModule;
using System.ComponentModel;

namespace eMedSchedule.Domain.DoctorActivityModule
{
    public class DoctorActivity
    {
        public ActivityType ActivityType { get; set; }
        public List<Doctor> Doctors { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan RecoveryTime { get; set; }

        public DoctorActivity()
        {
        }

        public DoctorActivity(ActivityType activityType, List<Doctor> doctors, DateTime startTime, DateTime endTime, TimeSpan recoveryTime)
        {
            ActivityType = activityType;
            Doctors = doctors;
            StartTime = startTime;
            EndTime = endTime;
            RecoveryTime = recoveryTime;
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