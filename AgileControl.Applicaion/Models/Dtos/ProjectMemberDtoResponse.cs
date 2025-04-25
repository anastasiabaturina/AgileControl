using AgileControl.Domain.Enums;

namespace AgileControl.Applicaion.Models.Dtos;

public class ProjectMemberDtoResponse
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public ProjectRole? ProjectRole { get; set; }
}