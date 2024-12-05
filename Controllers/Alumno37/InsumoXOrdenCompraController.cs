

using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;




namespace ERP_TECKIO
{
    [Route("api/insumoxordencompra/37")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InsumoXOrdenCompraAlumno37Controller : ControllerBase
    {
        private readonly IInsumoXOrdenCompraService<Alumno37Context> _Service;
        private readonly OrdenCompraProceso<Alumno37Context> _Proceso;
        private readonly ILogger<InsumoXOrdenCompraAlumno37Controller> _Logger;
        private readonly Alumno37Context _Context;
        public InsumoXOrdenCompraAlumno37Controller(
            ILogger<InsumoXOrdenCompraAlumno37Controller> Logger,
            Alumno37Context Context
            , IInsumoXOrdenCompraService<Alumno37Context> Service
            , OrdenCompraProceso<Alumno37Context> Proceso)
        {
            _Logger = Logger;
            _Context = Context;
            _Service = Service;
            _Proceso = Proceso;
        }

        [HttpPost("CrearInsumoOrdenCompra")]
        public async Task<ActionResult<RespuestaDTO>> CrearInsumoOrdenCompra(InsumoXOrdenCompraCreacionDTO objeto)
        {
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
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno37Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
