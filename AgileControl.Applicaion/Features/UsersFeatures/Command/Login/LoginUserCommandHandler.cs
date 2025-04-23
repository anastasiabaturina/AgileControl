using AgileControl.API.Models.Exceptions;
using AgileControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgileControl.Applicaion.Features.UsersFeatures.Command.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(command.Email);

        if (user == null)
        {
            throw new BadRequestException("Пользователя с таким e-mail не существует");
        }

        if (!await _userManager.CheckPasswordAsync(user, command.Password))
        {
            throw new BadRequestException("Неверный пароль");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Email)
        };

        var jwtSettings = _configuration.GetSection("JWTSettings");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["JwtSecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(jwtSettings["JwtExpiryInDays"]));

        var token = new JwtSecurityToken(
            jwtSettings["JwtIssuer"],
            jwtSettings["JwtAudience"],
            claims,
            expires: expiry,
            signingCredentials: creds
        );

        var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginUserResponse
        {
            Token = tokenResponse
        };
    }
}
