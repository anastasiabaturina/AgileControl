using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.Delete;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskResponse>
{
    private readonly ApplicationDbContext _context;

    public DeleteTaskCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTaskResponse> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

        _context.ProjectTasks.Remove(task);

        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTaskResponse();
    }
}
