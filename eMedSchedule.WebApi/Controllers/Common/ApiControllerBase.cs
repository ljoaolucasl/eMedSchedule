using FluentResults;

namespace eMedSchedule.WebApi.Controllers.Common
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMapper mapper;

        public ApiControllerBase(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public override OkObjectResult Ok(object? data)
        {
            return base.Ok(new
            {
                sucesso = true,
                data
            });
        }

        public override BadRequestObjectResult BadRequest(object objErrors)
        {
            IList<IError> erros = (List<IError>)objErrors;

            return base.BadRequest(new
            {
                sucesso = false,
                erros = erros.Select(x => x.Message)
            });
        }

        public override NotFoundObjectResult NotFound(object objErrors)
        {
            IList<IError> erros = (List<IError>)objErrors;

            return base.NotFound(new
            {
                sucesso = false,
                erros = erros.Select(x => x.Message)
            });
        }

        protected IActionResult ProcessResult<T>(Result<T> response, object model)
        {
            if (response.IsFailed)
                return BadRequest(response.Errors);

            return Ok(model);
        }
    }
}