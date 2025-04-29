using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Register;
using AgileControl.Client.Interfaces;
using AgileControl.Shared.Features.Requests.Auth;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AgileControl.Client.Feature.AuthProviders;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<string> RegisterAsync(string email, string firstName, string lastName, string nickName, string password)
    {
        var requestBody = new RegisterUserRequest
        {
            Email = email,
            FirstName = firstName,
            NickName = nickName,
            LastName = lastName,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync("api/v1/auth/register", requestBody);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Response<RegisterUserCommandResponse>>();
            await AuthenticateAsync(result.Data.Token);
            return result.Data.Token;
        }

        return null;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var requestBody = new LoginUserRequest
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync("api/v1/auth/login", requestBody);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            await AuthenticateAsync(token); 
            return token;
        }

        return null;
    }

    public async Task AuthenticateAsync(string token)
    {
        await _localStorage.SetItemAsync("authToken", token);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<string> GetTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        return token ?? string.Empty;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }
}