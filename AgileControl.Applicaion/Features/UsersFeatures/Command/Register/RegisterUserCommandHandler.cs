using AgileControl.API.Models.Exceptions;
using AgileControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AgileControl.Applicaion.Features.UsersFeatures.Command.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
{
    private readonly UserManager<User> _userManager;

    public RegisterUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        //добавить првоерку на существование такого же пользователя 
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

        return new RegisterUserCommandResponse
        {
            Id = user.Id
        };
    }
}