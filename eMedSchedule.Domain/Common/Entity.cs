using eMedSchedule.Domain.AuthenticationModule;
using SequentialGuid;

namespace eMedSchedule.Domain.Common
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Entity()
        {
            Id = SequentialGuidGenerator.Instance.NewGuid();
        }
    }
}