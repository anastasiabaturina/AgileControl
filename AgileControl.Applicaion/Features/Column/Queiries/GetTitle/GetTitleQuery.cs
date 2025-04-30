using MediatR;

namespace AgileControl.Applicaion.Features.Column.Queiries.GetTitle;

public class GetTitleQuery : IRequest<GetTitleResponse>
{
    public Guid ColumnId { get; set; }
}