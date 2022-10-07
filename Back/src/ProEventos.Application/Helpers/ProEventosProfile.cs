using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;

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
        }
    }
}