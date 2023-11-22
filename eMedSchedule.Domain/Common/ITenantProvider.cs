namespace eMedSchedule.Domain.Common
{
    public interface ITenantProvider
    {
        Guid UserId { get; }
    }
}