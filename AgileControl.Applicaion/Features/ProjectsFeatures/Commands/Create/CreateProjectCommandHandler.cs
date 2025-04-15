using AgileControl.Applicaion.Interfaces;
using AgileControl.Domain.Entities;
using MediatR;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse>
{
    private readonly IAgileControlDbContext _context;

    public CreateProjectCommandHandler(IAgileControlDbContext context)
    {
        _context = context;
    }

    public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var users = _context.Set<User>()
            .Where(Id  == command.UsersId.ToList())
    }
}
