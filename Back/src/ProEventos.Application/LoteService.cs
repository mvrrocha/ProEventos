using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;

        public LoteService(IGeralPersist geralPersist, ILotePersist lotePersist, IMapper mapper)
        {
            this._geralPersist = geralPersist;
            this._lotePersist = lotePersist;
            this._mapper = mapper;
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) return null;
            var resultado = _mapper.Map<LoteDto>(lote);
            return resultado;
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
            if (lotes == null) return null;
            var resultado = _mapper.Map<LoteDto[]>(lotes);
            return resultado;
        }

        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;
                _geralPersist.Add<Lote>(lote);
                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
            if (lotes == null) return null;

            foreach (var model in models)
            {
                if (model.Id == 0)
                {
                    await AddLote(eventoId, model);
                }
                else
                {
                    var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                    model.EventoId = eventoId;
                    _mapper.Map(model, lote);
                    _geralPersist.Update<Lote>(lote);
                    await _geralPersist.SaveChangesAsync();
                }
            }

            var loteRetorno = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
            return _mapper.Map<LoteDto[]>(loteRetorno);
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote não encontrado para exclusão!");
                _geralPersist.Delete<Lote>(lote);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}