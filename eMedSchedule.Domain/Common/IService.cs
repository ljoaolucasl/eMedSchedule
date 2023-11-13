using eMedSchedule.Domain.DoctorModule;
using FluentResults;

namespace eMedSchedule.Domain.Common
{
    public interface IService<T>
    {
        Task<Result<List<T>>> RetrieveAllAsync();

        Task<Result<T>> RetrieveByIDAsync(Guid id);

        Task<Result<T>> AddAsync(T objectToAdd);

        Task<Result<T>> UpdateAsync(T objectToUpdate);

        Task<Result> DeleteAsync(T objectToDelete);
    }
}