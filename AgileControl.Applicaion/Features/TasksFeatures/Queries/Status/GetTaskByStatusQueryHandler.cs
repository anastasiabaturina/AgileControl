using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;

public class GetTaskByStatusQueryHandler : IRequestHandler<GetTasksByStatusQuery, IReadOnlyList<TaskDto>>
{
    private readonly ApplicationDbContext _context;

    public GetTaskByStatusQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TaskDto>> Handle(GetTasksByStatusQuery query, CancellationToken cancellationToken)
    {
        return await _context.ProjectTasks
            .Where(t => t.ProjectId == query.ProjectId && t.Status == query.Status)
            .Select(t => new TaskDto(
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.CreatedDate,
                t.AssigneeId))
            .ToListAsync(cancellationToken);
    }
}