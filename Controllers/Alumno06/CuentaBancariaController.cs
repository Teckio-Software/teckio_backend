using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de las cuentas bancarias de la empresa que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/cuentabancaria/6")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaAlumno06Controller : ControllerBase
    {
        private readonly CuentaBancariaProceso<Alumno06Context> _proceso;

        private readonly ICuentaBancariaService<Alumno06Context> _servicio;
        public CuentaBancariaAlumno06Controller(CuentaBancariaProceso<Alumno06Context> _proceso, ICuentaBancariaService<Alumno06Context> _servicio
           )
        {
            this._proceso = _proceso;
            this._servicio = _servicio;
        }
        /// <summary>
        /// Obtiene las cuentas bancarias de las empresas
        /// </summary>
        /// <param name="paginacionDTO">Un objeto del tipo <see cref="PaginacionDTO"/></param>
        /// <returns>Una lista de objetos del tipo <see cref="CuentaBancariaEmpresaDTO"/> de manera paginada</returns>
        [HttpGet("ObtenTodos")]
        public async Task<ActionResult<List<CuentaBancariaDTO>>> Get([FromQuery] CuentaBancariaDTO cuentabancaria)
        {
            return await _proceso.ObtenerNombresCunetas();
        }
        /// <summary>
        /// Obtiene una cuenta bancaria de una empresa
        /// </summary>
        /// <param name="Id">Identificador único de la cuenta bancaria de la empresa</param>
        /// <returns>Un objeto de tipo <see cref="CuentaBancariaEmpresaDTO"/></returns>
        [HttpGet("ObtenerXId/{Id:int}")]
        public async Task<ActionResult<CuentaBancariaDTO>> GetXId(int Id)
        {
            return await _servicio.ObtenXId(Id);
        }
        [HttpGet("ObtenerXIdContratista/{IdContratista:int}")]
        public async Task<ActionResult<List<CuentaBancariaDTO>>> GetObtenerXIdContratistaXId(int IdContratista)
        {
            return await _proceso.ObtenerXIdContratista(IdContratista);
        }

        [HttpPost]
        [Route("GuardarCuentaBancaria")]
        public async Task<ActionResult<bool>> GuardarCuentaBancaria([FromBody] CuentaBancariaDTO cuentaBancaria)
        {
            return await _servicio.Crear(cuentaBancaria);
        }

        [HttpPost]
        [Route("GuardarYObtenCuentaBancaria")]
        public async Task<ActionResult<CuentaBancariaDTO>> GuardarYObtenCuentaBancaria([FromBody] CuentaBancariaDTO cuentaBancaria)
        {
            return await _servicio.CrearYObtener(cuentaBancaria);
        }

        [HttpPut]
        [Route("EditarCuentaBancaria")]
        public async Task<ActionResult<RespuestaDTO>> EditarCuentaBancaria([FromBody] CuentaBancariaDTO cuentaBancaria)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (await _servicio.Editar(cuentaBancaria))
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
        public async Task<ActionResult<RespuestaDTO>> EliminarCuentaBancaria(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (await _servicio.Eliminar(id))
            {
                respuesta.Estatus = true;
                respuesta.Descripcion = "La cuenta bancaria se ha sido eliminado";
                return respuesta;
            }
            else
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "La cuenta bancaria no se ha eliminado";
                return respuesta;
            }
        }
    }
}
