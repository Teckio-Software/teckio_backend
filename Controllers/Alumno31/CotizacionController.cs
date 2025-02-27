using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/cotizacion/31")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CotizacionAlumno31Controller : ControllerBase
    {
        private readonly ICotizacionService<Alumno31Context> _Service;
        private readonly CotizacionProceso<Alumno31Context> _Proceso;
        private readonly IOrdenCompraService<Alumno31Context> _OrdenCompraService;
        private readonly ITipoImpuestoService<Alumno31Context> _TipoImpuestoService;
        private readonly ILogger<CotizacionAlumno31Controller> _Logger;
        private readonly Alumno31Context Context;
        public CotizacionAlumno31Controller(

            ILogger<CotizacionAlumno31Controller> Logger
            , Alumno31Context Context
            , ICotizacionService<Alumno31Context> Service
            , IOrdenCompraService<Alumno31Context> OrdenCompraService,
              ITipoImpuestoService<Alumno31Context> TipoImpuestoService
            , CotizacionProceso<Alumno31Context> Proceso)
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
        public async Task<ActionResult<List<TipoImpuestoDTO>>> ObtenerImpuestos() { 
            return await _TipoImpuestoService.ObtenTodos();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearCotizacion-Empresa1")]
        public async Task<ActionResult> Post([FromBody] CotizacionCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno31Context>(resultado.Estatus, resultado.Descripcion);
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno31Context>(resultado.Estatus, resultado.Descripcion);
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno31Context>(resultado.Estatus, resultado.Descripcion);
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno31Context>(resultado.Estatus, resultado.Descripcion);
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno31Context>(resultado.Estatus, resultado.Descripcion);
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
