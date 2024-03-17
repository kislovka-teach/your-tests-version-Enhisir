using Api.Dtos;
using Api.Models;
using AutoMapper;

namespace Api;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<GameDto, Game>();
        CreateMap<GameNoteDto, GameNote>();
        CreateMap<CompanyDto, Company>();
    }
}