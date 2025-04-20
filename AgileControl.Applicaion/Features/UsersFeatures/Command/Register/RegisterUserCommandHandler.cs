using AgileControl.API.Models.Exceptions;
using AgileControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgileControl.Applicaion.Features.UsersFeatures.Command.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public RegisterUserCommandHandler(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var userExist = await _userManager.FindByEmailAsync(command.Email);

        if (userExist != null)
        {
            throw new BadRequestException("Пользователь с таким e-mail уже существует");
        }

        var user = new User
        {
            UserName = command.UserName,
            Email = command.Email,
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Errors.Any())
        {
            throw new BadRequestException(result.Errors.First().Description);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Email)
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

        return new RegisterUserCommandResponse
        {
            User = user,
            Token = tokenResponse
        };
    }
}