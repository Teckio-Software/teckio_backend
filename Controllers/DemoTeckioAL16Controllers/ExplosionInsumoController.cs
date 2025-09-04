using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de Explosion de Insumos que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/explosioninsumos/16")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionInsumo-Empresa1")]
    public class ExplosionInsumoDemoTeckioAL16Controller : ControllerBase
    {
        private readonly IExplosionInsumoService _ExplosionInsumoService;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<ExplosionInsumoDemoTeckioAL16Controller> _Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly DemoTeckioAL16Context Context;
        /// <summary>
        /// Constructor del controlador de EspecialIdad
        /// </summary>
        /// <param name="Logger">Para mostrar errores en consola</param>
        /// <param name="Context">Para mandar información de los registros</param>
        public ExplosionInsumoDemoTeckioAL16Controller(
            ILogger<ExplosionInsumoDemoTeckioAL16Controller> Logger,
            DemoTeckioAL16Context Context
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
