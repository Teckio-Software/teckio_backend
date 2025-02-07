using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador del tipo InsumoXRequisicion que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/insumoxrequisicion/6")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InsumoXRequisicionAlumno06Controller : ControllerBase
    {
        private readonly IInsumoXRequisicionService<Alumno06Context> _Service;

        private readonly RequisicionProceso<Alumno06Context> _Proceso;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<InsumoXRequisicionAlumno06Controller> _Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno06Context Context;
        /// <summary>
        /// Constructor del controlador de InsumoXRequisicion
        /// </summary>
        /// <param name="Logger"></param>
        /// <param name="Context"></param>
        public InsumoXRequisicionAlumno06Controller(
            ILogger<InsumoXRequisicionAlumno06Controller> Logger,
            RequisicionProceso<Alumno06Context> Proceso,
            Alumno06Context Context
            , IInsumoXRequisicionService<Alumno06Context> Service)
        {
            _Logger = Logger;
            this.Context = Context;
            _Service = Service;
            _Proceso = Proceso;
        }

        [HttpPost]
        [Route("CrearInsumoXRequisicion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> CrearInsumoXRequisicion([FromBody] InsumoXRequisicionCreacionDTO parametro)
        {
            return await _Proceso.CrearInsumoXRequisicion(parametro);
        }

        /// <summary>
        /// Endpoint del tipo get que muestra todos los insumos por requisición a partir de 
        /// un Id de requisición dado paginados
        /// </summary>
        /// <param name="paginacionDTO">Datos de paginación</param>
        /// <param name="IdRequisicion">Identificador de la requisición</param>
        /// <returns>Insumos de la requisición</returns>
        [HttpGet("todos/{IdRequisicion:int}")]
        public async Task<ActionResult<List<InsumoXRequisicionDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdRequisicion)
        {
            var lista = await _Service.ObtenXIdRequisicion(IdRequisicion);
            var query = lista.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(query);
            var listaResult = query.OrderBy(z => z.Id).Paginar(paginacionDTO).ToList();
            return listaResult;
        }

        /// <summary>
        /// Endpoint del tipo get que muestra todos los insumos por requisición a partir de 
        /// un Id de requisición
        /// </summary>
        /// <param name="IdRequisicion">Identificador de la requisición</param>
        /// <returns>Insumos de la requisición</returns>
        [HttpGet("sinpaginar/{IdRequisicion:int}")]
        public async Task<ActionResult<List<InsumoXRequisicionDTO>>> Get(int IdRequisicion)
        {
            return await _Proceso.InsumosXRequisicion(IdRequisicion);
        }

        [HttpGet("AutorizarXId/{idInusmoRequisicion:int}")]
        public async Task<ActionResult<RespuestaDTO>> AutorizarXId(int idInusmoRequisicion)
        {
            return await _Proceso.AutorizarXIdInsumoRequisicion(idInusmoRequisicion);
        }

        [HttpPost("AutorizarInsumosRequisicionSeleccionados")]
        public async Task<ActionResult<RespuestaDTO>> AutorizarInsumosRequisicionSeleccionados([FromBody] List<InsumoXRequisicionDTO> seleccionados)
        {
            return await _Proceso.AutorizarInsumosRequisicionSeleccionados(seleccionados);
        }

        /// <summary>
        /// Endpoint del tipo Get para que recupera un registro de la tabla InsumoXRequisicion
        /// cuyo Id sea igual al del parametro recibIdo
        /// </summary>
        /// <param name="Id">Id del registro a buscar</param>
        /// <returns>Registro con el Id solicitado</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<InsumoXRequisicionDTO>> GetXId(int Id)
        {
            var lista = await _Service.ObtenXId(Id);
            return lista;
        }

        /// <summary>
        /// Endpoint del tipo put para editar un registro a partir del Id del objeto que recibe
        /// como parametro e incerta los demas datos en ese registro
        /// </summary>
        /// <param name="Edita"></param>
        /// <returns></returns>
        [HttpPut("EditarInsumoXRequisicion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarRequisicion-Empresa1")]
        public async Task<ActionResult<RespuestaDTO>> EditarInsumoXRequisicion([FromBody] InsumoXRequisicionDTO objeto)
        {
            return await _Service.Editar(objeto);
        }

        /// <summary>
        /// Endpoint del tipo delete para eliminar un registro cuyo Id coincIda con el 
        /// Id recibIdo como parametro
        /// </summary>
        /// <param name="Id">Identificador del registro a eliminar</param>
        /// <returns>NoContent</returns>
        [HttpDelete("EliminarInsumoXRequisicion/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarRequisicion-Empresa1")]
        public async Task<ActionResult<RespuestaDTO>> Delete(int Id)
        {
            return await _Service.Eliminar(Id);
        }
    }
}
