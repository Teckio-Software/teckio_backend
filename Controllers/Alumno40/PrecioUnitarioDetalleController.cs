using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.DTO;
using DocumentFormat.OpenXml.Office2010.Excel;





namespace SistemaERP.API.Alumno40Controllers.Procomi
{
    [Route("api/preciounitariodetalle/1040")]
    [ApiController]
    public class PrecioUnitarioDetalleAlumno40Controller : ControllerBase
    {
        private readonly PrecioUnitarioProceso<Alumno40Context> _precioUnitarioProceso;
        private readonly DbContextOptionsBuilder<Alumno40Context> _Options;
        public PrecioUnitarioDetalleAlumno40Controller(
            PrecioUnitarioProceso<Alumno40Context> precioUnitarioProceso,
            DbContextOptionsBuilder<Alumno40Context> options
            )
        {
            _precioUnitarioProceso = precioUnitarioProceso;
            _Options = options;
        }

        [HttpGet("todos/{IdPrecioUnitario:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> Obtener(int IdPrecioUnitario)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno40Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.ObtenerDetalles(IdPrecioUnitario, db);
            }
            return registros;
        }

        [HttpGet("todosFiltrado/{IdPrecioUnitario:int}/{IdTipoInsumo}")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> ObtenerFiltrado(int IdPrecioUnitario, int IdTipoInsumo)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno40Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.ObtenerDetallesPorTipoInsumo(IdPrecioUnitario, IdTipoInsumo, db);
            }
            return registros;
        }

        [HttpPost("obtenhijos")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> ObtenerHijos([FromBody] PrecioUnitarioDetalleDTO registro)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno40Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.ObtenerDetallesHijos(registro, db);
            }
            return registros;
        }

        [HttpPost("crear")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> CrearYObtener([FromBody] PrecioUnitarioDetalleDTO registro)
        {
            return await _precioUnitarioProceso.CrearYObtenerDetalle(registro);
        }

        [HttpPost("editar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> Editar([FromBody] PrecioUnitarioDetalleDTO registro)
        {
            return await _precioUnitarioProceso.EditarDetalle(registro);
        }

        [HttpPost("editarImporte")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> editarImporte([FromBody] PrecioUnitarioDetalleDTO registro)
        {
            return await _precioUnitarioProceso.EditarImporteDetalle(registro);
        }

        [HttpPost("partirDetalle")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> partirDetalle([FromBody] PrecioUnitarioDetalleDTO registro)
        {
            return await _precioUnitarioProceso.PartirDetalle(registro);
        }

        [HttpPost("eliminar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> Eliminar([FromBody] int Id)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno40Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.EliminarDetalle(Id, db);
            }
            return registros;
        }

        [HttpPost("crearOperaciones")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> CrearOperacion([FromBody] OperacionesXPrecioUnitarioDetalleDTO registro)
        {
            return await _precioUnitarioProceso.CrearOperacion(registro);
        }

        [HttpPost("editarOperaciones")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> EditarOperacion([FromBody] OperacionesXPrecioUnitarioDetalleDTO registro)
        {
            return await _precioUnitarioProceso.EditarOperacion(registro);
        }

        [HttpPost("eliminarOperacion")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> EliminarOperacion([FromBody] int Id)
        {
            return await _precioUnitarioProceso.EliminarOperacion(Id);
        }

        [HttpGet("obtenerOperaciones/{IdPrecioUnitarioDetalle:int}")]
        public async Task<ActionResult<List<OperacionesXPrecioUnitarioDetalleDTO>>> ObtenerOperaciones(int IdPrecioUnitarioDetalle)
        {
            return await _precioUnitarioProceso.ObtenerOperaciones(IdPrecioUnitarioDetalle);
        }

        [HttpGet("arreglarBimsa/{IdPrecioUnitarioDetalle:int}")]
        public async Task<ActionResult<List<OperacionesXPrecioUnitarioDetalleDTO>>> ActualizarCostos(int IdPrecioUnitarioDetalle)
        {
                var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno40Context(_Options.Options))
            {
                await _precioUnitarioProceso.ActualizarInsumosBimsa(2);
                await _precioUnitarioProceso.RecalcularArmadoPrecioUnitario(2, db);
            }
            return await _precioUnitarioProceso.ObtenerOperaciones(IdPrecioUnitarioDetalle);
        }

        //[HttpPost("correccionBimsa")]
        //public async Task CorreccionBimsa()
        //{
        //    await _precioUnitarioProceso.CorreccionBimsa();
        //}
    }

}
