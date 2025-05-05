using AgileControl.API.Models.Exceptions;
using AgileControl.Domain.Entities;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.CheckListsFeature.Command.Create;

public class CreateCheckListCommandHandler : IRequestHandler<CreateCheckListCommand, CreateCheckListResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateCheckListCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateCheckListResponse> Handle(CreateCheckListCommand command, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectTasks.AnyAsync(t => t.Id == command.TaskId, cancellationToken);

        if (!task)
        {
            throw new BadRequestException($"{command.TaskId} не найдена!");
        }

        if (command.CheckLists != null && command.CheckLists.Any())
        {
            var checkLists = command.CheckLists
                .Select(item => new CheckList
                {
                    Text = item.Text,
                    IsCompleted = false,
                    CreatedDate = DateTime.UtcNow,
                })
                .ToList();

            foreach (var c in checkLists)
            {
                _context.CheckLists.Add(c);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCheckListResponse();
    }
}