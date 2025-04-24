namespace AgileControl.Applicaion.Models.Dtos;

public class CreaterUserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
}