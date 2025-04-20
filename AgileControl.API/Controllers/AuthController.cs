using AgileControl.API.Models.Requests;
using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Register;
using AgileControl.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthController(SignInManager<User> signInManager, IMapper mapper, IMediator mediator)
    {
        _signInManager = signInManager;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);

        var user = await _mediator.Send(command);

        var result = _mapper.Map<User>(user);

        await _signInManager.SignInAsync(result, false);

        var response = new Response<string>
        {
            Data = user.Token,
        };

        return CreatedAtAction(nameof(Register), response);
    }
}