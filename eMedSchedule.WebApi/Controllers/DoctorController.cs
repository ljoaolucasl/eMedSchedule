using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.WebApi.ViewModels.DoctorModule;
using Microsoft.AspNetCore.Authorization;

namespace eMedSchedule.WebApi.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    [Authorize]
    public class DoctorController : ApiControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService, IMapper mapper) : base(mapper)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsDoctorViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> PostAsync(FormsDoctorViewModel doctor)
        {
            var resultado = await _doctorService.AddAsync(mapper.Map<Doctor>(doctor));

            return ProcessResult(resultado, doctor);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsDoctorViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> PutAsync(Guid id, FormsDoctorViewModel doctor)
        {
            var searchResult = await _doctorService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            var result = await _doctorService.UpdateAsync(mapper.Map(doctor, searchResult.Value));

            return ProcessResult(result, doctor);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FormsDoctorViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var searchResult = await _doctorService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            var resultado = await _doctorService.DeleteAsync(searchResult.Value);

            return ProcessResult<Doctor>(resultado, mapper.Map<FormsDoctorViewModel>(searchResult.Value));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListDoctorViewModel[]), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetAsync()
        {
            var searchResult = await _doctorService.RetrieveAllAsync();

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            return ProcessResult(searchResult, mapper.Map<List<ListDoctorViewModel>>(searchResult.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormsDoctorViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var searchResult = await _doctorService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            return ProcessResult(searchResult, mapper.Map<FormsDoctorViewModel>(searchResult.Value));
        }

        [HttpGet("complete-view/{id}")]
        [ProducesResponseType(typeof(CompleteDoctorViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetCompleteByIdAsync(Guid id)
        {
            var searchResult = await _doctorService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            return ProcessResult(searchResult, mapper.Map<CompleteDoctorViewModel>(searchResult.Value));
        }

        [HttpGet("worked-hours/{startDate:datetime}={endDate:datetime}")]
        [ProducesResponseType(typeof(ListWorkedHoursDoctorViewModel[]), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult GetDoctorsWokedHours(DateTime startDate, DateTime endDate)
        {
            var searchResult = _doctorService.GetListDoctorsMoreHoursWorked(startDate.ToUniversalTime(), endDate.ToUniversalTime());

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            return ProcessResult(searchResult, mapper.Map<List<ListWorkedHoursDoctorViewModel>>(searchResult.Value));
        }
    }
}