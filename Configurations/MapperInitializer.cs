using Api.Models.Data;
using Api.Models.Dto;
using Api.Models.Dto.WebUserLogin;
using AutoMapper;
using Eventei_Api.Models.Data;
using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Collections.Specialized.BitVector32;

namespace Api.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<WebCreateUserLoginDto, User>().ReverseMap();

            CreateMap<EventoDto, Evento>().ReverseMap();
            CreateMap<CreateEventoDto, Evento>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))
                .ReverseMap();


            CreateMap<PhotoDto, Photo>().ReverseMap();
            CreateMap<CreatePhotoDto, Photo>().ReverseMap();


        }
    }
}
