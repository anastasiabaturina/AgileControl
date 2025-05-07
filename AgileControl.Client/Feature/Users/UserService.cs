using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;
using AgileControl.Applicaion.Features.UsersFeatures.Queries.SearchByProject;
using AgileControl.Client.Interfaces;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AgileControl.Client.Feature.Users;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public UserService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<SearchUserByNameResponse> SearchByName(string name, int? limit)
    {
        try
        {
            await AddAuthHeader();

            var response = await _httpClient.GetFromJsonAsync<Response<SearchUserByNameResponse>>(
                $"api/v1/users/search?name={name}&limit={limit}");

            return response?.Data;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<SearchByProjectResponse> SearchByNameUserByProject(string name, int? limit, Guid projectId)
    {

            await AddAuthHeader();

            // Безопасное формирование URL с параметрами
            var queryParams = new Dictionary<string, string>
            {
                ["name"] = name,
                ["limit"] = limit?.ToString() ?? string.Empty
            };

            var queryString = new FormUrlEncodedContent(queryParams)
                .ReadAsStringAsync()
                .Result;

        var response = await _httpClient.GetFromJsonAsync<Response<SearchByProjectResponse>>(
             $"api/v1/users/project/search?projectid={projectId}&name={name}&limit={limit}");
        
        return response?.Data;
    }

    private async Task AddAuthHeader()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
