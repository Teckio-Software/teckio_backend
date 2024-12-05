using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los Insumos que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/insumo/34")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionInsumo-Empresa34")]
    public class InsumoAlumno34Controller : ControllerBase
    {
        private readonly InsumoProceso<Alumno34Context> _InsumoProceso;
        public InsumoAlumno34Controller(
            InsumoProceso<Alumno34Context> insumoProceso)
        {
            _InsumoProceso = insumoProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearInsumo-Empresa34")]
        public async Task<ActionResult> Post([FromBody] InsumoCreacionDTO parametro)
        {
            await _InsumoProceso.Post(parametro);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarInsumo-Empresa34")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarInsumo-Empresa34")]
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
