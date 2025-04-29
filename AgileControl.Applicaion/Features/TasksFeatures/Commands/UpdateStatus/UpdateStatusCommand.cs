using AgileControl.Domain.Enums;
using MediatR;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;

public class UpdateStatusCommand : IRequest<UpdateStatusResponse>
{
    public Guid TaskId { get; set; }

    public Status Status { get; set; }
}