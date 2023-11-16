using AutoMapper;
using skolesystem.DTOs;
using skolesystem.Models;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserCreateDto, Users>();
        CreateMap<Users, UserReadDto>();
        CreateMap<UserUpdateDto, Users>();
        CreateMap<Users, UserUpdateDto>();
        
    }
}