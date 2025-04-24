using MediatR;

namespace AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;

public class GetInfoProjectIDQuery : IRequest<GetInfoGueryIDResponse>
{
    public Guid ProjectId { get; set; }
}