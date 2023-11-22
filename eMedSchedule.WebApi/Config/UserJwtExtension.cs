using eMedSchedule.Domain.AuthenticationModule;
using eMedSchedule.WebApi.ViewModels.AuthenticationModule;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eMedSchedule.WebApi.Config
{
    public static class UserJwtExtension
    {
        public static TokenViewModel GenerateJwt(this User user, DateTime expirationDate)
        {
            string tokenMain = CreateToken(user, expirationDate);

            var token = new TokenViewModel(tokenMain, expirationDate, new UserTokenViewModel(user.Id, user.Email, user.Name));

            return token;
        }

        private static string CreateToken(User user, DateTime expirationDate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes("SuperSecretEMEDSchedule");

            var tokenMain = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "eMed",
                Audience = "http://localhost",
                Subject = GetIdentityClaims(user),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            });

            string token = tokenHandler.WriteToken(tokenMain);

            return token;
        }

        private static ClaimsIdentity GetIdentityClaims(User user)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.GivenName, user.Name));

            return identityClaims;
        }
    }
}