using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.Kanban.Commands.Create;
using AgileControl.Applicaion.Features.Column.Queiries.GetTitle;
using AgileControl.Shared.Features.Requests.Columns;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Endpoints.Controllers;

[Route("api/v1/boards")]
[ApiController]
[Authorize]

public class KanbanBoardController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public KanbanBoardController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateColumnRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateColumnCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = new Response<CreateColumnResponse>
        {
            Data = result
        };

        var location = Url.Action(nameof(CreateAsync), new { id = result });
        return Created(location!, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTitleAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTitleQuery
        {
            ColumnId = id
        };

        var result = await _mediator.Send(query, cancellationToken);

        var response = new Response<GetTitleResponse>
        {
            Data = result
        };

        return Ok(response);
    }
}