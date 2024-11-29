

using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using ERP_TECKIO;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;



namespace SistemaERP.API.Alumno44Controllers.Procomi
{

    [Route("api/almacen/44")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlmacenAlumno44Controller : ControllerBase
    {
        private readonly IAlmacenService<Alumno44Context> _almacenServicio;

        public AlmacenAlumno44Controller(
            IAlmacenService<Alumno44Context> almacenServicio)
        {
            _almacenServicio = almacenServicio;
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
        public async Task<ActionResult<RespuestaDTO>> Editar([FromBody] AlmacenDTO Edit)
        {
            return await _almacenServicio.Editar(Edit);
        }

    }
}
