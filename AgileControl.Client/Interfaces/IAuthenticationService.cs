namespace AgileControl.Client.Interfaces;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(string email, string firstName, string lastName, string nickName, string password);

    Task<string> LoginAsync(string email, string password);

    Task AuthenticateAsync(string token);

    Task<string> GetTokenAsync();

    Task LogoutAsync();

    Task<bool> IsAuthenticatedAsync();
}