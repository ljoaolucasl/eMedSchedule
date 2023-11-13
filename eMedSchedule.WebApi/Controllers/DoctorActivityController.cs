﻿using eMedSchedule.Application.Services;
using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.WebApi.ViewModels.DoctorActivityModule;

namespace eMedSchedule.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorActivityController : ApiControllerBase
    {
        private readonly DoctorActivityService _doctorActivityService;

        public DoctorActivityController(DoctorActivityService doctorActivityService, IMapper mapper) : base(mapper)
        {
            _doctorActivityService = doctorActivityService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsDoctorActivityViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> PostAsync(FormsDoctorActivityViewModel doctorActivity)
        {
            var resultado = await _doctorActivityService.AddAsync(mapper.Map<DoctorActivity>(doctorActivity));

            return ProcessResult(resultado, doctorActivity);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsDoctorActivityViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> PutAsync(Guid id, FormsDoctorActivityViewModel doctorActivity)
        {
            var searchResult = await _doctorActivityService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            var result = await _doctorActivityService.UpdateAsync(mapper.Map(doctorActivity, searchResult.Value));

            return ProcessResult(result, doctorActivity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FormsDoctorActivityViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var searchResult = await _doctorActivityService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            var resultado = await _doctorActivityService.DeleteAsync(searchResult.Value);

            return ProcessResult<DoctorActivity>(resultado, mapper.Map<FormsDoctorActivityViewModel>(searchResult.Value));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListDoctorActivityViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetAsync()
        {
            var searchResult = await _doctorActivityService.RetrieveAllAsync();

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            return ProcessResult(searchResult, mapper.Map<List<ListDoctorActivityViewModel>>(searchResult.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormsDoctorActivityViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var searchResult = await _doctorActivityService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            return ProcessResult(searchResult, mapper.Map<FormsDoctorActivityViewModel>(searchResult.Value));
        }

        [HttpGet("complete-view/{id}")]
        [ProducesResponseType(typeof(CompleteDoctorActivityViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> GetCompleteByIdAsync(Guid id)
        {
            var searchResult = await _doctorActivityService.RetrieveByIDAsync(id);

            if (searchResult.IsFailed)
                return NotFound(searchResult);

            return ProcessResult(searchResult, mapper.Map<CompleteDoctorActivityViewModel>(searchResult.Value));
        }
    }
}