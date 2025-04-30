using AgileControl.Domain.Enums;

namespace AgileControl.Applicaion.Models.Dtos;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EndDate { get; set; }

    public UserDto UserCreated { get; set; }

    public UserDto Assignee { get; set; }

    public Priority Priority { get; set; }

    public List<CheckListDto>? CheckList { get; set; }
}