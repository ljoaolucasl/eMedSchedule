using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.WebApi.ViewModels.DoctorModule;

namespace eMedSchedule.WebApi.ViewModels.DoctorActivityModule
{
    public class CompleteDoctorActivityViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<ListDoctorViewModel> Doctors { get; set; }
        public ActivityTypeEnum ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public TimeSpan RecoveryTime => ActivityType == ActivityTypeEnum.Appointment ?
            TimeSpan.FromMinutes(20) : TimeSpan.FromHours(4);

        public CompleteDoctorActivityViewModel()
        {
        }

        public CompleteDoctorActivityViewModel(Guid id, string title, List<ListDoctorViewModel> doctors,
            ActivityTypeEnum activityType, DateTime date, string startTime, string endTime)
        {
            Id = id;
            Title = title;
            Doctors = doctors;
            ActivityType = activityType;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}