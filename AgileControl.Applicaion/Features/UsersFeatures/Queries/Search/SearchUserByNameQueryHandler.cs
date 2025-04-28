using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AgileControl.Applicaion.Features.UsersFeatures.Queries.Search;

public class SearchUserByNameQueryHandler : IRequestHandler<SearchUserByNameQuery, SearchUserByNameResponse>
{
    private readonly UserManager<User> _userManager;

    public SearchUserByNameQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Task<SearchUserByNameResponse> Handle(SearchUserByNameQuery query, CancellationToken cancellationToken)
    {
        var usersQuery = _userManager.Users
           .Where(u => u.FirstName.Contains(query.SearchName) ||
                       u.LastName.Contains(query.SearchName) ||
                       u.Email.Contains(query.SearchName));

        if (query.Limit > 0)
        {
            usersQuery = usersQuery.Take(query.Limit);
        }

        var users = usersQuery.ToList();

        var response =  new SearchUserByNameResponse
        {
            Users = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = $"{u.FirstName} {u.LastName}",
                Email = u.Email
            }).ToList()
        };

        return Task.FromResult(response);
    }
}