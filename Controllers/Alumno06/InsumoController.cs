using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los Insumos que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/insumo/6")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionInsumo-Empresa6")]
    public class InsumoAlumno06Controller : ControllerBase
    {
        private readonly InsumoProceso<Alumno06Context> _InsumoProceso;
        public InsumoAlumno06Controller(
            InsumoProceso<Alumno06Context> insumoProceso)
        {
            _InsumoProceso = insumoProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearInsumo-Empresa6")]
        public async Task<ActionResult> Post([FromBody] InsumoCreacionDTO parametro)
        {
            await _InsumoProceso.Post(parametro);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarInsumo-Empresa6")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarInsumo-Empresa6")]
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
