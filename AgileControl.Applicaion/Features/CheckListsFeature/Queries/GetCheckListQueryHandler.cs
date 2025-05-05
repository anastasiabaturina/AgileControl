using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.CheckListsFeature.Queries;

public class GetCheckListQueryHandler : IRequestHandler<GetCheckListQuery, GetCheckListResponse>
{
    private readonly ApplicationDbContext _context;


    public GetCheckListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetCheckListResponse> Handle(GetCheckListQuery query, CancellationToken cancellationToken)
    {
        var checkLists = await _context.CheckLists
        .Where(c => c.TaskId == query.TaskId)
        .Skip((query.PageNumber - 1) * query.PageSize)
        .Take(query.PageSize)
        .Select(c => new CheckListDto
        {
            Id = c.Id,
            Text = c.Text,
            IsCompleted = c.IsCompleted,
            CreatedDate = c.CreatedDate,
        })
        .ToListAsync(cancellationToken);

        return new GetCheckListResponse
        {
            CheckLists = checkLists
        };
    }
}