namespace AgileControl.Shared.Features.Requests.Tasks;

public class CheckListRequest
{
    public string Text { get; set; } = default!;

    public DateTime? CompletedDate { get; set; }
}