


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;



namespace SistemaERP.API.Alumno41Controllers.Procomi
{
    /// <summary>
    /// Controlador de Explosion de Insumos que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/explosioninsumos/41")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionInsumo-Empresa35")]
    public class ExplosionInsumoAlumno41Controller : ControllerBase
    {
        private readonly IExplosionInsumoService _ExplosionInsumoService;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<ExplosionInsumoAlumno41Controller> _Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno41Context Context;
        /// <summary>
        /// Constructor del controlador de EspecialIdad
        /// </summary>
        /// <param name="Logger">Para mostrar errores en consola</param>
        /// <param name="Context">Para mandar información de los registros</param>
        public ExplosionInsumoAlumno41Controller(
            ILogger<ExplosionInsumoAlumno41Controller> Logger,
            Alumno41Context Context
            , IExplosionInsumoService ExplosionInsumoService
            )
        {
            _Logger = Logger;
            this.Context = Context;
            _ExplosionInsumoService = ExplosionInsumoService;
        }

        /// <summary>
        /// Endpoint del tipo Get para llamar al metodo que obtiene todos los registros de la tabla
        /// de Explosion de Insumos que pertenezcan al proyecto seleccionado y paginados
        /// </summary>
        /// <param name="paginacionDTO">Parametros de paginacion</param>
        /// <param name="IdProyecto">Identificador del proyecto</param>
        /// <returns>Registros paginados</returns>
        //[HttpGet("todos/{IdProyecto:int}")]
        //public async Task<ActionResult<List<ExplosionInsumoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdProyecto)
        //{
        //    var queryable = await _ExplosionInsumoService.ObtenXIdProyecto(IdProyecto);
        //    await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable.AsQueryable());
        //    var lista = queryable.AsQueryable().OrderBy(z => z.Codigo).Paginar(paginacionDTO).ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}
    }
}
