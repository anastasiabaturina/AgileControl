using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;
using AgileControl.Applicaion.Features.UsersFeatures.Queries.SearchByProject;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Endpoints.Controllers;

[Route("api/v1/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByNameAsync([FromQuery] string name,  [FromQuery] int limit)
    {
        var query = new SearchUserByNameQuery { SearchName = name, Limit = limit };
        var result = await _mediator.Send(query);

        var response = new Response<SearchUserByNameResponse>
        {
            Data = result,
        };

        return Ok(response);
    }

    [HttpGet("project/search")]
    public async Task<IActionResult> SearchByNameByProjectAsync([FromQuery] Guid projectid,[FromQuery] string name, [FromQuery] int limit)
    {
        var query = new SearchByProjectQuery
        {
            SearchName = name,
            Limit = limit,
            ProjectId = projectid
        };

        var result = await _mediator.Send(query);

        var response = new Response<SearchByProjectResponse>
        {
            Data = result,
        };

        return Ok(response);
    }
}