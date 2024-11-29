using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de las entradas a almacén que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/almacenentrada/4")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlmacenEntradaAlumno04Controller : ControllerBase
    {
        private readonly IAlmacenEntradaService<Alumno04Context> _AlmacenEntradaServicio;
        private readonly IInsumoXAlmacenEntradaService<Alumno04Context> _AlmacenEntradaDetalleServicio;
        private readonly AlmacenEntradaProceso<Alumno04Context> _Proceso;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<AlmacenEntradaAlumno04Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno04Context Context;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public AlmacenEntradaAlumno04Controller(
            ILogger<AlmacenEntradaAlumno04Controller> logger,
            Alumno04Context context
            , IAlmacenEntradaService<Alumno04Context> almacenServicio
            , IInsumoXAlmacenEntradaService<Alumno04Context> AlmacenEntradaDetalleServicio
            , AlmacenEntradaProceso<Alumno04Context> Proceso
            )
        {
            Logger = logger;
            Context = context;
            _AlmacenEntradaServicio = almacenServicio;
            _AlmacenEntradaDetalleServicio = AlmacenEntradaDetalleServicio;
            _Proceso = Proceso;
        }

        /// <summary>
        /// Endpoint que llama al método para mostrar los registros de la tabla de almacenes
        /// que recibe como parametro los datos de paginación.
        /// </summary>
        /// <param name="paginacionDTO">Parametros de paginación</param>
        /// <returns>Lista con los registros de la tabla de almacenes sin paginar</returns>
        [HttpGet("todos")]
        public async Task<ActionResult<List<AlmacenEntradaDTO>>> GetAlmacenEntradasPaginado([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = await _AlmacenEntradaServicio.ObtenTodos();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable.AsQueryable());
            var lista = queryable.AsQueryable().Paginar(paginacionDTO).OrderByDescending(x => x.NoEntrada).ToList();
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
        [HttpGet("sinpaginar")]
        public async Task<ActionResult<List<AlmacenEntradaDTO>>> GetAlmacenEntradaSinPaginar()
        {
            return await Task.Run(async () =>
            {
                var queryable = await _AlmacenEntradaServicio.ObtenTodos();
                var lista = queryable.OrderByDescending(x => x.NoEntrada).ToList();
                if (lista.Count <= 0)
                {
                    return new List<AlmacenEntradaDTO>();
                }
                return lista;
            }); ;
        }

        /// <summary>
        /// Endpoint que llama al método que permite obtener un registro a partir del Id de este
        /// </summary>
        /// <param name="Id">Id del registro a obtener</param>
        /// <returns>lista con el registro del Id dado</returns>
        /// 
        [HttpGet("ObtenXIdProyecto/{idProyecto}")]
        public async Task<ActionResult<List<AlmacenEntradaDTO>>> ObtenXIdProyecto(int idProyecto)
        {
            var lista = await _Proceso.ObtenXIdProyecto(idProyecto);
            return lista;
        }
        [HttpGet("ObtenXIdRequisicion/{idRequisicion}")]
        public async Task<ActionResult<List<AlmacenEntradaDTO>>> ObtenXIdRequisicion(int idRequisicion)
        {
            var lista = await _Proceso.ObtenXIdRequisicion(idRequisicion);
            return lista;
        }

        [HttpGet("ObtenXIdOrdenCompra/{idOrdenCompra}")]
        public async Task<ActionResult<List<AlmacenEntradaDTO>>> ObtenXIdOrdenCompra(int idOrdenCompra)
        {
            var lista = await _Proceso.ObtenXIdOrdenCompra(idOrdenCompra);
            return lista;
        }

        /// <summary>
        /// Endpoint para crear un nuevo almacen el cual recibe los parametros necesarios
        /// para la creación del nuevo registro
        /// </summary>
        /// <param name="CreacionDTO">Todos los campos de la tabla excepto el Id</param>
        /// <returns>NoContent</returns>
        [HttpPost("CrearAlmacenEntrada")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<RespuestaDTO> CrearAlmacenEntrada([FromBody] AlmacenEntradaCreacionDTO CreacionDTO)
        {
            var authen = HttpContext.User;
            return await _Proceso.CrearAlmacenEntrada(CreacionDTO, authen.Claims.ToList());
        }

        [HttpPut("EditarAlmacenEntrada")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<RespuestaDTO> EditarAlmacenEntrada([FromBody] AlmacenEntradaDTO almacenEntrada)
        {
            return await _Proceso.EditarAlmacenEntrada(almacenEntrada);
        }

        [HttpPost("CrearAjusteEntradaAlmacen")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<RespuestaDTO> CrearAjusteEntradaAlmacen([FromBody] AlmacenEntradaCreacionDTO CreacionDTO)
        {
            var authen = HttpContext.User;
            return await _Proceso.CrearAjusteEntradaAlmacen(CreacionDTO, authen.Claims.ToList());
        }

        [HttpPost("CrearDevolucionEntradaAlmacen")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<RespuestaDTO> CrearDevolucionEntradaAlmacen([FromBody] AlmacenEntradaDevolucionCreacionDTO CreacionDTO)
        {
            var authen = HttpContext.User;
            return await _Proceso.CrearDevolucionEntradaAlmacen(CreacionDTO, authen.Claims.ToList());
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
        public async Task<ActionResult> PutAlmacenEntrada([FromBody] AlmacenEntradaDTO Edita)
        {
            var resultado = await _AlmacenEntradaServicio.Editar(Edita);
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
        public async Task<ActionResult> DeleteAlmacenEntrada(int Id)
        {
            var lista = await _AlmacenEntradaServicio.Cancelar(Id);
            return NoContent();
        }
    }
}
