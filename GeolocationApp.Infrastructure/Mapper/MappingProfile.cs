using AutoMapper;
using GeolocationApp.Domain.DTOs;
using GeolocationApp.Domain.Entities;

namespace GeolocationApp.Infrastructure.Mapper;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateVisit, Visit>().ReverseMap();
        CreateMap<ResponseVisit, Visit>().ReverseMap();
    }
}