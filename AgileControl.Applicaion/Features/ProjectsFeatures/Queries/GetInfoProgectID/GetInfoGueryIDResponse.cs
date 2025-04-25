using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Domain.Entities;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;

public class GetInfoGueryIDResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? EndDate { get; set; }

    public UserDto Creator { get; set; } = default!;

    public List<ProjectMemberDtoResponse> ProjectMembers { get; set; } = new List<ProjectMemberDtoResponse>();
}