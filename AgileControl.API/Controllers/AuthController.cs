using AgileControl.API.Models.Requests;
using AgileControl.API.Models.Responses;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Register;
using AgileControl.Applicaion.Models.Requests;
using AgileControl.Applicaion.Models.Responses;
using AgileControl.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AgileControl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public AuthController(SignInManager<User> signInManager, 
        IMapper mapper, 
        IMediator mediator, 
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _mapper = mapper;
        _mediator = mediator;
        _configuration = configuration;
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

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

        if (!result.Succeeded)
        {
            return BadRequest(new Response<object>
            {
                Error = new Error
                {
                    Message = "Неверный логин или пароль",
                    ErrorCode = HttpStatusCode.BadRequest
                }
            });
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, request.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

        var token = new JwtSecurityToken(
            _configuration["JwtIssuer"],
            _configuration["JwtAudience"],
            claims,
            expires: expiry,
            signingCredentials: creds
        );

        var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new LoginResponse { Token = tokenResponse });
    }

    //добавить метод по выходу из системы и запись токена в бд
}