using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Procesos.Facturacion;
using ERP_TECKIO.DTO.Factura;


namespace ERP_TECKIO.Controllers
{
    [Route("api/ordencompra/15")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenCompraAlumno15Controller : ControllerBase
    {
        private readonly IOrdenCompraService<Alumno15Context> _Service;
        private readonly OrdenCompraProceso<Alumno15Context> _Proceso;
        private readonly ILogger<OrdenCompraAlumno15Controller> _Logger;
        private readonly Alumno15Context Context;
        private readonly PolizaProceso<Alumno15Context> _polizaProceso;
        private readonly ObtenFacturaProceso<Alumno15Context> _obtenFacturaProceso;

        public OrdenCompraAlumno15Controller(
            ILogger<OrdenCompraAlumno15Controller> Logger
            , Alumno15Context Context
            , IOrdenCompraService<Alumno15Context> Service
            , OrdenCompraProceso<Alumno15Context> Proceso
            , PolizaProceso<Alumno15Context> polizaProceso
            , ObtenFacturaProceso<Alumno15Context> obtenFacturaProceso
            )
        {
            _Logger = Logger;
            this.Context = Context;
            _Service = Service;
            _Proceso = Proceso;
            _polizaProceso = polizaProceso;
            _obtenFacturaProceso = obtenFacturaProceso;
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno15Context>(resultado.Estatus, resultado.Descripcion);
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno15Context>(resultado.Estatus, resultado.Descripcion);
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno15Context>(resultado.Estatus, resultado.Descripcion);
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

        [HttpGet("ObtenerTodas")]
        public async Task<ActionResult<List<OrdenCompraDTO>>> ObtenerTodas()
        {
            var lista = await _Proceso.ObtenerTodas();
            return lista;
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

        [HttpPost("cargarFacturasXOrdenCompra")]
        public async Task<ActionResult<RespuestaDTO>> CargarFacturaXOrdenCompra([FromForm] List<IFormFile> files ,[FromForm] int IdOrdenCompra) {
            return await _obtenFacturaProceso.CargarFacturaXOrdenCompra(files, IdOrdenCompra);
        }

        [HttpGet("obtenerFacturasXOrdenCompra/{IdOrdenCompra:int}")]
        public async Task<ActionResult<OrdenCompraFacturasDTO>> obtenerFacturasXOrdenCompra(int IdOrdenCompra) {
            return await _obtenFacturaProceso.ObtenFacturaXOrdenCompra(IdOrdenCompra);
        }


        [HttpGet("ObtenerXIdContratistaSinPagar/{IdContratista:int}")]
        public async Task<ActionResult<List<OrdenCompraDTO>>> ObtenerXIdContratistaSinPagar(int IdContratista)
        {
            var lista = await _Proceso.ObtenerXIdContratistaSinPagar(IdContratista);
            return lista;
        }

        [HttpGet("ObtenerFacturasXIdContratistaSinPagar/{IdContratista:int}")]
        public async Task<ActionResult<List<FacturaXOrdenCompraDTO>>> ObtenerFacturasXIdContratistaSinPagar(int IdContratista)
        {
            var lista = await _Proceso.ObtenerFacturasXIdContratistaSinPagar(IdContratista);
            return lista;
        }

        [HttpPost("AutorizarFacturaXOrdenCompra")]
        public async Task<ActionResult<RespuestaDTO>> AutorizarFacturaXOrdenCompra(FacturaXOrdenCompraDTO facturaXOrdenCompra)
        {
            var lista = await _obtenFacturaProceso.AutorizarFacturaXOrdenCompra(facturaXOrdenCompra);
            return lista;
        }

        [HttpPost("CancelarFacturaXOrdenCompra")]
        public async Task<ActionResult<RespuestaDTO>> CancelarFacturaXOrdenCompra(FacturaXOrdenCompraDTO facturaXOrdenCompra)
        {
            var lista = await _obtenFacturaProceso.CancelarFacturaXOrdenCompra(facturaXOrdenCompra);
            return lista;
        }

        [HttpGet("ObtenerInsumosComprados/{IdProyecto:int}")]
        public async Task<ActionResult<List<InsumoDTO>>> ObtenerInsumosComprados(int IdProyecto)
        {
            var respuesta = await _Proceso.ObtenerInsumosComprados(IdProyecto);
            return respuesta;
        }

        [HttpGet("ObtenerOrdenesCompraXInsumo/{IdInsumo:int}")]
        public async Task<ActionResult<List<OrdenesCompraXInsumoDTO>>> ObtenerOrdenesCompraXInsumo(int IdInsumo)
        {
            var respuesta = await _Proceso.ObtenerOrdenesCompraXInsumo(IdInsumo);
            return respuesta;
        }

        
    }
}
