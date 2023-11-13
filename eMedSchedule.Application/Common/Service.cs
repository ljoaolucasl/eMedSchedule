using eMedSchedule.Domain.Common;
using eMedSchedule.Domain.DoctorModule;
using FluentValidation;
using Serilog;

namespace eMedSchedule.Application.Common
{
    public abstract class Service<TEntity, TValidator> : IService<TEntity> where TValidator : AbstractValidator<TEntity>, new()
    {
        public abstract Task<Result<TEntity>> AddAsync(TEntity objectToAdd);

        public abstract Task<Result> DeleteAsync(TEntity objectToDelete);

        public abstract Task<Result<List<TEntity>>> RetrieveAllAsync();

        public abstract Task<Result<TEntity>> RetrieveByIDAsync(Guid id);

        public abstract Task<Result<TEntity>> UpdateAsync(TEntity objectToUpdate);

        protected virtual Result Validar(TEntity obj)
        {
            var validator = new TValidator();

            var resultValidation = validator.Validate(obj);

            var errors = new List<Error>();

            foreach (var validationFailure in resultValidation.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                errors.Add(new Error(validationFailure.ErrorMessage));
            }

            if (errors.Any())
                return Result.Fail(errors);

            return Result.Ok();
        }
    }
}