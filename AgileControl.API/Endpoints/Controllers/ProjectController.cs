using AgileControl.API.Extrnsions;
using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;
using AgileControl.Shared.Features.Requests.Projects;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Endpoints.Controllers;

[Route("api/v1/projects")]
[ApiController]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();

        var command = _mapper.Map<CreateProjectCommand>(request);

        command.CreaterId = userId;

        var result = await _mediator.Send(command, cancellationToken);

        var response = new Response<CreateProjectCommandResponse>
        {
            Data = result
        };

        var location = Url.Action(nameof(GetProjectIDAsync), new { id = result.ProjectId });

        return Created(location!, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectIDAsync([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetInfoProjectIDQuery>(id);

        var result = await _mediator.Send(query, cancellationToken);

        var response = new Response<GetInfoGueryIDResponse>
        {
            Data = result
        };

        return Ok(response);
    }
}
