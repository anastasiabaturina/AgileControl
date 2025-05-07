using AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;
using AgileControl.Applicaion.Features.UsersFeatures.Queries.SearchByProject;

namespace AgileControl.Client.Interfaces;

public interface IUserService
{
    Task<SearchUserByNameResponse> SearchByName(string name, int? limit);

    Task<SearchByProjectResponse> SearchByNameUserByProject(string name, int? limit, Guid projectId);
}