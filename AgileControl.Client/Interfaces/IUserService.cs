using AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;

namespace AgileControl.Client.Interfaces;

public interface IUserService
{
    Task<SearchUserByNameResponse> SearchByName(string name, int? limit);
}
