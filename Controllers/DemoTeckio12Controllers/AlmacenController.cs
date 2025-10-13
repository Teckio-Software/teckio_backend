using ERP_TECKIO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers
{


    [Route("api/almacen/12")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlmacenDemoTeckioAL12Controller : ControllerBase
    {
        private readonly IAlmacenService<DemoTeckioAL12Context> _almacenServicio;
        private readonly AlmacenProceso<DemoTeckioAL12Context> _almacenProceso;

        public AlmacenDemoTeckioAL12Controller(
            IAlmacenService<DemoTeckioAL12Context> almacenServicio, AlmacenProceso<DemoTeckioAL12Context> almacenProceso)
        {
            _almacenServicio = almacenServicio;
            _almacenProceso = almacenProceso;
        }

        [HttpPost("Crear")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> CreaAlmacen([FromBody] AlmacenCreacionDTO CreacionDTO)
        {
            return await _almacenServicio.Crear(CreacionDTO);
        }

        [HttpGet("ObtenXIdProyecto/{IdProyecto:int}")]
        public async Task<ActionResult<List<AlmacenDTO>>> GetAlmacenPaginado(int IdProyecto)
        {
            return await _almacenServicio.ObtenerXIdProyecto(IdProyecto);
        }

        [HttpGet("ObtenTodos")]
        public async Task<ActionResult<List<AlmacenDTO>>> ObtenTodos()
        {

            return await _almacenServicio.ObtenTodos();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<AlmacenDTO>> GetAlmacenXId(int Id)
        {
            return await _almacenServicio.ObtenXId(Id);
        }

        [HttpDelete]
        public async Task<ActionResult<RespuestaDTO>> Delete(int id)
        {
            
            return await _almacenServicio.Eliminar(id);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar([FromBody]AlmacenDTO Edit)
        {
            return await _almacenServicio.Editar(Edit);
        }
        [HttpGet("ObtenTodosConProyecto")]
        public async Task<ActionResult<List<AlmacenDTO>>> ObtenTodosConProyecto()
        {
            return await _almacenProceso.ObtenerConNombresProyecto();
        }
    }
}
