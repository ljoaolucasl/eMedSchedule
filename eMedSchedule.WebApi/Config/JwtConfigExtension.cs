using eMedSchedule.Domain.AuthenticationModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Net.Sockets;
using System.Text;

namespace eMedSchedule.WebApi.Config
{
    public static class JwtConfigExtension
    {
        public static void ConfigureJwt(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes("SuperSecretEMEDSchedule");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidAudience = "http://localhost",
                    ValidIssuer = "eMed",
                };
            });
        }
    }
}