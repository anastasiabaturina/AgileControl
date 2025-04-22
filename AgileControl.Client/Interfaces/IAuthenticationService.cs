namespace AgileControl.Client.Interfaces;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(string email, string userName, string password);

    Task<string> LoginAsync(string email, string password);

    Task AuthenticateAsync(string token);

    Task<string> GetTokenAsync();

    Task LogoutAsync();

    Task<bool> IsAuthenticatedAsync();
}