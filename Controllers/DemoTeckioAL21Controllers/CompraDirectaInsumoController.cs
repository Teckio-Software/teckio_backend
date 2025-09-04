using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los conceptos que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/compradirectainsumos/21")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionOrdenCompra-Empresa1")]
    public class CompraDirectaInsumoDemoTeckioAL21Controller : ControllerBase
    {
        private readonly IInsumoXCompraDirectaService<DemoTeckioAL21Context> _Service;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<CompraDirectaInsumoDemoTeckioAL21Controller> _Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly DemoTeckioAL21Context _Context;
        /// <summary>
        /// Constructor del controlador de Conceptos
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public CompraDirectaInsumoDemoTeckioAL21Controller(
            ILogger<CompraDirectaInsumoDemoTeckioAL21Controller> logger
            , DemoTeckioAL21Context context
            , IInsumoXCompraDirectaService<DemoTeckioAL21Context> Service)
        {
            _Logger = logger;
            _Context = context;
            _Service = Service;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para obtener los registros de la tabla de concepto
        /// </summary>
        /// <param name="paginacionDTO">Numero de pagina y Cantidad de registros</param>
        /// <returns>Lista de los conceptos</returns>
        [HttpGet("todos/{IdCompraDirecta:int}")]
        public async Task<ActionResult<List<InsumoXCompraDirectaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdCompraDirecta)
        {
            //var queryable = CompraDirectaInsumoSP.obtenCompraDirectaInsumosAsync(IdCompraDirecta).Result.AsQueryable();
            var lista = await _Service.ObtenXIdCompraDirecta(IdCompraDirecta);
            var queryable = lista.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var listaResult = queryable.OrderBy(x => x.CodigoInsumo).Paginar(paginacionDTO).ToList();
            if (listaResult.Count <= 0)
            {
                return new List<InsumoXCompraDirectaDTO>();
            }
            return listaResult;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para obtener los registros de la tabla de concepto pero sin paginar
        /// </summary>
        /// <returns>Lista de conceptos sin paginar</returns>
        [HttpGet("sinpaginar/{IdCompraDirecta:int}")]
        public async Task<ActionResult<List<InsumoXCompraDirectaDTO>>> GetSinPaginar(int IdCompraDirecta)
        {
            var lista = await _Service.ObtenXIdCompraDirecta(IdCompraDirecta);
            if (lista.Count <= 0)
            {
                return new List<InsumoXCompraDirectaDTO>();
            }
            return lista;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para obtener un registro a partir de un Id dado
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Registro especifico a partir del Id</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<InsumoXCompraDirectaDTO>> GetXId(int Id) //recibe un Id para ejecutar la acción
        {
            var objeto = await _Service.ObtenXId(Id);
            if (objeto.Id <= 0)
            {
                return new InsumoXCompraDirectaDTO();
            }
            return objeto;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para eliminar un registro en tabla
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarOrdenCompra-Empresa1")]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusCancelar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<DemoTeckioAL21Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
