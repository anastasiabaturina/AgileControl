using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using MediatR;

namespace AgileControl.Applicaion.Features.Kanban.Commands.Create;

public  class CreateColumnCommand : IRequest<CreateColumnResponse>
{
    public string Title { get; set; } = default!;
}