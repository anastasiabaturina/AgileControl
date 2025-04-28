using AgileControl.API.Extrnsions;
using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;
using AgileControl.Applicaion.Features.TasksFeatures.Commands.UpdateStatus;
using AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;
using AgileControl.Applicaion.Models.Dtos;
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
    public async Task<IActionResult> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var userCreatedId = HttpContext.GetUserId();

        var command = _mapper.Map<CreateTaskCommand>(request);

        command.IdUserCreated = userCreatedId;

        var result = await _mediator.Send(command, cancellationToken);

        var response = new Response<CreateTaskResponse>
        {
            Data = result
        };

        var location = Url.Action(nameof(CreateAsync));

        return Created(location!, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetByStatus(Guid projectId, Domain.Enums.TaskStatus status)
    {
        var query = new GetTasksByStatusQuery(projectId, status);

        var result = await _mediator.Send(query);

        var response = new Response<IReadOnlyList<TaskDto>>
        {
            Data = result
        };

        return Ok(response);
    }

    [HttpPatch("{taskId}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute]Guid taskId, [FromBody] Domain.Enums.TaskStatus status)
    {
        var command = new UpdateStatusCommand
        {
            TaskId = taskId,
            Status = status
        };

        var result = await _mediator.Send(command);

        var response = new Response<UpdateStatusResponse>
        {
            Data = result
        };

        return Ok(response);
    }
}