using AgileControl.Applicaion.Models.Dtos;

namespace AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;

public class SearchUserByNameResponse
{
    public List<UserDto> Users { get; set; }
}