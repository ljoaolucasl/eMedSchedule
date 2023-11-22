namespace eMedSchedule.WebApi.ViewModels.AuthenticationModule
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserTokenViewModel User { get; set; }

        public TokenViewModel(string token, DateTime expirationDate, UserTokenViewModel user)
        {
            Token = token;
            ExpirationDate = expirationDate;
            User = user;
        }
    }
}