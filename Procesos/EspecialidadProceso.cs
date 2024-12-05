using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class EspecialidadProceso<TContext> where TContext : DbContext
    {
        private readonly IEspecialidadService<TContext> _Service;
        public EspecialidadProceso(
            IEspecialidadService<TContext> service)
        {
            _Service = service;
        }

        public async Task Post([FromBody] EspecialidadDTO parametros)
        {
            try
            {
                var resultado = await _Service.Crear(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task Put([FromBody] EspecialidadDTO parametros)
        {
            try
            {
                var resultado = await _Service.Editar(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task Delete(int Id)
        {
            try
            {
                var resultado = await _Service.Eliminar(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task<ActionResult<List<EspecialidadDTO>>> Get()
        {
            var query = _Service.ObtenerTodos().Result.AsQueryable();
            var lista = query.OrderBy(x => x.Id).ToList();
            if (lista.Count <= 0)
            {
                return new List<EspecialidadDTO>();
            }
            return lista;
        }

        public async Task<ActionResult<EspecialidadDTO>> GetXId(int Id)
        {
            var objeto = await _Service.ObtenXId(Id);
            if (objeto.Id <= 0)
            {
                return new EspecialidadDTO();
            }
            return objeto;
        }
    }
}
