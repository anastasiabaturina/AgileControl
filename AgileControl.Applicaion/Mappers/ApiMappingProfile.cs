using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;
using AgileControl.Applicaion.Features.TasksFeatures.Commands.Create;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Login;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Register;
using AgileControl.Applicaion.Models.Dtos;
using AgileControl.Domain.Entities;
using AgileControl.Shared.Features.Requests.Auth;
using AgileControl.Shared.Features.Requests.Projects;
using AgileControl.Shared.Features.Requests.Tasks;
using AutoMapper;
using TaskStatus = AgileControl.Domain.Enums.TaskStatus;

namespace AgileControl.Applicaion.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<LoginUserRequest, LoginUserCommand>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<CreateProjectRequest, CreateProjectCommand>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.ProjectMembers, opt => opt.MapFrom(src => src.ProjectMembers));

        CreateMap<ProjectMemberRequest, ProjectMemberDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ProjectRole, opt => opt.MapFrom(src => src.ProjectRole));

        CreateMap<CreateProjectCommand, Project>()
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.CreaterId))
           .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src =>
                src.EndDate.HasValue
                    ? DateTime.SpecifyKind(src.EndDate.Value, DateTimeKind.Utc)
                    : (DateTime?)null))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Guid, GetInfoProjectIDQuery>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src));

        CreateMap<CreateTaskRequest, CreateTaskCommand>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))  
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)) 

            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))  
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))  
            .ForMember(dest => dest.CheckLists, opt => opt.MapFrom(src => src.CheckLists)) 
             .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src =>
                src.EndDate.HasValue
                    ? DateTime.SpecifyKind(src.EndDate.Value, DateTimeKind.Utc)
                    : (DateTime?)null));

        CreateMap<CreateTaskCommand, ProjectTask>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))  
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src =>
                src.EndDate.HasValue
                    ? DateTime.SpecifyKind(src.EndDate.Value, DateTimeKind.Utc)
                    : (DateTime?)null))
            .ForMember(dest => dest.IdUserCreated, opt => opt.MapFrom(src => src.IdUserCreated))
            .ForMember(dest => dest.AssigneeId, opt => opt.MapFrom(src => src.AssigneeId))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))  
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TaskStatus.Backlog))
            .ForMember(dest => dest.CheckList, opt => opt.MapFrom(src => src.CheckLists));

        CreateMap<CheckListRequest, CheckListDto>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text)) 
            .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate));
    }
}