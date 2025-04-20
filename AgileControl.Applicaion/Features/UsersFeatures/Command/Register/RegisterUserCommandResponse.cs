using AgileControl.Domain.Entities;

namespace AgileControl.Applicaion.Features.UsersFeatures.Command.Register;

public class RegisterUserCommandResponse
{
    public User User { get; set; }

    public string Token { get; set; }
}