using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;
using AgileControl.Shared.Features.Requests.Projects;

namespace AgileControl.Client.Interfaces;

public interface IProjectService
{
    Task<CreateProjectCommandResponse> CreateProjectAsync(CreateProjectRequest request);

    Task<GetInfoGueryIDResponse> GetProjectByIdAsync(Guid projectId);
}

