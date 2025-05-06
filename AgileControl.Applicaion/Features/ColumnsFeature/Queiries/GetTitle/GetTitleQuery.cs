using MediatR;

namespace AgileControl.Applicaion.Features.Column.Queiries.GetTitle;

public class GetTitleQuery : IRequest<GetTitleResponse>
{
    public Guid ProjectId { get; set; }
}