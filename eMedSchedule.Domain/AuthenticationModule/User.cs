using Microsoft.AspNetCore.Identity;
using SequentialGuid;

namespace eMedSchedule.Domain.AuthenticationModule
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Id = SequentialGuidGenerator.Instance.NewGuid();
        }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            UserName = email;
        }

        public string Name { get; set; }
    }
}