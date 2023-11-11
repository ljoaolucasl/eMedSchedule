namespace eMedSchedule.Domain.Common
{
    public interface IService<T>
    {
        List<T> RetrieveAll();

        T RetrieveByID(int id);

        void Add(T objectToAdd);

        void Update(T objectToUpdate);

        void Delete(T objectToDelete);
    }
}