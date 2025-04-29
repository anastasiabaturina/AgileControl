using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using MediatR;

namespace AgileControl.Applicaion.Features.UsersFeatures.Command.Register;

public class RegisterUserCommand : IRequest<RegisterUserCommandResponse>
{
    public string Email { get; set; } = default!;

    public string FirstName { get; set; } =default!;

    public string NickName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Password { get; set; } = default!;
}