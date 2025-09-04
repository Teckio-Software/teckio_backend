using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los proyectos que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/proyecto/18")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//)]//, Policy = "SeccionProyecto-Empresa1")]
    public class ProyectoDemoTeckioAL18Controller : ControllerBase
    {
        private readonly ProyectoProceso<DemoTeckioAL18Context> _ProyectoProceso;
        public ProyectoDemoTeckioAL18Controller(
            ProyectoProceso<DemoTeckioAL18Context> proyectoProceso)
        {
            _ProyectoProceso = proyectoProceso;
        }
        /// <summary>
        /// Endpoint para crear un proyecto
        /// </summary>
        /// <param name="parametroCreacionDTO">Un objeto del tipo </param>
        /// <returns>Un mensaje de resultado en headers</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//)]//, Policy = "CrearProyecto-Empresa1")]
        public async Task<ActionResult<ProyectoDTO>> Post([FromBody] ProyectoDTO parametroCreacionDTO)
        {
            var respuesta =  await _ProyectoProceso.Post(parametroCreacionDTO);
            return respuesta;
        }
        /// <summary>
        /// EndPoint para crear y regresar un proyecto
        /// </summary>
        /// <param name="creacionDTO">Un objeto del tipo <see cref="ProyectoCreacionDTO"/></param>
        /// <returns>Un objeto del tipo <see cref="ProyectoDTO"/></returns>
        [HttpPost("crearyobtener")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//)]//, Policy = "CrearProyecto-Empresa1")]
        public async Task<ActionResult<ProyectoDTO>> PostCrearYObtener([FromBody] ProyectoDTO creacionDTO)
        {
            return await _ProyectoProceso.PostCrearYObtener(creacionDTO);
        }
        /// <summary>
        /// Endpoint del tipo Put que permite editar un registro que recibe como parametro un objeto del tipo
        /// ProyectoDTO el cual contiene los parametros que se editarán y el Id, este ultimo es utilizado para
        /// Identificar que registro se editará
        /// </summary>
        /// <param name="parametros">Datos necesarios para editar un registro y el Id del registro a editar</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//)]//, Policy = "EditarProyecto-Empresa1")]
        public async Task<ActionResult> Put([FromBody] ProyectoDTO parametros)
        {
            await _ProyectoProceso.Put(parametros);
            return NoContent();
        }
        /// <summary>
        /// Endpoint del tipo delete que permite eliminar un registro, recibe como parametro el Id del registro
        /// a eliminar.
        /// </summary>
        /// <param name="Id">Id del registro a eliminar</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//)]//, Policy = "EliminarProyecto-Empresa1")]
        public async Task<ActionResult> delete(int Id)
        {
            await _ProyectoProceso.delete(Id);
            return NoContent();
        }
        /// <summary>
        /// Endpoint del tipo Get para llamar al metodo que obtiene todos los registros de 
        /// la tabla de proyectos y lo retorna como una lista paginada
        /// </summary>
        /// <param name="paginacionDTO">Recibe los parametros de paginación para devolverlos paginados</param>
        /// <returns>Lista con los registros de la tabla acorde a los parametros de paginación</returns>
        [HttpGet("todos")]
        public async Task<ActionResult<List<ProyectoDTO>>> Get()
        {
            return await _ProyectoProceso.Get();
        }

        /// <summary>
        /// Endpoint del tipo Get para invocar el metodo que obtiene todos los registros
        /// de la tabla de proyectos sin un paginado
        /// </summary>
        /// <returns>Lista con los registros de la tabla de Proyectos sin paginar</returns>
        [HttpGet("sinpaginar")]
        public async Task<ActionResult<List<ProyectoDTO>>> GetSinEstructura()
        {
            return await _ProyectoProceso.GetSinEstructura();
        }
        ///// <summary>
        ///// Arma un treeNode con sus hijos de los proyectos
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("sinpaginarparatabla")]
        //public async Task<ActionResult<List<ProyectoDTO>>> GetParaTabla()
        //{
        //    var queryable = ProyectoSP.roots();
        //    var queryable1 = ProyectoSP.obtenProyectoAsync().Result.AsQueryable();
        //    await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable1);
        //    var lista = queryable.OrderBy(z => z.Id).ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}

        /// <summary>
        /// Endpoint del tipo Get que recupera un registro de la tabla de proyectos a partir de un Id
        /// el Id lo recibe como parametro para obtener ese registro.
        /// </summary>
        /// <param name="Id">Id del proyecto solicitado</param>
        /// <returns>Proyecto cuyo Id es igual al recibIdo como parametro</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProyectoDTO>> GetXId(int Id)
        {
            return await _ProyectoProceso.GetXId(Id);
        }
    }
}