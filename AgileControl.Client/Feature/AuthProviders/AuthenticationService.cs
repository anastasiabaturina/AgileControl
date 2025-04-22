using AgileControl.API.Models.Requests;
using AgileControl.Applicaion.Models.Requests;
using AgileControl.Client.Interfaces;
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

    // Регистрация нового пользователя
    public async Task<string> RegisterAsync(string email, string userName, string password)
    {
        var requestBody = new RegisterUserRequest
        {
            Email = email,
            UserName = userName,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync("api/v1/auth/register", requestBody);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            await AuthenticateAsync(token); // Сохраняем токен в localStorage
            return token;
        }

        return null; // Ошибка регистрации
    }

    // Логин пользователя
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
            await AuthenticateAsync(token); // Сохраняем токен в localStorage
            return token;
        }

        return null; // Ошибка логина
    }

    // Аутентификация (сохранение токена в localStorage)
    public async Task AuthenticateAsync(string token)
    {
        await _localStorage.SetItemAsync("authToken", token);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    // Получение текущего токена из localStorage
    public async Task<string> GetTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        return token ?? string.Empty;
    }

    // Выход из системы (удаление токена)
    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    // Проверка, авторизован ли пользователь
    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }
}