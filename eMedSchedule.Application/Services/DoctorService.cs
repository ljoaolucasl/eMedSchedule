using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using Serilog;

namespace eMedSchedule.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IPersistenceContext _persistenceContext;
        private readonly IDoctorRepository _doctorRespository;
        private readonly IDoctorValidator _doctorValidator;

        public DoctorService(IPersistenceContext persistenceContext, IDoctorRepository doctorRespository, IDoctorValidator doctorValidator)
        {
            _persistenceContext = persistenceContext;
            _doctorRespository = doctorRespository;
            _doctorValidator = doctorValidator;
        }

        public async Task<Result<Doctor>> AddAsync(Doctor doctorToAdd)
        {
            Result result = ValidateService(doctorToAdd);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            await _doctorRespository.AddAsync(doctorToAdd);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorToAdd);
        }

        public async Task<Result<Doctor>> UpdateAsync(Doctor doctorToUpdate)
        {
            var result = ValidateService(doctorToUpdate);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            _doctorRespository.Update(doctorToUpdate);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorToUpdate);
        }

        public async Task<Result> DeleteAsync(Doctor doctorToDelete)
        {
            if (doctorToDelete.ValidateDoctorPendingActivity(doctorToDelete))
            {
                _doctorRespository.Delete(doctorToDelete);

                await _persistenceContext.SaveDataAsync();

                return Result.Ok();
            }

            Log.Logger.Warning("Doctor {DoctorName} has pending activities", doctorToDelete.Name);

            return Result.Fail($"Doctor {doctorToDelete.Name} has pending activities");
        }

        public async Task<Result<List<Doctor>>> RetrieveAllAsync()
        {
            var doctors = await _doctorRespository.RetrieveAllAsync();

            return Result.Ok(doctors);
        }

        public async Task<Result<Doctor>> RetrieveByIDAsync(Guid id)
        {
            var doctor = await _doctorRespository.RetrieveByIDAsync(id);

            if (doctor == null)
            {
                Log.Logger.Warning("Doctor {DoctorId} not found", id);

                return Result.Fail($"Doctor {id} not found");
            }

            return Result.Ok(doctor);
        }

        public Result<List<Doctor>> GetListDoctorsMoreHoursWorked(DateTime startDate, DateTime endDate)
        {
            var doctors = _doctorRespository.GetListDoctorsMoreHoursWorked(startDate, endDate);

            return Result.Ok(doctors);
        }

        public Result ValidateService(Doctor obj)
        {
            var resultValidation = _doctorValidator.Validate(obj);

            var errors = new List<Error>();

            foreach (var validationFailure in resultValidation.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                errors.Add(new Error(validationFailure.ErrorMessage));
            }

            if (_doctorRespository.Exist(obj))
                errors.Add(new Error("This CRM is already registered"));

            if (errors.Any())
                return Result.Fail(errors);

            return Result.Ok();
        }
    }
}