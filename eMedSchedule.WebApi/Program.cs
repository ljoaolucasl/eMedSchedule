using Serilog;

namespace eMedSchedule.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Services.AddSerilog(Log.Logger);

            builder.Services.ConfigureControllers();

            builder.Services.ConfigureDependencyInjection(builder.Configuration);
            builder.Services.ConfigureAutoMapper();
            builder.Services.ConfigureSwagger();

            var app = builder.Build();

            app.UseMiddleware<ManipulatorExceptions>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}