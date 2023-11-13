﻿using eMedSchedule.Application.Services;
using eMedSchedule.Domain.DoctorModule;
using eMedSchedule.WebApi.ViewModels.DoctorModule;

namespace eMedSchedule.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ApiControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService, IMapper mapper) : base(mapper)
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
        [ProducesResponseType(typeof(ListDoctorViewModel), 200)]
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
    }
}