using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;
using AgileControl.Client.Interfaces;
using AgileControl.Shared.Features.Requests.Projects;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using IAuthenticationService = AgileControl.Client.Interfaces.IAuthenticationService;

namespace AgileControl.Client.Feature.Projects;

public class ProjectService : IProjectService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ProjectService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<CreateProjectCommandResponse> CreateProjectAsync(CreateProjectRequest request)
    {
        await AddAuthHeader();

        var response = await _httpClient.PostAsJsonAsync("api/v1/projects", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Response<CreateProjectCommandResponse>>();
            return result?.Data;
        }

        return null;
    }

    public async Task<GetInfoGueryIDResponse?> GetProjectByIdAsync(Guid projectId)
    {
        try
        {
            await AddAuthHeader();
            var response = await _httpClient.GetFromJsonAsync<Response<GetInfoGueryIDResponse>>(
                $"api/v1/projects/{projectId}");

            return response?.Data;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
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
