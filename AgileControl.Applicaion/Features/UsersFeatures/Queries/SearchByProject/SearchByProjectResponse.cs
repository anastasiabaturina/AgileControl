using AgileControl.Applicaion.Models.Dtos;

namespace AgileControl.Applicaion.Features.UsersFeatures.Queries.SearchByProject;

public  class SearchByProjectResponse
{
    public List<UserDto> Users { get; set; }
}