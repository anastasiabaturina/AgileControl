namespace AgileControl.Applicaion.Models.Dtos;

public class CheckListDto
{
    public string Text { get; set; } = default!;

    public DateTime? CompletedDate { get; set; }
}
