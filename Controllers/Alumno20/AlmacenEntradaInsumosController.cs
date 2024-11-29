using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de las entradas a almacén que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/almacenentradainsumo/20")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionEntradaAlmacen-Empresa1")]
    public class AlmacenEntradaInsumoAlumno20Controller : ControllerBase
    {
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<AlmacenEntradaInsumoAlumno20Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno20Context Context;
        private readonly IInsumoXAlmacenEntradaService<Alumno20Context> _AlmacenEntradaDetalle;
        private readonly AlmacenEntradaProceso<Alumno20Context> _Proceso;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public AlmacenEntradaInsumoAlumno20Controller(
            ILogger<AlmacenEntradaInsumoAlumno20Controller> logger,
            Alumno20Context context
            , IInsumoXAlmacenEntradaService<Alumno20Context> AlmacenEntradaDetalle
            , AlmacenEntradaProceso<Alumno20Context> Proceso
            )
        {
            Logger = logger;
            Context = context;
            _AlmacenEntradaDetalle = AlmacenEntradaDetalle;
            _Proceso = Proceso;
        }

        [HttpPost("CrearInsumoEntradaAlmacen")]
        public async Task<ActionResult<RespuestaDTO>> CrearInsumoEntradaAlmacen(AlmacenEntradaInsumoCreacionDTO parametro)
        {
            return await _Proceso.CrearInsumoEntradaAlmacen(parametro);
        }

        [HttpPost("CrearInsumoAjusteAlmacen")]
        public async Task<ActionResult<RespuestaDTO>> CrearInsumoAjusteAlmacen(AlmacenEntradaInsumoCreacionDTO parametro)
        {
            return await _Proceso.CrearInsumoAjusteAlmacen(parametro);
        }

        /// <summary>
        /// Endpoint que llama al método para mostrar los registros de la tabla de almacenes
        /// que recibe como parametro los datos de paginación.
        /// </summary>
        /// <param name="paginacionDTO">Parametros de paginación</param>
        /// <param name="IdAlmacenEntrada">Parametros de paginación</param>
        /// <returns>Lista con los registros de la tabla de almacenes sin paginar</returns>
        [HttpGet("todos/{IdAlmacenEntrada:int}")]
        public async Task<ActionResult<List<AlmacenEntradaInsumoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdAlmacenEntrada)
        {
            var queryable = await _AlmacenEntradaDetalle.ObtenXIdAlmacenEntrada(IdAlmacenEntrada);
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable.AsQueryable());
            var lista = queryable.AsQueryable().Paginar(paginacionDTO).ToList();
            if (lista.Count <= 0)
            {
                return NoContent();
            }
            return lista;
        }

        /// <summary>
        /// Endpoint que obtiene todos los registros de la tabla de almacenes sin paginar
        /// </summary>
        /// <returns>todos los registros de la tabla sin paginar</returns>
        [HttpGet("sinpaginar/{IdAlmacenEntrada:int}")]
        public async Task<ActionResult<List<AlmacenEntradaInsumoDTO>>> Get(int IdAlmacenEntrada)
        {
            var lista = await _AlmacenEntradaDetalle.ObtenXIdAlmacenEntrada(IdAlmacenEntrada);
            return lista;
        }

        [HttpGet("ObtenXIdProyecto/{idProyecto}")]
        public async Task<ActionResult<List<AlmacenEntradaInsumoDTO>>> ObtenXIdProyecto(int idProyecto)
        {
            var lista = await _Proceso.InsumosAlmacenEntradaObtenXIdProyceto(idProyecto);
            return lista;
        }


        [HttpGet("ObtenXIdRequisicion/{idRequisicion}")]
        public async Task<ActionResult<List<AlmacenEntradaInsumoDTO>>> ObtenXIdRequisicion(int idRequisicion)
        {
            var lista = await _Proceso.InsumosAlmacenEntradaObtenXIdRequisicion(idRequisicion);
            return lista;
        }

        [HttpGet("ObtenXIdEntradaAlmacen/{idEntradaAlmacen}")]
        public async Task<ActionResult<List<AlmacenEntradaInsumoDTO>>> ObtenXIdEntradaAlmacen(int idEntradaAlmacen)
        {
            var lista = await _Proceso.IsumosAlmacenEntradaObtenXIdEntradaAlmacen(idEntradaAlmacen);
            return lista;
        }

        /// <summary>
        /// Endpoint que llama al método que permite obtener un registro a partir del Id de este
        /// </summary>
        /// <param name="Id">Id del registro a obtener</param>
        /// <returns>lista con el registro del Id dado</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<AlmacenEntradaInsumoDTO>> GetXId(int Id)
        {
            var lista = await _AlmacenEntradaDetalle.ObtenXId(Id);
            return lista;

        }

        /// <summary>
        /// Endpoint para crear un nuevo almacen el cual recibe los parametros necesarios
        /// para la creación del nuevo registro
        /// </summary>
        /// <param name="CreacionDTO">Todos los campos de la tabla excepto el Id</param>
        /// <returns>NoContent</returns>
        [HttpPost("{IdAlmacenEntrada:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearEntradaAlmacen-Empresa1")]
        public async Task<ActionResult> Post([FromBody] AlmacenEntradaInsumoCreacionDTO CreacionDTO, int IdAlmacenEntrada)
        {
            await _AlmacenEntradaDetalle.Crear(CreacionDTO);
            return NoContent();
        }

        /// <summary>
        /// Éndpoint para editar un registro el cual recibe como parametros almacenDTO
        /// el cual contiene todos los campos de la tabla y hace una edición a partir de 
        /// el Id del registro.
        /// </summary>
        /// <param name="Edita">Todos los campos de la tabla de almacen</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarEntradaAlmacen-Empresa1")]
        public async Task<ActionResult> Put([FromBody] AlmacenEntradaInsumoDTO Edita)
        {
            var resultado = await _AlmacenEntradaDetalle.Editar(Edita);
            return NoContent();
        }

        /// <summary>
        /// Endpoint que permite eliminar un registro de la tabla a partir del Id que recibe
        /// el cual es el Id del registro
        /// </summary>
        /// <param name="Id">Id del registro a eliminar</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarEntradaAlmacen-Empresa1")]
        public async Task<ActionResult> Delete(int Id)
        {
            var lista = await _AlmacenEntradaDetalle.Cancelar(Id);
            return NoContent();
        }
    }
}
