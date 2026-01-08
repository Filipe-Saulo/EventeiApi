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
            CreateMap<CreateUserDto, User>().ReverseMap();

            CreateMap<EventoDto, Event>().ReverseMap();
            CreateMap<CreateEventoDto, Event>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))
                .ReverseMap();


            CreateMap<PhotoDto, Photo>().ReverseMap();
            CreateMap<CreatePhotoDto, Photo>().ReverseMap();


        }
    }
}
