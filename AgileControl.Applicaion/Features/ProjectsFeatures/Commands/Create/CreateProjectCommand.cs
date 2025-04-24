using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Domain.Entities;
using MediatR;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;

public class CreateProjectCommand : IRequest<CreateProjectCommandResponse>
{
    public string Title { get; set; } = default!;

    public Guid CreaterId { get; set; }

    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public List<ProjectMemberDto>? ProjectMembers { get; set; }
}