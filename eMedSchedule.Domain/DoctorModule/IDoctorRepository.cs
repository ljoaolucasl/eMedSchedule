namespace eMedSchedule.Domain.DoctorModule
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        List<Doctor> GetListDoctorsMoreHoursWorked(DateTime startDate, DateTime endDate);

        List<Doctor> RetrieveMany(List<Guid> doctorsIds);

        bool Exist(Doctor objectToCheck);
    }
}