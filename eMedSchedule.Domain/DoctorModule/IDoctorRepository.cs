namespace eMedSchedule.Domain.DoctorModule
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        List<Doctor> RetrieveMany(List<Guid> doctorsIds);
    }
}