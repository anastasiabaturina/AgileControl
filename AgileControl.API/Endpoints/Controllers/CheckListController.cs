using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.CheckListsFeature.Command.Create;
using AgileControl.Applicaion.Features.CheckListsFeature.Queries;
using AgileControl.Shared.Features.Requests.CheckLists;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Endpoints.Controllers;

[Route("api/v1/checklists")]
[ApiController]
[Authorize]
public class CheckListController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CheckListController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCheckListsRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateCheckListCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = new Response<CreateCheckListResponse>
        {
            Data = result
        };

        var location = Url.Action(nameof(CreateAsync), new { id = result });
        return Created(location!, response);
    }

    [HttpGet("tasks/{taskId}/checklists")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] Guid taskId,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetCheckListQuery
        {
            TaskId = taskId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query, cancellationToken);

        var response = new Response<GetCheckListResponse>
        {
            Data = result
        };

        return Ok(response);
    }
}