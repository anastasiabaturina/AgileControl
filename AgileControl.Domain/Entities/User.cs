namespace AgileControl.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Avatar { get; set; } = default!;

    public ICollection<ProgectMember> ProgectMembers { get; set; } = new List<ProgectMember>();
}
