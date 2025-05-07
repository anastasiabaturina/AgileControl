using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Domain.Entities;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.UsersFeatures.Queries.SearchByProject;

public class SearchByProjectQueryHandler : IRequestHandler<SearchByProjectQuery, SearchByProjectResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public SearchByProjectQueryHandler(UserManager<User> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<SearchByProjectResponse> Handle(SearchByProjectQuery query, CancellationToken cancellationToken)
    {
        var searchTerm = query.SearchName?.Trim() ?? string.Empty;

        var usersQuery = _context.ProjectMembers
        .AsNoTracking()
        .Include(p => p.User)
        .Where(p => p.PojectId == query.ProjectId)
        .Where(p =>
            p.User.Email.Contains(searchTerm) ||
            p.User.FirstName.Contains(searchTerm) ||
            p.User.LastName.Contains(searchTerm));

        if (query.Limit > 0)
        {
            usersQuery = usersQuery.Take(query.Limit);
        }

        var users = await usersQuery.ToListAsync(cancellationToken);

        var response = new SearchByProjectResponse
        {
            Users = users.Select(u => new UserDto
            {
                Id = u.UserId,
                UserName = $"{u.User.FirstName} {u.User.LastName}",
                Email = u.User.Email
            }).ToList()
        };

        return await Task.FromResult(response);
    }
}