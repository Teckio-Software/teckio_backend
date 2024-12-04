using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class FamiliaInsumoProceso<TContext> where TContext : DbContext
    {
        private readonly IFamiliaInsumoService<TContext> _FamiliaInsumoService;

        public FamiliaInsumoProceso(
            IFamiliaInsumoService<TContext> familiaInsumoService
            )
        {
            _FamiliaInsumoService = familiaInsumoService;
        }

        public async Task Post([FromBody] FamiliaInsumoCreacionDTO parametros)
        {
            try
            {
                var resultado = await _FamiliaInsumoService.Crear(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task Put([FromBody] FamiliaInsumoDTO parametros)
        {
            //var lista = TipoInsumoSP.obtenTipoInsumoXIdAsync(parametros.Id);
            //if (lista == null)
            //{
            //    return NotFound();
            //}
            //await TipoInsumoSP.editaTipoInsumoAsync(parametros);
            try
            {
                var resultado = await _FamiliaInsumoService.Editar(parametros);
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
                var resultado = await _FamiliaInsumoService.Eliminar(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task<ActionResult<List<FamiliaInsumoDTO>>> Get()
        {
            //var queryable = TipoInsumoSP.obtenTiposInsumoAsync().Result.AsQueryable();
            var query = _FamiliaInsumoService.Lista().Result.AsQueryable();
            var lista = query.OrderBy(x => x.Descripcion).ToList();
            if (lista.Count <= 0) { return new List<FamiliaInsumoDTO>(); }
            return lista;
        }

        public async Task<ActionResult<List<FamiliaInsumoDTO>>> GetSinPaginar()
        {
            //var queryable = TipoInsumoSP.obtenTiposInsumoAsync().Result.AsQueryable();
            var lista = await _FamiliaInsumoService.Lista();
            if (lista.Count <= 0) { return new List<FamiliaInsumoDTO>(); }
            return lista;
        }

        public async Task<ActionResult<FamiliaInsumoDTO>> GetXId(int Id)
        {
            var objeto = await _FamiliaInsumoService.ObtenXId(Id);
            if (objeto.Id <= 0)
            {
                return new FamiliaInsumoDTO();
            }
            return objeto;
        }
    }
}