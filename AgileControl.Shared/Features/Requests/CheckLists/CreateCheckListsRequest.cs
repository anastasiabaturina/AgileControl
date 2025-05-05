namespace AgileControl.Shared.Features.Requests.CheckLists;

public class CreateCheckListsRequest
{
    public Guid TaskId { get; set; }

    public List<CheckListRequest> CheckLists { get; set; }
}