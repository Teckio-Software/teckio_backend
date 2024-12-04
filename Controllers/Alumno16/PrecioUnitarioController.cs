
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



using ERP_TECKIO;




namespace SistemaERP.API.Alumno16Controllers.Procomi
{
    [Route("api/preciounitario/16")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPrecioUnitario-Empresa2")]
    public class PrecioUnitarioAlumno16Controller : ControllerBase
    {
        private readonly PrecioUnitarioProceso<Alumno16Context> _precioUnitarioProceso;
        private readonly IProgramacionEstimadaService<Alumno16Context> _programacionestimadaService;
        private readonly DbContextOptionsBuilder<Alumno16Context> _Options;
        public PrecioUnitarioAlumno16Controller(
            PrecioUnitarioProceso<Alumno16Context> precioUnitarioProceso
            , IProgramacionEstimadaService<Alumno16Context> programacionEstimadaService
            , DbContextOptionsBuilder<Alumno16Context> options
            )
        {
            _precioUnitarioProceso = precioUnitarioProceso;
            _programacionestimadaService = programacionEstimadaService;
            _Options = options;
        }

        [HttpGet("todos/{IdProyecto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> Obtener(int IdProyecto)
        {
            return await _precioUnitarioProceso.ObtenerPrecioUnitario(IdProyecto);
        }

        [HttpGet("sinestructurar/{IdProyecto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> ObtenerSinEstructura(int IdProyecto)
        {
            return await _precioUnitarioProceso.ObtenerSinEstructura(IdProyecto);
        }

        [HttpPost("crear")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CrearYObtener([FromBody] PrecioUnitarioDTO registro)
        {
            using (var db = new Alumno16Context(_Options.Options))
            {
                return await _precioUnitarioProceso.CrearYObtener(registro, db);
            }
        }

        [HttpPost("editar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> Editar([FromBody] PrecioUnitarioDTO registro)
        {
            return await _precioUnitarioProceso.Editar(registro);
        }

        [HttpPost("EditarIndirectoPrecioUnitario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<bool>> EditarIndirectoPrecioUnitario([FromBody] PrecioUnitarioDTO registro)
        {
            return await _precioUnitarioProceso.EditarIndirectoPrecioUnitario(registro);
        }

        [HttpPost("eliminar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> Eliminar([FromBody] int idPrecioUnitario)
        {
            return await _precioUnitarioProceso.Eliminar(idPrecioUnitario);
        }

        [HttpPost("copiaregistros")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CopiarRegistros([FromBody] DatosParaCopiarDTO datos)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno16Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.CrearRegistrosCopias(datos.Registros, datos.IdPrecioUnitarioBase, datos.IdProyecto, db);
            }
            return registros;
        }

        [HttpGet("todoscopia/{IdProyecto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioCopiaDTO>>> ObtenerCopia(int IdProyecto)
        {
            return await _precioUnitarioProceso.ObtenerPrecioUnitarioCopia(IdProyecto);
        }

        [HttpPost("copiararmado")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CopiarArmado([FromBody] DatosParaCopiarArmadoDTO datos)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno16Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.CrearRegistrosDetallesCopia(datos.Registros, datos.IdPrecioUnitarioBase, datos.IdProyecto, db);
            }
            return registros;
        }

        [HttpPost("copiararmadocomoconcepto")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CopiarArmadoComoConcepto([FromBody] DatosParaCopiarArmadoDTO datos)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno16Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.CrearRegistrosDetallesCopiaConcepto(datos.Registros, datos.IdPrecioUnitarioBase, datos.IdProyecto, db);
            }
            return registros;
        }

        [HttpPost("excel")]
        public async Task<ActionResult> Excel(int i)
        {
            await _precioUnitarioProceso.CreardeExcel(i);
            return NoContent();
        }

        [HttpPost("excelInsumos")]
        public async Task<ActionResult> ExcelInsumos(int i)
        {
            await _precioUnitarioProceso.CrearRegistrosDeInsumosDeExcel(i);
            return NoContent();
        }

        [HttpGet("obtenerExplosionDeInsumos/{IdProyecto}")]
        public async Task<ActionResult<List<InsumoParaExplosionDTO>>> ObtenerExplosion(int IdProyecto)
        {
            return await _precioUnitarioProceso.obtenerExplosion(IdProyecto);
        }

        [HttpPost("recalcularPresupuesto")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> RecalcularPresupuest([FromBody] int IdProyecto)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno16Context(_Options.Options))
            {
                await _precioUnitarioProceso.RecalcularPresupuesto(IdProyecto, db);
                registros = await _precioUnitarioProceso.ObtenerPrecioUnitario(IdProyecto);
            }
            return registros;
        }
    }
}
