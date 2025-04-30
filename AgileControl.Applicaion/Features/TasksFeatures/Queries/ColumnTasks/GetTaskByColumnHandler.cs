using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;

public class GetTaskByColumnHandler : IRequestHandler<GetTasksByStatusQuery, GetTaskByColumnResponse>
{
    private readonly ApplicationDbContext _context;

    public GetTaskByColumnHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetTaskByColumnResponse> Handle(GetTasksByStatusQuery query, CancellationToken cancellationToken)
    {
        var tasks = await _context.ProjectTasks
            .Where(t => t.ProjectId == query.ProjectId && t.KanbanColumnId == query.Columnid)
            .Include(t => t.Assignee)
            .Include(t => t.UserCreated)
            .Include(t => t.CheckList)
            .Select(t => new TaskDto
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
                CheckList = t.CheckList.Select(cl => new CheckListDto
                {
                    Id = cl.Id,
                    Text = cl.Text,
                    IsCompleted = cl.IsCompleted,
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return new GetTaskByColumnResponse
        {
            Tasks = tasks
        };
    }
}