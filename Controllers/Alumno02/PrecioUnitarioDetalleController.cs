using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    [Route("api/preciounitariodetalle/2")]
    [ApiController]
    public class PrecioUnitarioDetalleAlumno02Controller : ControllerBase
    {
        private readonly PrecioUnitarioProceso<Alumno02Context> _precioUnitarioProceso;
        private readonly DbContextOptionsBuilder<Alumno02Context> _Options;
        public PrecioUnitarioDetalleAlumno02Controller(
            PrecioUnitarioProceso<Alumno02Context> precioUnitarioProceso,
            DbContextOptionsBuilder<Alumno02Context> options
            )
        {
            _precioUnitarioProceso = precioUnitarioProceso;
            _Options = options;
        }

        [HttpGet("todos/{IdPrecioUnitario:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> Obtener(int IdPrecioUnitario)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno02Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.ObtenerDetalles(IdPrecioUnitario, db);
            }
            return registros;
        }

        [HttpGet("todosFiltrado/{IdPrecioUnitario:int}/{IdTipoInsumo}")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> ObtenerFiltrado(int IdPrecioUnitario, int IdTipoInsumo)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno02Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.ObtenerDetallesPorTipoInsumo(IdPrecioUnitario, IdTipoInsumo, db);
            }
            return registros;
        }

        [HttpPost("obtenhijos")]
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> ObtenerHijos([FromBody] PrecioUnitarioDetalleDTO registro)
        {
            var registros = new List<PrecioUnitarioDetalleDTO>();
            using (var db = new Alumno02Context(_Options.Options))
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
            using (var db = new Alumno02Context(_Options.Options))
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
