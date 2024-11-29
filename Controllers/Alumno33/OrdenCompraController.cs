

using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;



namespace ERP_TECKIO
{
    [Route("api/ordencompra/33")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenCompraAlumno33Controller : ControllerBase
    {
        private readonly IOrdenCompraService<Alumno33Context> _Service;
        private readonly OrdenCompraProceso<Alumno33Context> _Proceso;
        private readonly ILogger<OrdenCompraAlumno33Controller> _Logger;
        private readonly Alumno33Context Context;
        public OrdenCompraAlumno33Controller(
            ILogger<OrdenCompraAlumno33Controller> Logger
            , Alumno33Context Context
            , IOrdenCompraService<Alumno33Context> Service
            , OrdenCompraProceso<Alumno33Context> Proceso)
        {
            _Logger = Logger;
            this.Context = Context;
            _Service = Service;
            _Proceso = Proceso;
        }
        [HttpPost]
        [Route("CrearOrdenCompra")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> CrearOrdenCompra([FromBody] OrdenCompraCreacionDTO parametro)
        {
            var authen = HttpContext.User;
            return await _Proceso.CrearOrdenCompra(parametro, authen.Claims.ToList());
        }

        [HttpPut]
        [Route("EditarOrdenCompra")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> EditarOrdenCompra([FromBody] OrdenCompraDTO parametro)
        {
            return await _Service.Editar(parametro);
        }

        [HttpPost("comprarcotizacion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearOrdenCompra-Empresa1")]
        public async Task<ActionResult> Post([FromBody] OrdenCompraCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno33Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }


        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarOrdenCompra-Empresa1")]
        public async Task<ActionResult> Put([FromBody] OrdenCompraDTO parametros)
        {
            try
            {
                var resultado = await _Service.Editar(parametros);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno33Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
        [HttpPut("cancelar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarOrdenCompra-Empresa1")]
        public async Task<ActionResult> PutCancelar(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusCancelar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno33Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
        [HttpGet("todos/{IdProyecto:int}")]
        public async Task<ActionResult<List<OrdenCompraDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdProyecto)
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

        [HttpGet("sinpaginar/{IdProyecto:int}")]
        public async Task<ActionResult<List<OrdenCompraDTO>>> Get(int IdProyecto)
        {
            var lista = await _Service.ObtenXIdProyecto(IdProyecto);
            var queryable = lista.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var listaResult = queryable.OrderBy(z => z.IdRequisicion).OrderBy(z => z.NoOrdenCompra).ToList();
            if (listaResult.Count <= 0)
            {
                return NoContent();
            }
            return listaResult;
        }

        [HttpGet("ObtenXIdRequisicion/{idRequisicion:int}")]
        public async Task<ActionResult<List<OrdenCompraDTO>>> ObtenXIdRequisicion(int idRequisicion)
        {
            var lista = await _Proceso.OrdenesCompraXIdRequisicion(idRequisicion);
            return lista;
        }

        [HttpGet("ObtenXIdCotizacion/{idCotizacion:int}")]
        public async Task<ActionResult<List<OrdenCompraDTO>>> ObtenXIdCotizacion(int idCotizacion)
        {
            var lista = await _Proceso.OrdenesCompraXIdCotizacion(idCotizacion);
            return lista;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<OrdenCompraDTO>> GetXId(int Id)
        {
            var lista = await _Service.ObtenXId(Id);
            return lista;
        }
    }
}
