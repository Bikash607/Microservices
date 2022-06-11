using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PratformProfile: Profile
{
    public PratformProfile()
    {
        CreateMap<Platform, PlatformReadDto>().ReverseMap();
        CreateMap<PlatformCreateDto, Platform>().ReverseMap();
    }
}