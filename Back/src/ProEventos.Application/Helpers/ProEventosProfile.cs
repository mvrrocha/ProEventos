using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap(); // ReverseMap faz o mapeamento nos dois sentidos
            CreateMap<Lote, LoteDto>().ReverseMap(); // ReverseMap faz o mapeamento nos dois sentidos
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap(); // ReverseMap faz o mapeamento nos dois sentidos
            CreateMap<Palestrante, PalestranteDto>().ReverseMap(); // ReverseMap faz o mapeamento nos dois sentidos
            CreateMap<User, UserDto>().ReverseMap(); // ReverseMap faz o mapeamento nos dois sentidos
            CreateMap<User, UserLoginDto>().ReverseMap(); // ReverseMap faz o mapeamento nos dois sentidos
            CreateMap<User, UserUpdateDto>().ReverseMap(); // ReverseMap faz o mapeamento nos dois sentidos
        }
    }
}