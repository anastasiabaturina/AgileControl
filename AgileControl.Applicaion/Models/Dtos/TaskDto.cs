using AgileControl.Domain.Enums;
using AgileControl.Shared.Features.Requests.Tasks;

namespace AgileControl.Applicaion.Models.Dtos;

public record TaskDto(
    Guid Id,
    string Title,
    string Description,
    Status Status);
    //DateTime CreatedDate,
    //Guid AssigneeId);