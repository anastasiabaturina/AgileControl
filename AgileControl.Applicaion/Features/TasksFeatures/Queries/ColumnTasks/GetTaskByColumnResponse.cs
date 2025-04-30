using AgileControl.Applicaion.Models.Dtos;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.Status;

public class GetTaskByColumnResponse
{
    public List<TaskDto> Tasks { get; set; }
}