using MediatR;

namespace AgileControl.Applicaion.Features.UsersFeatures.Command.Login;

public class LoginUserCommand : IRequest<LoginUserResponse>
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
