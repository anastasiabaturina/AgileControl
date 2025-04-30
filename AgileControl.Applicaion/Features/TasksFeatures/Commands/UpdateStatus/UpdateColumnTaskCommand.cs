using AgileControl.Domain.Enums;
using MediatR;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;

public class UpdateColumnTaskCommand : IRequest<UpdateColumnTaskResponse>
{
    public Guid TaskId { get; set; }

    public Guid ColumnId { get; set; }
}