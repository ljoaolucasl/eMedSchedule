namespace eMedSchedule.Infra.Orm.Common
{
    public interface IPersistenceContext
    {
        Task SaveDataAsync();
    }
}