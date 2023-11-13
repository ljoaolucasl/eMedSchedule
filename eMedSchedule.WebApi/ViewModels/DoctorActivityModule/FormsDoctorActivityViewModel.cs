using eMedSchedule.Domain.DoctorActivityModule;

namespace eMedSchedule.WebApi.ViewModels.DoctorActivityModule
{
    public class FormsDoctorActivityViewModel
    {
        public string Title { get; set; }
        public List<Guid> SelectedDoctors { get; set; }
        public ActivityTypeEnum ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public TimeSpan RecoveryTime => ActivityType == ActivityTypeEnum.Appointment ?
            TimeSpan.FromMinutes(20) : TimeSpan.FromHours(4);

        public FormsDoctorActivityViewModel()
        {
        }

        public FormsDoctorActivityViewModel(string title, List<Guid> selectedDoctors, ActivityTypeEnum activityType,
            DateTime date, string startTime, string endTime)
        {
            Title = title;
            SelectedDoctors = selectedDoctors;
            ActivityType = activityType;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}