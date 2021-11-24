namespace ChatHub.Mobile.Services
{
    public interface IUserService
    {
        string GetCurrentUsername();
        void SaveCurrentUsername(string username);
    }
}