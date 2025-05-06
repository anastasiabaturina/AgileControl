using AgileControl.API.Extensions;
using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;
using AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;
using AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;
using AgileControl.Shared.Features.Requests.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Endpoints.Controllers;

[Route("api/v1/tasks")]
[ApiController]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public TasksController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
       [FromBody] CreateTaskRequest request,
       CancellationToken cancellationToken)
    {
        var userCreatedId = HttpContext.GetUserId();

        var command = _mapper.Map<CreateTaskCommand>(request);
        command.IdUserCreated = userCreatedId;

        var result = await _mediator.Send(command, cancellationToken);

        var response = new Response<CreateTaskResponse>
        {
            Data = result
        };

        var location = Url.Action(nameof(CreateAsync), new { id = result.TaskId });
        return Created(location!, response);
    }

    [HttpGet("projects/{projectId}")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var query = new GetTasksQuery
        {
            ProjectId = projectId,
        };

        var result = await _mediator.Send(query, cancellationToken);

        var response = new Response<GetTaskResponse>
        {
            Data = result
        };

        return Ok(response);
    }

    [HttpPatch("{taskId}/columns/{columnId}")]
    public async Task<IActionResult> UpdateStatus(
        [FromRoute] Guid taskId,
        [FromRoute] Guid columnId,
        CancellationToken cancellationToken)
    {
        var command = new UpdateColumnTaskCommand
        {
            TaskId = taskId,
            ColumnId = columnId
        };

        var result = await _mediator.Send(command, cancellationToken);

        var response = new Response<UpdateColumnTaskResponse>
        {
            Data = result
        };

        return Ok(response);
    }
}