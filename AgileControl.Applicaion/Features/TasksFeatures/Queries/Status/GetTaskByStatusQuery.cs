using AgileControl.Applicaion.Models.Dtos;
using MediatR;
using System;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;

public class GetTasksByStatusQuery : IRequest<IReadOnlyList<TaskDto>>
{
    public Guid ProjectId { get; set; }

    public Domain.Enums.Status Status { get; set; }
}