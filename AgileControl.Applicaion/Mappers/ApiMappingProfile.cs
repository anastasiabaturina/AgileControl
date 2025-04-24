using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using AgileControl.Applicaion.Features.ProjectsFeatures.Queries.GetInfoProgect;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Login;
using AgileControl.Applicaion.Features.UsersFeatures.Command.Register;
using AgileControl.Domain.Entities;
using AgileControl.Shared.Features.Requests.Auth;
using AgileControl.Shared.Features.Requests.Projects;
using AutoMapper;

namespace AgileControl.Applicaion.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<LoginUserRequest, LoginUserCommand>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<CreateProjectRequest, CreateProjectCommand>()
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

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
    }
}
