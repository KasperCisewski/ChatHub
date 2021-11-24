namespace ChatHub.Mobile.Services.Implementation
{
    public class UserService : IUserService
    {
        private string _username;
        
        public string GetCurrentUsername() => _username;

        public void SaveCurrentUsername(string username)
        {
            _username = username;
        }
    }
}