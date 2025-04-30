namespace AgileControl.Applicaion.Models.Dtos;

public class CheckListDto
{
    public Guid Id { get; set; }

    public string Text { get; set; } = default!;

    public bool IsCompleted { get; set; }
}