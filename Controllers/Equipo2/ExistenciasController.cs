


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

using ERP_TECKIO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;




namespace SistemaERP.API.Alumno40Controllers.Procomi
{
    /// <summary>
    /// Controlador para las Requisiciones a las que accede el usuario
    /// </summary>
    [Route("api/existencias/40")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionEspecialidad-Empresa1")]
    public class ExistenciasAlumno40Controller : ControllerBase
    {
        private readonly IAlmacenExistenciaInsumoService<Alumno40Context> _ExistenciaService;
        private readonly ExistenciasProceso<Alumno40Context> _Proceso;
        /// <summary>
        /// Se usa para mostrar errores en la consola
        /// </summary>
        private readonly ILogger<ExistenciasAlumno40Controller> _Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno40Context Context;
        /// <summary>
        /// Constructor del controlador de Requisiciones
        /// </summary>
        /// <param name="Logger"></param>
        /// <param name="Context"></param>
        public ExistenciasAlumno40Controller(
            ILogger<ExistenciasAlumno40Controller> Logger,
            Alumno40Context Context
            , IAlmacenExistenciaInsumoService<Alumno40Context> ExistenciaService
            , ExistenciasProceso<Alumno40Context> Proceso
            )
        {
            _Logger = Logger;
            this.Context = Context;
            _ExistenciaService = ExistenciaService;
            _Proceso = Proceso;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginacionDTO"></param>
        /// <returns></returns>
        [HttpGet("todos")]
        public async Task<ActionResult<List<AlmacenExistenciaInsumoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = await _ExistenciaService.ObtenTodos();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable.AsQueryable());
            var lista = queryable.AsQueryable().Paginar(paginacionDTO).ToList();
            return lista;
        }

        [HttpGet("obtenInsumosExistentes/{idAlmacen:int}")]
        public async Task<ActionResult<List<AlmacenExistenciaInsumoDTO>>> obtenInsumosExistentes(int IdAlmacen)
        {
            var lista = await _Proceso.obtenInsumosExistentes(IdAlmacen);
            return lista;
        }

        [HttpGet("obtenDetallesInsumosExistentes/{idAlmacen:int}/{idInsumo:int}")]
        public async Task<ActionResult<List<AlmacenExistenciaInsumoDTO>>> obtenDetallesInsumosExistentes(int IdAlmacen, int IdInsumo)
        {
            var lista = await _Proceso.obtenDetallesInsumosExistentes(IdAlmacen, IdInsumo);
            return lista;
        }

        [HttpGet("existenciaYAlmacenDeInsumo/{idInsumo:int}/{idProyecto:int}")]
        public async Task<ActionResult<RespuestaDTO>> existenciaYAlmacenDeInsumo(int IdInsumo, int IdProyecto)
        {
            var respuesta = await _Proceso.existenciaYAlmacenDeInsumo(IdInsumo, IdProyecto);
            return respuesta;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("sinpaginar")]
        public async Task<ActionResult<List<AlmacenExistenciaInsumoDTO>>> Get()
        {
            var lista = await _ExistenciaService.ObtenTodos();
            return lista;
        }
        [HttpGet("sinpaginar/{IdAlmacen:int}")]
        public async Task<ActionResult<List<AlmacenExistenciaInsumoDTO>>> ObtenerPorAlmacen(int IdAlmacen)
        {
            var lista = await _ExistenciaService.ObtenXIdAlmacen(IdAlmacen);
            return lista;
        }
    }
}
