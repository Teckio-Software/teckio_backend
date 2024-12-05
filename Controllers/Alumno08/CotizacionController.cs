

using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;







namespace ERP_TECKIO
{
    [Route("api/cotizacion/8")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CotizacionAlumno08Controller : ControllerBase
    {
        private readonly ICotizacionService<Alumno08Context> _Service;
        private readonly CotizacionProceso<Alumno08Context> _Proceso;
        private readonly IOrdenCompraService<Alumno08Context> _OrdenCompraService;
        private readonly ITipoImpuestoService<Alumno08Context> _TipoImpuestoService;
        private readonly ILogger<CotizacionAlumno08Controller> _Logger;
        private readonly Alumno08Context Context;
        public CotizacionAlumno08Controller(

            ILogger<CotizacionAlumno08Controller> Logger
            , Alumno08Context Context
            , ICotizacionService<Alumno08Context> Service
            , IOrdenCompraService<Alumno08Context> OrdenCompraService,
              ITipoImpuestoService<Alumno08Context> TipoImpuestoService
            , CotizacionProceso<Alumno08Context> Proceso)
        {
            _Logger = Logger;
            this.Context = Context;
            _Service = Service;
            _OrdenCompraService = OrdenCompraService;
            _TipoImpuestoService = TipoImpuestoService;
            _Proceso = Proceso;
        }

        [HttpPost]
        [Route("CrearCotizacion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> CrearCotizacion([FromBody] CotizacionCreacionDTO parametro)
        {
            return await _Proceso.CrearCotizacion(parametro);
        }

        [HttpPut]
        [Route("EditarCotizacion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> EditarCotizacion([FromBody] CotizacionDTO parametro)
        {
            return await _Service.Editar(parametro);
        }

        [HttpGet]
        [Route("AutorizarTodos/{idCotizacion:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> AutorizarTodos(int idCotizacion)
        {
            var authen = HttpContext.User;
            return await _Proceso.AutorizarTodos(idCotizacion, authen.Claims.ToList());
        }

        [HttpPut]
        [Route("ActualizarInsumosCotizados")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> ActualizarInsumosCotizados(CotizacionObjetoRequisicionDTO cotizacion)
        {
            return await _Proceso.ActualizarInsumosCotizados(cotizacion);
        }

        [HttpGet]
        [Route("ObtenerImpuestos")]
        public async Task<ActionResult<List<TipoImpuestoDTO>>> ObtenerImpuestos()
        {
            return await _TipoImpuestoService.ObtenTodos();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearCotizacion-Empresa1")]
        public async Task<ActionResult> Post([FromBody] CotizacionCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno08Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarCotizacion-Empresa1")]
        public async Task<ActionResult> Put([FromBody] CotizacionDTO parametros)
        {
            try
            {
                var resultado = await _Service.Editar(parametros);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno08Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("autorizar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "AutorizarCotizacion-Empresa1")]
        public async Task<ActionResult> PutAutorizar(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusAutorizar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno08Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("cancelar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarCotizacion-Empresa1")]
        public async Task<ActionResult> PutCancelar(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusCancelar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno08Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("remover/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "RemoverAutorizacionCotizacion-Empresa1")]
        public async Task<ActionResult> PutRemover(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusRemoverAutorizar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno08Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpGet("todos/{IdProyecto:int}")]
        public async Task<ActionResult<List<CotizacionDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdProyecto)
        {
            var lista = await _Service.ObtenXIdProyecto(IdProyecto);
            var queryable = lista.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var listaResult = queryable.OrderBy(z => z.IdRequisicion).OrderBy(z => z.NoCotizacion).Paginar(paginacionDTO).ToList();
            if (listaResult.Count <= 0)
            {
                return NoContent();
            }
            return listaResult;
        }

        [HttpGet("todosrequisicion/{IdProyecto:int}/{IdRequisicion:int}")]
        public async Task<ActionResult<List<CotizacionDTO>>> obtenCotizacionesXIdRequisicion([FromQuery] PaginacionDTO paginacionDTO, int IdProyecto, int IdRequisicion)
        {
            var lista = await _Service.ObtenXIdProyecto(IdProyecto);
            var queryable = lista.AsQueryable();
            var listaResult = queryable.Where(z => z.IdRequisicion == IdRequisicion).OrderBy(z => z.NoCotizacion).Paginar(paginacionDTO).ToList();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(lista.AsQueryable());
            if (listaResult.Count <= 0)
            {
                return NoContent();
            }
            return listaResult;
        }

        [HttpGet("sinpaginar/{IdProyecto:int}")]
        public async Task<ActionResult<List<CotizacionDTO>>> Get(int IdProyecto)
        {
            var lista = await _Service.ObtenXIdProyecto(IdProyecto);
            var queryable = lista.AsQueryable();
            var listaResult = queryable.OrderBy(z => z.IdRequisicion).OrderBy(z => z.NoCotizacion).ToList();
            if (lista.Count <= 0)
            {
                return NoContent();
            }
            return listaResult;
        }

        [HttpGet("ObtenXIdRequisicion/{idRequisicion:int}")]
        public async Task<ActionResult<List<CotizacionDTO>>> ObtenXIdRequisicion(int idRequisicion)
        {
            var lista = await _Proceso.CotizacionesXIdRequisicion(idRequisicion);
            return lista;
        }



        [HttpGet("ObtenerXId/{id:int}")]
        public async Task<ActionResult<CotizacionDTO>> GetXId(int Id)
        {
            var lista = await _Service.ObtenXId(Id);
            return lista;
        }



    }
}
