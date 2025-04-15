using AgileControl.Domain.Entities;
using MediatR;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;

public class CreateProjectCommand : IRequest<CreateProjectCommandResponse>
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime EndDate { get; set; }

    public ICollection<Guid>? UsersId { get; set; }
}