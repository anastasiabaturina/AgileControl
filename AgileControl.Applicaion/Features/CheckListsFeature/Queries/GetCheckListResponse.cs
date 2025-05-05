using AgileControl.Applicaion.Models.Dtos;

namespace AgileControl.Applicaion.Features.CheckListsFeature.Queries;

public class GetCheckListResponse
{
    public List<CheckListDto> CheckLists { get; set; }
}