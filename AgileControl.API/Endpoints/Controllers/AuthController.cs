using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Login;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Register;
using AgileControl.Client.Pages.Auth;
using AgileControl.Shared.Features.Requests.Auth;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);

        var token = await _mediator.Send(command);

        var response = new Response<RegisterUserCommandResponse>
        {
            Data = token,
        };

        return CreatedAtAction(nameof(Register), response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
       var command = _mapper.Map<LoginUserCommand>(request);

       var token = await _mediator.Send(command);

       var response = new Response<LoginUserResponse>
       {
           Data = token,
       };

       return Ok(response);
    }
}