using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        public readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this._eventoPersist = eventoPersist;
            this._geralPersist = geralPersist;            
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);

                if (await _geralPersist.SaveChangesAsync())
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao adicionar o evento! " + ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                Evento evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);

                if (evento == null)
                    return null;

                model.Id = evento.Id;

                _geralPersist.Update(model);

                if (await _geralPersist.SaveChangesAsync())
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao atualizar o evento! " + ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                Evento evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);

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

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);   

                if (eventos == null)
                    return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao buscar os eventos! " + ex.Message);
            }            
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);   

                if (eventos == null)
                    return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao buscar os eventos! " + ex.Message);
            } 
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);   

                if (evento== null)
                    return null;
                
                return evento;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao busca os evento! " + ex.Message);
            } 
        }        
    }
}