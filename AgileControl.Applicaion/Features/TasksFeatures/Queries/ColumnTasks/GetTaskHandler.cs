using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;

public class GetTaskHandler : IRequestHandler<GetTasksQuery, GetTaskResponse>
{
    private readonly ApplicationDbContext _context;

    public GetTaskHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetTaskResponse> Handle(GetTasksQuery query, CancellationToken cancellationToken)
    {
        var tasks = await _context.ProjectTasks
            .Where(t => t.ProjectId == query.ProjectId)
            .Include(t => t.Assignee)
            .Include(t => t.UserCreated) 
            .Select(t => new TaskDto (t.Title, t.KanbanColumnId)
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                EndDate = t.EndDate,
                UserCreated = new UserDto
                {
                    Id = t.UserCreated.Id,
                    UserName = $"{t.UserCreated.FirstName} {t.UserCreated.LastName}",
                    Email = t.UserCreated.Email
                },
                Assignee = t.Assignee != null ? new UserDto
                {
                    Id = t.Assignee.Id,
                    UserName = $"{t.UserCreated.FirstName} {t.UserCreated.LastName}",
                    Email = t.Assignee.Email
                } : null,
                Priority = t.Priority,
                ColumnId = t.KanbanColumnId
            })
            .ToListAsync(cancellationToken);

        return new GetTaskResponse
        {
            Tasks = tasks
        };
    }
}