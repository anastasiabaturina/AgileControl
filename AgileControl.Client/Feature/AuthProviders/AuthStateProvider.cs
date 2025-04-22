using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace AgileControl.Client.Feature.AuthProviders;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Получаем токен из localStorage
        var token = await _localStorage.GetItemAsync<string>("authToken");

        // Если токен отсутствует, возвращаем состояние как анонимное
        if (string.IsNullOrWhiteSpace(token))
            return _anonymous;

        // Добавляем токен в заголовок Authorization для всех запросов
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Разбираем токен и создаем ClaimsPrincipal
        var claims = JwtParser.ParseClaimsFromJwt(token);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));

        // Возвращаем аутентифицированное состояние
        return new AuthenticationState(user);
    }

    // Метод для уведомления системы о том, что пользователь аутентифицирован
    public void NotifyUserAuthentication(string token)
    {
        var claims = JwtParser.ParseClaimsFromJwt(token);
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    // Метод для уведомления о выходе пользователя
    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}