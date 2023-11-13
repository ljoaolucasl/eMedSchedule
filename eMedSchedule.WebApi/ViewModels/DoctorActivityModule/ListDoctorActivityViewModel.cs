using eMedSchedule.Domain.DoctorActivityModule;

namespace eMedSchedule.WebApi.ViewModels.DoctorActivityModule
{
    public class ListDoctorActivityViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ActivityTypeEnum ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public ListDoctorActivityViewModel()
        {
        }

        public ListDoctorActivityViewModel(Guid id, string title, ActivityTypeEnum activityType,
            DateTime date, string startTime, string endTime)
        {
            Id = id;
            Title = title;
            ActivityType = activityType;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}