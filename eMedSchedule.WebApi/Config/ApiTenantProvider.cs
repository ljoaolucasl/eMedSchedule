using eMedSchedule.Domain.Common;
using System.Security.Claims;

namespace eMedSchedule.WebApi.Config
{
    public class ApiTenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApiTenantProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var claimId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if (claimId == null)
                    return Guid.Empty;

                return Guid.Parse(claimId.Value);
            }
        }
    }
}