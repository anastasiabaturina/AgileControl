using AgileControl.Domain.Entities;
using AgileControl.Infrastructure.Context;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {

        var project = _mapper.Map<Project>(command);

        var user = await _userManager.FindByIdAsync(command.CreaterId.ToString());

        project.CreatorUser = user;

        _context.Projects.Add(project);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProjectCommandResponse
        {
            ProjectId = project.Id,
        };
    }
}