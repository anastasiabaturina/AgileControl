using AgileControl.Applicaion.Models.Dtos;

namespace AgileControl.Applicaion.Features.TasksFeatures.Queries.BacklogProject;

public class GetTasksByProjectResponse
{
    public List<TaskDto> Tasks { get; set; }
}