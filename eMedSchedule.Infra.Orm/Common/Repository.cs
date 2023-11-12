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
            await DbContext.AddAsync(objectToAdd);
        }

        public void Update(T objectToUpdate)
        {
            DbContext.Update(objectToUpdate);
        }

        public void Delete(T objectToDelete)
        {
            DbContext.Remove(objectToDelete);
        }

        public async Task<List<T>> RetrieveAllAsync()
        {
            return await Data.ToListAsync();
        }

        public async Task<T> RetrieveByIDAsync(Guid id)
        {
            return await Data.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}