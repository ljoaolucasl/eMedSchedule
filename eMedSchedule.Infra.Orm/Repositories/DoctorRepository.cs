using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.Infra.Orm.Common;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace eMedSchedule.Infra.Orm.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository()
        {
        }

        public DoctorRepository(EMedScheduleContext contextDb) : base(contextDb)
        {
        }

        public override async Task<Doctor> RetrieveByIDAsync(Guid id)
        {
            return await Data
                .Include(x => x.Activities)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<Doctor>> RetrieveAllAsync()
        {
            return await Data.Include(x => x.Activities).ToListAsync();
        }

        public List<Doctor> RetrieveMany(List<Guid> doctorsIds)
        {
            return Data.Include(x => x.Activities).Where(doctor => doctorsIds.Contains(doctor.Id)).ToList();
        }

        public bool Exist(Doctor objectToCheck)
        {
            return Data.ToList().Any(d => d.Id != objectToCheck.Id && d.CRM == objectToCheck.CRM);
        }

        public List<Doctor> GetListDoctorsMoreHoursWorked(DateTime startDate, DateTime endDate)
        {
            return Data
                .Include(d => d.Activities)
                .ToList()
                .Select(d =>
                {
                    d.CalculateWorkedHourDoctorsPeriod(startDate, endDate);
                    return d;
                })
                .OrderByDescending(d => d.WorkedHours)
                .ToList();
        }
    }
}