namespace eMedSchedule.Domain.DoctorModule
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<Doctor>> RetrieveManyAsync(List<Guid> doctorsIds);
    }
}