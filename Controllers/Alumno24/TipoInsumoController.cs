
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;







namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los tipos de insumo que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/tipoinsumo/24")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionTipoInsumo-Empresa24")]
    public class TipoInsumoAlumno24Controller : ControllerBase
    {
        private readonly TipoInsumoProceso<Alumno24Context> _TipoInsumoProceso;

        public TipoInsumoAlumno24Controller(
            TipoInsumoProceso<Alumno24Context> tipoInsumoProceso
            )
        {
            _TipoInsumoProceso = tipoInsumoProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearTipoInsumo-Empresa24")]
        public async Task<ActionResult> Post([FromBody] TipoInsumoCreacionDTO parametros)
        {
            await _TipoInsumoProceso.Post(parametros);
            return NoContent();
        }

        /// <summary>
        /// Endpoint del tipo Put que obtiene un registro a partir del Id que recibe dentro del
        /// objeto que recibe como parametro, en el caso de que exista el registro con ese Id
        /// edita con el resto de parametros recibIdos en el objeto mediante el método
        /// "editaTipoInsumoAsync".
        /// </summary>
        /// <param name="parametros">objeto del tipo "TipoInsumoDTO" que recibe los parametros
        /// necesarios para editar el registro</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarTipoInsumo-Empresa24")]
        public async Task<ActionResult> Put([FromBody] TipoInsumoDTO parametros)
        {
            try
            {
                await _TipoInsumoProceso.Put(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
        /// <summary>
        /// Endpoint del tipo Put que obtiene busca un registro de la tabla de TiposInsumo
        /// el cual su Id coincIda con el recibIdo como parametro, y en el caso de que exista
        /// ese insumo, se ejecuta el método "eliminaTipoInsumoAsync" para eliminar el registro
        /// </summary>
        /// <param name="Id">Id del insumo que se desea eliminar</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarTipoInsumo-Empresa24")]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                await _TipoInsumoProceso.Delete(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
        /// <summary>
        /// Endpoint del tipo Get que accede al metodo "obtenTiposInsumoAsync" para 
        /// obtener todos los registros y los regresa paginados conforme a los parametros
        /// de paginación recibIdos.
        /// </summary>
        /// <param name="paginacionDTO">Parametros de paginación</param>
        /// <returns>Lista con todos los registros de la tabla de tipos de insumo</returns>
        [HttpGet("todos")]
        public async Task<ActionResult<List<TipoInsumoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            return await _TipoInsumoProceso.Get();
        }

        /// <summary>
        /// Endpoint del tipo Get que accede al metodo "obtenTiposInsumoAsync" para
        /// obtener todos los registros y los regresa sin paginar
        /// </summary>
        /// <returns></returns>
        [HttpGet("sinpaginar")]
        public async Task<ActionResult<List<TipoInsumoDTO>>> Get()
        {
            return await _TipoInsumoProceso.GetSinPaginar();
        }

        /// <summary>
        /// Endpoint del tipo Get que accede al método "obtenTiposInsumoAsync" para
        /// obtener un registro de la tabla de Tipo de Insumo a partir del Id recibIdo
        /// como parametro
        /// </summary>
        /// <param name="Id">Id del tipo de insumo solicitado</param>
        /// <returns>registro con el Id recibIdo como parametro</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<TipoInsumoDTO>> Get(int Id)
        {
            return await _TipoInsumoProceso.GetXId(Id);
        }

        [HttpGet("TipoInsumosParaRequisitar")]
        public async Task<ActionResult<List<TipoInsumoDTO>>> TipoInsumosParaRequisitar()
        {
            return await _TipoInsumoProceso.TipoInsumosParaRequisitar();
        }
    }
}
