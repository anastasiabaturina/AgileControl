using MediatR;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.Delete;

public class DeleteTaskCommand : IRequest<DeleteTaskResponse>
{
    public Guid TaskId { get; set; }
}