using Serilog;
using System.Text.Json;

namespace eMedSchedule.WebApi.Config
{
    public class ManipulatorExceptions
    {
        private readonly RequestDelegate _requestDelegate;

        public ManipulatorExceptions(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _requestDelegate(ctx);
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;
                ctx.Response.ContentType = "application/json";

                var erro = new
                {
                    sucesso = false,
                    erros = new List<string> { ex.Message }
                };

                Log.Logger.Error(ex, ex.Message);

                ctx.Response.WriteAsync(JsonSerializer.Serialize(erro));
            }
        }
    }
}