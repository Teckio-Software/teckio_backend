using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;




namespace SistemaERP.API.Alumno22Controllers.Procomi
{
    [Route("api/preciounitariodetalle/22")]
    [ApiController]
    public class PrecioUnitarioDetalleAlumno22Controller : ControllerBase
    {
        private readonly PrecioUnitarioProceso<Alumno22Context> _precioUnitarioProceso;
        private readonly DbContextOptionsBuilder<Alumno22Context> _Options;
        public PrecioUnitarioDetalleAlumno22Controller(
            PrecioUnitarioProceso<Alumno22Context> precioUnitarioProceso,
            DbContextOptionsBuilder<Alumno22Context> options
            )
        {
            _precioUnitarioProceso = precioUnitarioProceso;
            _Options = options;
        }

        [HttpGet("todos/{IdPrecioUnitario:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> Obtener(int IdPrecioUnitario)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno22Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.ObtenerDetalles(IdPrecioUnitario, db);
            }
            return registros;
        }

        [HttpGet("todosFiltrado/{IdPrecioUnitario:int}/{IdTipoInsumo}")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> ObtenerFiltrado(int IdPrecioUnitario, int IdTipoInsumo)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno22Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.ObtenerDetallesPorTipoInsumo(IdPrecioUnitario, IdTipoInsumo, db);
            }
            return registros;
        }

        [HttpPost("obtenhijos")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> ObtenerHijos([FromBody] PrecioUnitarioDetalleDTO registro)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno22Context(_Options.Options))
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

        [HttpPost("eliminar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> Eliminar([FromBody] int Id)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno22Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.EliminarDetalle(Id, db);
            }
            return registros;
        }

        //[HttpPost("correccionBimsa")]
        //public async Task CorreccionBimsa()
        //{
        //    await _precioUnitarioProceso.CorreccionBimsa();
        //}
    }

}
