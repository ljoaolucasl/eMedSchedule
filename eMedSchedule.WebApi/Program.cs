using Microsoft.AspNetCore.DataProtection;

namespace eMedSchedule.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var ambience = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.Services.ConfigureDependencyInjection(builder.Configuration);

            builder.Logging.ClearProviders();

            builder.Services.ConfigureValidation();
            builder.Services.ConfigureLog();
            builder.Services.ConfigureAutoMapper();
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureControllers();
            builder.Services.ConfigureJwt();

            builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Path.GetTempPath()));

            var app = builder.Build();

            app.UseMiddleware<ManipulatorExceptions>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}