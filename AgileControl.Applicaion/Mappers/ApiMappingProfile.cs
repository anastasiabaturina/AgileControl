using AgileControl.Applicaion.Features.ProjectsFeatures.Commands.Create;
using AgileControl.Domain.Entities;
using AutoMapper;

namespace AgileControl.Applicaion.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<CreateProjectCommand, Project>()
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));
    }
}
