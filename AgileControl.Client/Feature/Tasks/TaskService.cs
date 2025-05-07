using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.Column.Queiries.GetTitle;
using AgileControl.Applicaion.Features.Kanban.Commands.Create;
using AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;
using AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;
using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Client.Interfaces;
using AgileControl.Shared.Features.Requests.Columns;
using AgileControl.Shared.Features.Requests.Tasks;
using System.Net.Http.Json;

namespace AgileControl.Client.Feature.Tasks;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetTitleResponse> GetInfoColumnAsync(Guid projectId)
    {
        var response = await _httpClient.GetAsync($"api/v1/boards/projects/{projectId}/");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = await response.Content.ReadFromJsonAsync<Response<GetTitleResponse>>();

        return apiResponse?.Data ?? null;
    }

    public async Task<List<TaskDto>> GetTasksAsync(Guid projectId)
    {
        var response = await _httpClient.GetAsync($"api/v1/tasks/projects/{projectId}/");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = await response.Content.ReadFromJsonAsync<Response<GetTaskResponse>>();

        return apiResponse?.Data.Tasks ?? null;
    }

    public async Task<Guid?> AddColumnAsync(CreateColumnRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"api/v1/boards",
            request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = await response.Content.ReadFromJsonAsync<Response<CreateColumnResponse>>();

        return apiResponse?.Data.ColumnId;
    }

    public async Task<Guid> CreateTaskAsync(CreateTaskRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"api/v1/tasks",
            request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = await response.Content.ReadFromJsonAsync<Response<CreateTaskResponse>>();

        return apiResponse.Data.TaskId;
    }

    //public async Task<CreateTaskResponse> AddTaskAsync(
    //    CreateTaskRequest request,
    //    CancellationToken cancellationToken = default)
    //{
    //    var response = await _httpClient.PostAsJsonAsync(
    //        "api/v1/tasks",
    //        request,
    //        cancellationToken);

    //    response.EnsureSuccessStatusCode();

    //    var apiResponse = await response.Content.ReadFromJsonAsync<Response<CreateTaskResponse>>(cancellationToken);
    //    return apiResponse?.Data ?? throw new InvalidOperationException("Invalid API response");
    //}

    //public async Task<UpdateColumnTaskResponse> UpdateTaskStatusAsync(
    //    Guid taskId,
    //    CancellationToken cancellationToken = default)
    //{
    //    var response = await _httpClient.PatchAsJsonAsync(
    //        $"api/v1/tasks/{taskId}/status",
    //        cancellationToken);

    //    response.EnsureSuccessStatusCode();

    //    var apiResponse = await response.Content.ReadFromJsonAsync<Response<UpdateColumnTaskResponse>>(cancellationToken);
    //    return apiResponse?.Data ?? throw new InvalidOperationException("Invalid API response");
    //}
}