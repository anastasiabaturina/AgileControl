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

        if (!projectExists)
        {
            throw new BadRequestException($"Проект с ID {command.ProjectId} не найден");
        }

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

        _context.ProjectTasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTaskResponse
        {
            TaskId = task.Id,
        };
    }
}