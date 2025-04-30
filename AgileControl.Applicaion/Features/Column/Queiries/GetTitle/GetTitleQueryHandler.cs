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
        var title = await _context.Columns
            .FirstOrDefaultAsync(c => c.Id == query.ColumnId, cancellationToken);

        return new GetTitleResponse
        {
            Title = title.Title
        };
    }
}
