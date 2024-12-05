using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class ConceptoProceso<TContext> where TContext : DbContext
    {
        private readonly IConceptoService<TContext> _Service;
        private readonly IEspecialidadService<TContext> _EspecialidadService;

        public ConceptoProceso(
            IConceptoService<TContext> service
            , IEspecialidadService<TContext> especialidadService)
        {
            _Service = service;
            _EspecialidadService = especialidadService;
        }

        public async Task Post([FromBody] ConceptoDTO parametros)
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

        public async Task Put([FromBody] ConceptoDTO parametros)
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

        public async Task<ActionResult<List<ConceptoDTO>>> Get(int IdProyecto)
        {
            var query = _Service.ObtenerTodos(IdProyecto).Result.AsQueryable();
            var lista = query.OrderBy(x => x.Id).ToList();
            if (lista.Count <= 0)
            {
                return new List<ConceptoDTO>();
            }
            var especialidades = _EspecialidadService.ObtenerTodos().Result.AsQueryable();
            for (int i = 0; i < lista.Count; i++)
            {
                var especialidad = especialidades.Where(z => z.Id == lista[i].IdEspecialidad).FirstOrDefault();
                if(especialidad != null)
                {
                    lista[i].DescripcionEspecialidad = especialidad.Descripcion;
                }
            }
            return lista;
        }

        public async Task<ActionResult<ConceptoDTO>> GetXId(int Id)
        {
            var objeto = await _Service.ObtenXId(Id);
            if (objeto.Id <= 0)
            {
                return new ConceptoDTO();
            }
            return objeto;
        }
    }
}
