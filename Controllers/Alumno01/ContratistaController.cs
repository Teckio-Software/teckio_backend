using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los insumos por contratista que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/contratista/1")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionContratista-Empresa1")]
    public class ContratistaAlumno01Controller : ControllerBase
    {
        private readonly IContratistaService<Alumno01Context> _ContratistaService;
        /// <summary>
        /// Para mostrar los errores en consola
        /// </summary>
        private readonly ILogger<ContratistaAlumno01Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary> 
        private readonly Alumno01Context Context;
        /// <summary>
        /// Constructor del controlador de contratista
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public ContratistaAlumno01Controller(
            ILogger<ContratistaAlumno01Controller> logger,
            Alumno01Context context
            , IContratistaService<Alumno01Context> ContratistaService
            )
        {
            Logger = logger;
            Context = context;
            _ContratistaService = ContratistaService;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para obtener los registros de la tabla de concepto
        /// </summary>
        /// <param name="paginacionDTO">Numero de pagina y Cantidad de registros</param>
        /// <returns>Lista de los conceptos</returns>
        [HttpGet("todos")]
        public async Task<ActionResult<List<ContratistaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            return await _ContratistaService.ObtenTodos();
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para obtener un registro a partir de un Id dado
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Registro especifico a partir del Id</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ContratistaDTO>> GetContratista(int Id) //recibe un Id para ejecutar la acción
        {
            return await _ContratistaService.ObtenXId(Id);
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método que permite crear un registro en la tabla
        /// </summary>
        /// <param name="CreacionDTO">Descripción, Unidad, Detalle y Agrupación</param>
        /// <returns>NoContent</returns>
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearContratista-Empresa1")]
        public async Task<ActionResult> Post([FromBody] ContratistaDTO registro) //Recibe los parametros para la creación
        {
            await _ContratistaService.CrearYObtener(registro);
            return NoContent();
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para editar un registro en tabla
        /// </summary>
        /// <param name="Edita">Id, Descripción, Unidad, Detalle y Agrupación</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarContratista-Empresa1")]
        public async Task<ActionResult<RespuestaDTO>> Put([FromBody] ContratistaDTO Edita)
        {
            var respuesta = await _ContratistaService.Editar(Edita);
            return respuesta;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para eliminar un registro en tabla
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarContratista-Empresa1")]
        public async Task<ActionResult<RespuestaDTO>> Delete(int Id)
        {
            
            var lista = await _ContratistaService.Eliminar(Id);
            return lista;
        }
    }
}
