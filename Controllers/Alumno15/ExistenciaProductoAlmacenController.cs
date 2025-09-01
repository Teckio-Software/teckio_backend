using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno15
{

    [Route("api/existenciaproductoalmacen/15")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExistenciaProductoAlmacenController: ControllerBase
    {
        private readonly ExistenciaProductoAlmacenProceso<Alumno15Context> _proceso;

        public ExistenciaProductoAlmacenController(ExistenciaProductoAlmacenProceso<Alumno15Context> proces)
        {
            _proceso = proces;
        }

        //[HttpPost("crear")]
        //public async Task<RespuestaDTO> Crear(ExistenciaProductoAlmacenDTO parametro)
        //{
        //    var resultado = await _proceso.Crear(parametro);
        //    return resultado;
        //}

        //[HttpPost("crearYObtener")]
        //public async Task<ExistenciaProductoAlmacenDTO> CrearYObtener(ExistenciaProductoAlmacenDTO parametro)
        //{
        //    var resultado = await _proceso.CrearYObtener(parametro);
        //    return resultado;
        //}

        //[HttpPut("editar")]
        //public async Task<RespuestaDTO> Editar(ExistenciaProductoAlmacenDTO parametro)
        //{
        //    var resultado = await _proceso.Editar(parametro);
        //    return resultado;
        //}

        //[HttpDelete("eliminar")]
        //public async Task<RespuestaDTO> Eliminar(int parametro)
        //{
        //    var resultado = await _proceso.Eliminar(parametro);
        //    return resultado;
        //}
    }
}
