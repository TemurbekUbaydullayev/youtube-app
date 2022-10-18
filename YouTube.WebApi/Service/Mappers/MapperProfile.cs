using AutoMapper;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.DTOs.Users;
using YouTube.WebApi.Service.DTOs.Videos;

namespace YouTube.WebApi.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserForCreationDto, User>().ReverseMap();
        CreateMap<User, UserForViewDto>().ReverseMap();
        CreateMap<IEnumerable<UserForViewDto>, IQueryable<User>>();

        CreateMap<VideoForCreationDto, Video>().ReverseMap();
        CreateMap<Video, VideoForViewDto>().ReverseMap();
    }
}
