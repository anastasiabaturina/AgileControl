//using AgileControl.API.Models.Responses;
//using AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;
//using AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;
//using AgileControl.Applicaion.Models.Dtos;
//using AgileControl.Client.Interfaces;
//using AgileControl.Domain.Enums;
//using AgileControl.Shared.Features.Requests.Tasks;
//using System.Net.Http;
//using System.Net.Http.Json;

//namespace AgileControl.Client.Feature.Tasks;

//public class TaskService : ITaskService
//{
//    private readonly HttpClient _httpClient;
//    private readonly ILogger<TaskService> _logger;

//    public TaskService(HttpClient httpClient, ILogger<TaskService> logger)
//    {
//        _httpClient = httpClient;
//        _logger = logger;
//    }

//    public async Task<IReadOnlyList<TaskDto>> GetTasksByStatusAsync(Guid projectId)
//    {
//        try
//        {
//            var response = await _httpClient.GetAsync($"api/v1/tasks/projects/{projectId}/status/{status}");
//            response.EnsureSuccessStatusCode();

//            var content = await response.Content.ReadAsStringAsync();
//            _logger.LogInformation("API Response: {Content}", content);

//            // Десериализуем с учетом обертки Response<>
//            var apiResponse = await response.Content.ReadFromJsonAsync<Response<IReadOnlyList<TaskDto>>>();
//            return apiResponse?.Data ?? new List<TaskDto>();
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error loading tasks");
//            return new List<TaskDto>();
//        }
//    }
//    public async Task<CreateTaskResponse> AddTaskAsync(
//        CreateTaskRequest request,
//        CancellationToken cancellationToken = default)
//    {
//        var response = await _httpClient.PostAsJsonAsync(
//            "api/v1/tasks",
//            request,
//            cancellationToken);

//        response.EnsureSuccessStatusCode();

//        var apiResponse = await response.Content.ReadFromJsonAsync<Response<CreateTaskResponse>>(cancellationToken);
//        return apiResponse?.Data ?? throw new InvalidOperationException("Invalid API response");
//    }

//    public async Task<UpdateColumnTaskResponse> UpdateTaskStatusAsync(
//        Guid taskId,
//        Status newStatus,
//        CancellationToken cancellationToken = default)
//    {
//        var response = await _httpClient.PatchAsJsonAsync(
//            $"api/v1/tasks/{taskId}/status",
//            newStatus,
//            cancellationToken);

//        response.EnsureSuccessStatusCode();

//        var apiResponse = await response.Content.ReadFromJsonAsync<Response<UpdateColumnTaskResponse>>(cancellationToken);
//        return apiResponse?.Data ?? throw new InvalidOperationException("Invalid API response");
//    }
//}