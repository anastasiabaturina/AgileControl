using AgileControl.Domain.Entities;
using AgileControl.Infrastructure.Context;
using MediatR;

namespace AgileControl.Applicaion.Features.Kanban.Commands.Create;

public class CreateColumnCommandHandler : IRequestHandler<CreateColumnCommand, CreateColumnResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateColumnCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CreateColumnResponse> Handle(CreateColumnCommand command, CancellationToken cancellationToken)
    {
        var column = new KanbanColumn
        {
            Title = command.Title,
            ProjectId = command.ProjectId,
        };

        _context.Columns.Add(column);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateColumnResponse
        {
            ColumnId = column.Id,
        };
    }
}
