using AgileControl.Domain.Enums;

namespace AgileControl.Applicaion.Models.Dtos;

public class ProjectMemberDto
{
    public Guid UserId { get; set; }

    public ProjectRole? ProjectRole { get; set; }
}