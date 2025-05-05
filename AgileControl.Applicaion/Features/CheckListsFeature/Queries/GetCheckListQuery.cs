using MediatR;

namespace AgileControl.Applicaion.Features.CheckListsFeature.Queries;

public class GetCheckListQuery : IRequest<GetCheckListResponse>
{
    public Guid TaskId { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}