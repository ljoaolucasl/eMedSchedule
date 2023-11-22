using eMedSchedule.Domain.Common;
using System.Security.Claims;

namespace eMedSchedule.WebApi.Config.AutomapperConfig
{
    public class UserResolver : IValueResolver<Object, Entity, Guid>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserResolver(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid Resolve(Object source, Entity destination, Guid destMember, ResolutionContext context)
        {
            return Guid.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}