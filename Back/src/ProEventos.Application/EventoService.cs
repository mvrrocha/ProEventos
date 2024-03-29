using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Models;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist, IMapper mapper)
        {
            this._geralPersist = geralPersist;  
            this._eventoPersist = eventoPersist;            
            this._mapper = mapper;          
        }
        public async Task<EventoDto> AddEventos(int userId, EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                evento.UserId = userId;

                _geralPersist.Add<Evento>(evento);

                if (await _geralPersist.SaveChangesAsync()) {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }                    

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao adicionar o evento! " + ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
        {
            try
            {
                Evento evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);

                if (evento == null)
                    return null;

                model.Id = evento.Id; // atribuição do Id porque o model vem sem a informação do Id
                model.UserId = userId;

                _mapper.Map(model, evento); // Mapeia o model para o tipo evento

                _geralPersist.Update<Evento>(evento);

                if (await _geralPersist.SaveChangesAsync()) {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao atualizar o evento! " + ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                Evento evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);

                if (evento == null)
                    throw new Exception("Evento não encontrado!");

                _geralPersist.Delete<Evento>(evento);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao deletar o evento! " + ex.Message);
            }
        }

        public async Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(userId, pageParams);   

                if (eventos == null)
                    return null;
                
                var resultado = _mapper.Map<PageList<EventoDto>>(eventos);

                resultado.CurrentPage = eventos.CurrentPage;
                resultado.TotalPages = eventos.TotalPages;
                resultado.TotalCount = eventos.TotalCount;
                resultado.PageSize = eventos.PageSize;
                
                return resultado;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao buscar os eventos! " + ex.Message);
            }            
        }        

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);   

                if (evento== null)
                    return null;

                var resultado = _mapper.Map<EventoDto>(evento);
                
                return resultado;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao busca os evento! " + ex.Message);
            } 
        }        
    }
}