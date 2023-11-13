namespace eMedSchedule.Domain.Common
{
    public interface IRepository<T>
        where T : Entity
    {
        Task<List<T>> RetrieveAllAsync();

        Task<T> RetrieveByIDAsync(Guid id);

        Task AddAsync(T objectToAdd);

        void Update(T objectToUpdate);

        void Delete(T objectToDelete);

        void AddTest(T objectToAdd);
    }
}