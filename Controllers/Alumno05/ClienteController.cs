using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    [Route("api/cliente/5")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClienteAlumno05Controller : ControllerBase
    {
        private readonly IClientesService<Alumno05Context> _Service;

        private readonly ClienteProceso<Alumno05Context> _Proceso;
        public ClienteAlumno05Controller(
            IClientesService<Alumno05Context> service
            , ClienteProceso<Alumno05Context> proceso)
        {
            _Service = service;
            _Proceso = proceso;
        }

        [HttpPost]
        [Route("GuardarCliente")]
        public async Task<ActionResult<RespuestaDTO>> GuardarCliente([FromBody] ClienteDTO parametros)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (!await _Service.Crear(parametros))
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se guardaron los datos";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "El cliente se guardo correctamente";
            return respuesta;
        }

        [HttpPost]
        [Route("GuardarYObtenerCliente")]
        public async Task<ActionResult<ClienteDTO>> GuardarYObtenerCliente([FromBody] ClienteDTO parametros)
        {
            return await _Service.CrearYObtener(parametros);
        }

        [HttpPut]
        [Route("EditarCliente")]
        public async Task<ActionResult<RespuestaDTO>> EditarCliente([FromBody] ClienteDTO parametros)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (!await _Service.Editar(parametros))
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se actualizaron los datos";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "El cliente se actualizó correctamente";
            return respuesta;
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<RespuestaDTO>> Delete(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (!await _Service.Eliminar(Id))
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Error, el cliente no se elimino";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "El cliente ha sido elimino";
            return respuesta;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<ClienteDTO>>> todos()
        {
            return await _Service.ObtenTodos();
        }
    }
}
