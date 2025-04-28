namespace AgileControl.Applicaion.Models.Dtos;

public record TaskDto(
    Guid Id,
    string Title,
    string Description,
    Domain.Enums.TaskStatus Status,
    DateTime CreatedDate,
    Guid AssigneeId);