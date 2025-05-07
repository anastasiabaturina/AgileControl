using AgileControl.Applicaion.Features.Column.Queiries.GetTitle;
using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Shared.Features.Requests.Columns;
using AgileControl.Shared.Features.Requests.Tasks;

namespace AgileControl.Client.Interfaces;

public interface ITaskService
{
    Task<GetTitleResponse> GetInfoColumnAsync(Guid projectId);

    Task<List<TaskDto>> GetTasksAsync(Guid projectId);

    Task<Guid?> AddColumnAsync(CreateColumnRequest request);

    Task<Guid> CreateTaskAsync(CreateTaskRequest request);
}