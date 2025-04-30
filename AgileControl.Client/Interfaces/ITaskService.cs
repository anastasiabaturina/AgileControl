//using AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;
//using AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;
//using AgileControl.Applicaion.Models.Dtos;
//using AgileControl.Domain.Enums;
//using AgileControl.Shared.Features.Requests.Tasks;

//namespace AgileControl.Client.Interfaces;

//public interface ITaskService
//{
//    Task<IReadOnlyList<TaskDto>> GetTasksByStatusAsync(Guid projectId, Status status);
//    Task<CreateTaskResponse> AddTaskAsync(
//        CreateTaskRequest request,
//        CancellationToken cancellationToken = default);

//    Task<UpdateColumnTaskResponse> UpdateTaskStatusAsync(
//        Guid taskId,
//        Status newStatus,
//        CancellationToken cancellationToken = default);
//}
