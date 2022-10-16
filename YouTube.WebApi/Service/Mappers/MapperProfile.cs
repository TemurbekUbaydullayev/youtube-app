using AutoMapper;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.DTOs.Users;

namespace YouTube.WebApi.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserForCreationDto, User>();
        CreateMap<User, UserForViewDto>();
    }
}
