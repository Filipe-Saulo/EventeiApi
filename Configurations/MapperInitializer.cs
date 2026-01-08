using AutoMapper;

namespace EventeiApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            //CreateMap<LoginDto, User>().ReverseMap();
            //CreateMap<CreateUserDto, User>().ReverseMap();

            //CreateMap<EventoDto, Event>().ReverseMap();
            //CreateMap<CreateEventoDto, Event>()
            //    .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))
            //    .ReverseMap();


            //CreateMap<PhotoDto, Photo>().ReverseMap();
            //CreateMap<CreatePhotoDto, Photo>().ReverseMap();


        }
    }
}
