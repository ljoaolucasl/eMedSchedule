using eMedSchedule.Application.Common;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using Serilog;

namespace eMedSchedule.Application.Services
{
    public class DoctorService : Service<Doctor, DoctorValidator>, IDoctorService
    {
        private readonly IPersistenceContext _persistenceContext;
        private readonly IDoctorRepository _doctorRespository;

        public DoctorService(IPersistenceContext persistenceContext, IDoctorRepository doctorRespository)
        {
            _persistenceContext = persistenceContext;
            _doctorRespository = doctorRespository;
        }

        public override async Task<Result<Doctor>> AddAsync(Doctor doctorToAdd)
        {
            Result result = Validar(doctorToAdd);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            await _doctorRespository.AddAsync(doctorToAdd);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorToAdd);
        }

        public override async Task<Result<Doctor>> UpdateAsync(Doctor doctorToUpdate)
        {
            var result = Validar(doctorToUpdate);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            _doctorRespository.Update(doctorToUpdate);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok(doctorToUpdate);
        }

        public override async Task<Result> DeleteAsync(Doctor doctorToDelete)
        {
            _doctorRespository.Delete(doctorToDelete);

            await _persistenceContext.SaveDataAsync();

            return Result.Ok();
        }

        public override async Task<Result<List<Doctor>>> RetrieveAllAsync()
        {
            var doctors = await _doctorRespository.RetrieveAllAsync();

            return Result.Ok(doctors);
        }

        public override async Task<Result<Doctor>> RetrieveByIDAsync(Guid id)
        {
            var doctor = await _doctorRespository.RetrieveByIDAsync(id);

            if (doctor == null)
            {
                Log.Logger.Warning("Doctor {DoctorId} not found", id);

                return Result.Fail($"Doctor {id} not found");
            }

            return Result.Ok(doctor);
        }
    }
}