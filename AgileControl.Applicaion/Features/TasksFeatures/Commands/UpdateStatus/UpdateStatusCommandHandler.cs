using AgileControl.API.Models.Exceptions;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;

public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, UpdateStatusResponse>
{
    private readonly ApplicationDbContext _context;

    public UpdateStatusCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateStatusResponse> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectTasks
            .FirstOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

        if (task == null)
        {
            throw new BadRequestException("Задачи не сущесвует");
        }

        task.Status = command.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateStatusResponse()
        {
            TaskId = task.Id,
        };
    }
}
