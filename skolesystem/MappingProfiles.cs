using AutoMapper;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Repository;
using skolesystem.Service;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserCreateDto, Users>();
        CreateMap<Users, UserReadDto>();
        CreateMap<UserUpdateDto, Users>();
        CreateMap<Users, UserUpdateDto>();
        CreateMap<UsersService, UsersRepository>();
        CreateMap<UsersRepository, UsersService>();

    }
}