using AgileControl.API.Models.Exceptions;
using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Domain.Entities;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;

public class GetInfoProjectIDQueryIHandler : IRequestHandler<GetInfoProjectIDQuery, GetInfoGueryIDResponse>
{
    private readonly ApplicationDbContext _context;

    public GetInfoProjectIDQueryIHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetInfoGueryIDResponse> Handle(GetInfoProjectIDQuery query, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
    .Include(p => p.CreatorUser)
    .Include(p => p.ProjectMembers)
        .ThenInclude(pm => pm.User)
    .FirstOrDefaultAsync(p => p.Id == query.ProjectId, cancellationToken);

        if (project == null)
        {
            throw new NotFoundException("Проект не найден");
        }

        return new GetInfoGueryIDResponse
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            CreatedDate = project.CreatedDate,
            EndDate = project.EndDate,
            Creator = new UserDto
            {
                Id = project.CreatorUser.Id,
                UserName = project.CreatorUser.UserName,
                Email = project.CreatorUser.Email
            },
            ProjectMembers = project.ProjectMembers.Select(pm => new ProjectMemberDtoResponse
            {
                UserId = pm.User.Id,
                UserName = pm.User.UserName,
                UserEmail = pm.User.Email,
                ProjectRole = pm.ProjectRole
            }).ToList()
        };
    }
}
