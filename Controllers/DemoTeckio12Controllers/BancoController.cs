using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/Banco/12")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BancoDemoTeckioAL12Controller : ControllerBase 
    {
        private readonly IBancoService<DemoTeckioAL12Context> _bancoService;

        public BancoDemoTeckioAL12Controller(IBancoService<DemoTeckioAL12Context> bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<ActionResult<List<BancoDTO>>> ObtenerBancos()
        {
            return await _bancoService.ObtenTodos();
        }

        [HttpGet]
        [Route("ObtenerXId/{id:int}")]
        public async Task<ActionResult<BancoDTO>> ObtenerXId(int id)
        {
            return await _bancoService.ObtenXId(id);
        }

        [HttpGet]
        [Route("ObtenerXClave/{clave}")]
        public async Task<ActionResult<BancoDTO>> ObtenerXClave(string clave)
        {
            return await _bancoService.ObtenXClave(clave);
        }

        [HttpPost]
        [Route("GuardarBanco")]
        public async Task<ActionResult<RespuestaDTO>> GuardarBanco([FromBody] BancoDTO banco) {
            RespuestaDTO respuesta = new RespuestaDTO();
            if(await _bancoService.Crear(banco))
            {
                respuesta.Estatus = true;
                respuesta.Descripcion = "Datos del banco guardados correctamente";
                return respuesta;
            }
            else
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se guardaron los datos de banco";
                return respuesta;
            }
        }

        [HttpPost]
        [Route("GuardarYObtenerBanco")]
        public async Task<ActionResult<BancoDTO>> GuardarYObtenerBanco([FromBody] BancoDTO banco)
        {
            return await _bancoService.CrearYObtener(banco);
        }

        [HttpPut]
        [Route("EditarBanco")]
        public async Task<ActionResult<RespuestaDTO>> EditarBanco([FromBody] BancoDTO banco)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (await _bancoService.Editar(banco))
            {
                respuesta.Estatus = true;
                respuesta.Descripcion = "Los datos se actualizaron correctamente";
                return respuesta;
            }
            else
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Los datos no se actualizaron";
                return respuesta;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<RespuestaDTO>> EliminarBanco(int id) {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (await _bancoService.Eliminar(id))
            {
                respuesta.Estatus = true;
                respuesta.Descripcion = "El banco ha sido eliminado";
                return respuesta;
            }
            else
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El banco no se eliminado";
                return respuesta;
            }
        }
    }
}
