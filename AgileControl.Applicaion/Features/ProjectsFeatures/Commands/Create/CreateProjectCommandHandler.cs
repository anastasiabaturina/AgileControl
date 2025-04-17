using AgileControl.Applicaion.Interfaces;
using AgileControl.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {

        var project = _mapper.Map<Project>(command);

        _context.Projects.Add(project);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProjectCommandResponse
        {
            ProjectId = project.Id,
        };
    }
}
