using eMedSchedule.Application.Common;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Infra.Orm.Common;
using Serilog;

namespace eMedSchedule.Application.Services
{
    public class DoctorActivityService : Service<DoctorActivity, DoctorActivityValidator>, IDoctorActivityService
    {
        private readonly IPersistenceContext _persistenceContext;
        private readonly IDoctorActivityRepository _doctorActivityRespository;

        public DoctorActivityService(IPersistenceContext persistenceContext, IDoctorActivityRepository doctorActivityRespository)
        {
            _persistenceContext = persistenceContext;
            _doctorActivityRespository = doctorActivityRespository;
        }

        public override async Task<Result<DoctorActivity>> AddAsync(DoctorActivity doctorActivityToAdd)
        {
            Result result = Validar(doctorActivityToAdd);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            await _doctorActivityRespository.AddAsync(doctorActivityToAdd);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorActivityToAdd);
        }

        public override async Task<Result<DoctorActivity>> UpdateAsync(DoctorActivity doctorActivityToUpdate)
        {
            var result = Validar(doctorActivityToUpdate);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            _doctorActivityRespository.Update(doctorActivityToUpdate);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorActivityToUpdate);
        }

        public override async Task<Result> DeleteAsync(DoctorActivity doctorActivityToDelete)
        {
            _doctorActivityRespository.Delete(doctorActivityToDelete);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok();
        }

        public override async Task<Result<List<DoctorActivity>>> RetrieveAllAsync()
        {
            var doctors = await _doctorActivityRespository.RetrieveAllAsync();

            return Result.Ok(doctors);
        }

        public override async Task<Result<DoctorActivity>> RetrieveByIDAsync(Guid id)
        {
            var doctor = await _doctorActivityRespository.RetrieveByIDAsync(id);

            if (doctor == null)
            {
                Log.Logger.Warning("Doctor Activity {DoctorId} not found", id);

                return Result.Fail($"Doctor Activity {id} not found");
            }

            return Result.Ok(doctor);
        }
    }
}