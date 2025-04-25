using AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;
using MediatR;

namespace AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;

public class SearchUserByNameQuery : IRequest<SearchUserByNameResponse>
{
    public string SearchName { get; set; } = default!;

    public int Limit { get; set; } = 10;
}