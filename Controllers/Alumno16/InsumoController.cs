using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los Insumos que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/insumo/16")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionInsumo-Empresa16")]
    public class InsumoAlumno16Controller : ControllerBase
    {
        private readonly InsumoProceso<Alumno16Context> _InsumoProceso;
        public InsumoAlumno16Controller(
            InsumoProceso<Alumno16Context> insumoProceso)
        {
            _InsumoProceso = insumoProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearInsumo-Empresa16")]
        public async Task<ActionResult> Post([FromBody] InsumoCreacionDTO parametro)
        {
            await _InsumoProceso.Post(parametro);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarInsumo-Empresa16")]
        public async Task<ActionResult> Put([FromBody] InsumoDTO parametros)
        {
            try
            {
                await _InsumoProceso.Put(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpGet("todos/{IdProyecto:int}")]
        public async Task<ActionResult<List<InsumoDTO>>> Get(int IdProyecto)
        {
            return await _InsumoProceso.Get(IdProyecto);
        }

        [HttpGet("todosparaautocomplete/{IdProyecto:int}")]
        public async Task<ActionResult<List<InsumoDTO>>> GetParaAutoComplete(int IdProyecto)
        {
            return await _InsumoProceso.ObtenerInsumosParaAutoComplete(IdProyecto);
        }

        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarInsumo-Empresa16")]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                await _InsumoProceso.Delete(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPost("migrarInsumos")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarInsumo-Empresa1")]
        public async Task<ActionResult> MigrarInsumos(IdsProyectosParaMigrarInsumoDTO ids)
        {
            await _InsumoProceso.ImportarInsumosAOtroProyecto(ids.IdProyectoAMigrar, ids.IdProyectoActual);
            return NoContent();
        }
    }
}
