using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Infra.Orm.Common;
using Serilog;

namespace eMedSchedule.Application.Services
{
    public class DoctorActivityService : IDoctorActivityService
    {
        private readonly IPersistenceContext _persistenceContext;
        private readonly IDoctorActivityRepository _doctorActivityRespository;
        private readonly IDoctorActivityValidator _doctorActivityValidator;

        public DoctorActivityService(IPersistenceContext persistenceContext, IDoctorActivityRepository doctorActivityRespository, IDoctorActivityValidator doctorActivityValidator)
        {
            _persistenceContext = persistenceContext;
            _doctorActivityRespository = doctorActivityRespository;
            _doctorActivityValidator = doctorActivityValidator;
        }

        public async Task<Result<DoctorActivity>> AddAsync(DoctorActivity doctorActivityToAdd)
        {
            Result result = ValidateService(doctorActivityToAdd);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            await _doctorActivityRespository.AddAsync(doctorActivityToAdd);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorActivityToAdd);
        }

        public async Task<Result<DoctorActivity>> UpdateAsync(DoctorActivity doctorActivityToUpdate)
        {
            var result = ValidateService(doctorActivityToUpdate);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            _doctorActivityRespository.Update(doctorActivityToUpdate);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorActivityToUpdate);
        }

        public async Task<Result> DeleteAsync(DoctorActivity doctorActivityToDelete)
        {
            _doctorActivityRespository.Delete(doctorActivityToDelete);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok();
        }

        public async Task<Result<List<DoctorActivity>>> RetrieveAllAsync()
        {
            var doctors = await _doctorActivityRespository.RetrieveAllAsync();

            return Result.Ok(doctors);
        }

        public async Task<Result<DoctorActivity>> RetrieveByIDAsync(Guid id)
        {
            var doctor = await _doctorActivityRespository.RetrieveByIDAsync(id);

            if (doctor == null)
            {
                Log.Logger.Warning("Doctor Activity {DoctorId} not found", id);

                return Result.Fail($"Doctor Activity {id} not found");
            }

            return Result.Ok(doctor);
        }

        public Result ValidateService(DoctorActivity obj)
        {
            var resultValidation = _doctorActivityValidator.Validate(obj);

            var errors = new List<Error>();

            if (obj.Doctors != null && obj.Doctors.Count > 0)
                obj.Doctors[0].ValidateDoctorSchedule(obj, errors);

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