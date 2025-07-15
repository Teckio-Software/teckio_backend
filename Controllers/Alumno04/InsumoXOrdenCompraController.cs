using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/insumoxordencompra/4")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InsumoXOrdenCompraAlumno04Controller : ControllerBase
    {
        private readonly IInsumoXOrdenCompraService<Alumno04Context> _Service;
        private readonly OrdenCompraProceso<Alumno04Context> _Proceso;
        private readonly ILogger<InsumoXOrdenCompraAlumno04Controller> _Logger;
        private readonly Alumno04Context _Context;
        public InsumoXOrdenCompraAlumno04Controller(
            ILogger<InsumoXOrdenCompraAlumno04Controller> Logger,
            Alumno04Context Context
            , IInsumoXOrdenCompraService<Alumno04Context> Service
            , OrdenCompraProceso<Alumno04Context> Proceso)
        {
            _Logger = Logger;
            _Context = Context;
            _Service = Service;
            _Proceso = Proceso;
        }

        [HttpPost("CrearInsumoOrdenCompra")]
        public async Task<ActionResult<RespuestaDTO>> CrearInsumoOrdenCompra(InsumoXOrdenCompraCreacionDTO objeto) {
            return await _Proceso.CrearInsumoOrdenCompra(objeto);
        }

        [HttpGet("todos/{IdOrdenCompra:int}")]
        public async Task<ActionResult<List<InsumoXOrdenCompraDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdOrdenCompra)
        {
            var lista = await _Service.ObtenXIdOrdenCompra(IdOrdenCompra);
            var query = lista.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(query);
            var listaResult = query.OrderBy(z => z.Id).Paginar(paginacionDTO).ToList();
            return listaResult;
        }

        [HttpGet("ObtenXIdOrdenCompra/{idOrdenCompra:int}")]
        public async Task<ActionResult<List<InsumoXOrdenCompraDTO>>> ObtenXIdOrdenCompra(int idOrdenCompra)
        {
            var lista = await _Proceso.InsumosOrdenCompraXIdOrdenCompra(idOrdenCompra);
            return lista;
        }

        [HttpGet("ObtenXIdCotizacion/{idCotizacion:int}")]
        public async Task<ActionResult<List<InsumoXOrdenCompraDTO>>> ObtenXIdCotizacion(int idCotizacion)
        {
            var lista = await _Proceso.InsumosOrdenCompraXIdCotizacion(idCotizacion);
            return lista;
        }

        [HttpGet("ObtenXIdRequisicion/{idRequisicion:int}")]
        public async Task<ActionResult<List<InsumoXOrdenCompraDTO>>> ObtenXIdRequisicion(int idRequisicion)
        {
            var lista = await _Proceso.InsumosOrdenCompraXIdRequisicion(idRequisicion);
            return lista;
        }

        [HttpGet("obtenerXIdContratista/{idContratista:int}/{idProyecto:int}")]
        public async Task<ActionResult<List<InsumoXOrdenCompraDTO>>> ObtenXIdContratista(int idContratista, int idProyecto)
        {
            return await _Proceso.InsumosXOrdenCompraXIdContratista(idContratista, idProyecto);
        }

        [HttpGet("ObtenerImpuestosInsumoOrdenCompra/{idInsumoXOrdenCompra:int}")]
        public async Task<ActionResult<List<ImpuestoInsumoOrdenCompraDTO>>> ObtenerImpuestosInsumoOrdenCompra(int idInsumoXOrdenCompra)
        {
            return await _Proceso.ObtenerImpuestosInsumoOrdenCompra(idInsumoXOrdenCompra);
        }


        [HttpGet("sinpaginar/{IdOrdenCompra:int}")]
        public async Task<ActionResult<List<InsumoXOrdenCompraDTO>>> Get(int IdOrdenCompra)
        {
            var lista = await _Service.ObtenXIdOrdenCompra(IdOrdenCompra);
            var query = lista.AsQueryable();
            var listaResult = query.OrderBy(z => z.Id).ToList();
            return listaResult;
        }

        [HttpPut("cancelar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarOrdenCompra-Empresa1")]
        public async Task<ActionResult> PutCancelar(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusCancelar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno04Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
