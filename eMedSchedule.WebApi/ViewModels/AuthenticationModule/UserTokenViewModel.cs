namespace eMedSchedule.WebApi.ViewModels.AuthenticationModule
{
    public class UserTokenViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public UserTokenViewModel(Guid id, string email, string name)
        {
            Id = id;
            Email = email;
            Name = name;
        }
    }
}