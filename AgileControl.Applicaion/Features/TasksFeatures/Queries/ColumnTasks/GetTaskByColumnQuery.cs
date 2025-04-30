using AgileControl.Applicaion.Models.Dtos;
using MediatR;
using System;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;

public class GetTasksByStatusQuery : IRequest<GetTaskByColumnResponse>
{
    public Guid ProjectId { get; set; }

    public Guid Columnid { get; set; }
}