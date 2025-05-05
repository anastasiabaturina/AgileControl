using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Domain.Entities;
using AgileControl.Domain.Enums;
using MediatR;

namespace AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;

public class CreateTaskCommand : IRequest<CreateTaskResponse>
{
    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid IdUserCreated { get; set; }

    public  Guid? AssigneeId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid ColumnId { get; set; }

    public Priority Priority { get; set; }
}