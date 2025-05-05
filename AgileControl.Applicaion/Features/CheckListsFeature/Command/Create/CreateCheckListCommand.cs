using AgileControl.Applicaion.Models.Dtos;
using MediatR;

namespace AgileControl.Applicaion.Features.CheckListsFeature.Command.Create;

public class CreateCheckListCommand : IRequest<CreateCheckListResponse>
{
    public Guid TaskId { get; set; }

    public List<CheckListDto> CheckLists { get; set; }
}