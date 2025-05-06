using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Features.Column.Queiries.GetTitle;

public class GetTitleQueryHandler : IRequestHandler<GetTitleQuery, GetTitleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetTitleQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetTitleResponse> Handle(GetTitleQuery query, CancellationToken cancellationToken)
    {
        var columns = await _context.Columns
            .Where(c => c.ProjectId == query.ProjectId)
            .Select(t => new ColumnDto
            {
                ColumnName = t.Title,
                ColumnId = t.Id
            })
            .ToListAsync(cancellationToken);

        return new GetTitleResponse
        {
            Columns = columns
        };
    }
}
