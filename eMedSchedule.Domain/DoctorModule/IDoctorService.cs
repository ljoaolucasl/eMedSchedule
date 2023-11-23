using FluentResults;

namespace eMedSchedule.Domain.DoctorModule
{
    public interface IDoctorService : IService<Doctor>
    {
        Result<List<Doctor>> GetListDoctorsMoreHoursWorked(DateTime startDate, DateTime endDate);
    }
}