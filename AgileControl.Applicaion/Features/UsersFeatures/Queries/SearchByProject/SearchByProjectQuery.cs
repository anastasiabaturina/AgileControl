using AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;
using MediatR;

namespace AgileControl.Applicaion.Features.UsersFeatures.Queries.SearchByProject;

public class SearchByProjectQuery : IRequest<SearchByProjectResponse>
{
    public string SearchName { get; set; } = default!;

    public int Limit { get; set; } = 10;

    public Guid ProjectId { get; set; }
}