using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class TipoInsumoProceso<TContext> where TContext : DbContext
    {
        private readonly ITipoInsumoService<TContext> _TipoInsumoService;
        
        public TipoInsumoProceso(
            ITipoInsumoService<TContext> tipoInsumoService
            )
        {
            _TipoInsumoService = tipoInsumoService;
        }

        public async Task Post([FromBody] TipoInsumoCreacionDTO parametros)
        {
            try
            {
                var resultado = await _TipoInsumoService.Crear(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task Put([FromBody] TipoInsumoDTO parametros)
        {
            //var lista = TipoInsumoSP.obtenTipoInsumoXIdAsync(parametros.Id);
            //if (lista == null)
            //{
            //    return NotFound();
            //}
            //await TipoInsumoSP.editaTipoInsumoAsync(parametros);
            try
            {
                var resultado = await _TipoInsumoService.Editar(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task Delete(int Id)
        {
            //var lista = TipoInsumoSP.obtenTipoInsumoXIdAsync(Id);
            //if (lista == null)
            //{
            //    return NotFound();
            //}
            //await TipoInsumoSP.eliminaTipoInsumoAsync(Id);
            try
            {
                var resultado = await _TipoInsumoService.Eliminar(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task<ActionResult<List<TipoInsumoDTO>>> Get()
        {
            //var queryable = TipoInsumoSP.obtenTiposInsumoAsync().Result.AsQueryable();
            var query = _TipoInsumoService.Lista().Result.AsQueryable();
            var lista = query.OrderBy(x => x.Descripcion).ToList();
            if (lista.Count <= 0) { return new List<TipoInsumoDTO>(); }
            return lista;
        }

        public async Task<ActionResult<List<TipoInsumoDTO>>> GetSinPaginar()
        {
            //var queryable = TipoInsumoSP.obtenTiposInsumoAsync().Result.AsQueryable();
            var lista = await _TipoInsumoService.Lista();
            if (lista.Count <= 0) { return new List<TipoInsumoDTO>(); }
            return lista;
        }

        public async Task<ActionResult<List<TipoInsumoDTO>>> TipoInsumosParaRequisitar()
        {
            //var queryable = TipoInsumoSP.obtenTiposInsumoAsync().Result.AsQueryable();
            var lista = await _TipoInsumoService.Lista();
            var tiposInsumos = lista.Where(z => z.Id != 3 && z.Id != 10000 && z.Id != 10001).ToList();
            if (lista.Count <= 0 || tiposInsumos.Count() <= 0) { return new List<TipoInsumoDTO>(); }
            return tiposInsumos;
        }

        public async Task<ActionResult<TipoInsumoDTO>> GetXId(int Id)
        {
            var objeto = await _TipoInsumoService.ObtenXId(Id);
            if (objeto.Id <= 0)
            {
                return new TipoInsumoDTO();
            }
            return objeto;
        }
    }
}