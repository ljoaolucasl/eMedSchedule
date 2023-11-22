using eMedSchedule.Domain.Common;

namespace eMedSchedule.Tests.Common
{
    public class TestTenantProvider : ITenantProvider
    {
        public TestTenantProvider(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}