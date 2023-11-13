using Microsoft.EntityFrameworkCore;

namespace eMedSchedule.Infra.Orm.Common
{
    public abstract class Repository<T> : IRepository<T>
        where T : Entity
    {
        public EMedScheduleContext DbContext { get; }

        public DbSet<T> Data { get; }

        public Repository()
        {
        }

        public Repository(EMedScheduleContext contextDb)
        {
            DbContext = contextDb;
            Data = contextDb.Set<T>();
        }

        public async Task AddAsync(T objectToAdd)
        {
            await Data.AddAsync(objectToAdd);
        }

        public void Update(T objectToUpdate)
        {
            Data.Update(objectToUpdate);
        }

        public void Delete(T objectToDelete)
        {
            Data.Remove(objectToDelete);
        }

        public virtual async Task<List<T>> RetrieveAllAsync()
        {
            return await Data.ToListAsync();
        }

        public virtual async Task<T> RetrieveByIDAsync(Guid id)
        {
            return await Data.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void AddTest(T objectToAdd)
        {
            Data.Add(objectToAdd);
        }
    }
}