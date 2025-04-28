using AgileControl.Applicaion.Models.Dtos;
using MediatR;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;

public record GetTasksByStatusQuery(
    Guid ProjectId,
    Domain.Enums.TaskStatus Status) : IRequest<IReadOnlyList<TaskDto>>;