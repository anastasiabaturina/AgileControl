using AgileControl.API.Models.Exceptions;
using AgileControl.Domain.Entities;
using AgileControl.Infrastructure.Context;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<CreateTaskResponse> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var projectExists = await _context.Projects
            .AnyAsync(p => p.Id == command.ProjectId, cancellationToken);

        //нужно проверять существует ли проект

        var task = _mapper.Map<ProjectTask>(command);
        task.UserCreated = await _userManager.FindByIdAsync(command.IdUserCreated.ToString());

        if (command.AssigneeId != null)
        {
            var assignee = await _userManager.FindByIdAsync(command.AssigneeId.ToString());
            if (assignee == null)
            {
                throw new BadRequestException($" Исполнитель {command.AssigneeId} не найден");
            }
            task.Assignee = assignee;
        }

        

        await _context.ProjectTasks.AddAsync(task, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        if (command.CheckLists != null && command.CheckLists.Any())
        {
            task.CheckList = command.CheckLists
                .Select(item => new CheckList
                {
                    Text = item.Text,
                    IsCompleted = false,
                    CreatedDate = DateTime.UtcNow,
                })
                .ToList();
        }
        // ошибка в сохранении позадач поэтому нужно сначала сохранять задачу а потом отдельно подзадачи либо сделать отдельный контроллер специальный для чек-листов 
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTaskResponse
        {
            TaskId = task.Id,
        };
    }Microsoft.EntityFrameworkCore.DbUpdateException: "An error occurred while saving the entity changes. See the inner exception for details."

}