using AgileControl.API.Models.Exceptions;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;

public class UpdateColumnTaskCommandHandler : IRequestHandler<UpdateColumnTaskCommand, UpdateColumnTaskResponse>
{
    private readonly ApplicationDbContext _context;

    public UpdateColumnTaskCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateColumnTaskResponse> Handle(UpdateColumnTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectTasks
            .FirstOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

        if (task == null)
        {
            throw new BadRequestException("Задачи не сущесвует");
        }

        task.KanbanColumnId = command.ColumnId;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateColumnTaskResponse()
        {
            TaskId = task.Id,
        };
    }
}