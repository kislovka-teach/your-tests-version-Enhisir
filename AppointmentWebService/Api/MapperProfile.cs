using Api.Dtos;
using Api.Models;
using Api.Models.Enums;
using AutoMapper;

namespace Api;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterPatientDto, Patient>()
            .ForMember(
                dest => dest.PasswordHashed,
                opt 
                    => opt.MapFrom(src => src.Password));
        
        CreateMap<RegisterDoctorDto, Doctor>()
            .ForMember(
                dest => dest.PasswordHashed,
                opt 
                    => opt.MapFrom(src => src.Password));
        
        CreateMap<Specialization, SpecializationDto>().ReverseMap();
    }
}