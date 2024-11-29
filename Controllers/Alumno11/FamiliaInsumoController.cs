using ERP_TECKIO.DTO;

using Microsoft.AspNetCore.Mvc;



using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de las familias de Insumo que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/familiainsumo/11")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionFamiliaInsumo-Empresa11")]
    public class FamiliaInsumoAlumno11Controller : ControllerBase
    {
        private readonly FamiliaInsumoProceso<Alumno11Context> _FamiliaInsumoProceso;
        public FamiliaInsumoAlumno11Controller(
            FamiliaInsumoProceso<Alumno11Context> familiaInsumoProceso)
        {
            _FamiliaInsumoProceso = familiaInsumoProceso;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearFamiliaInsumo-Empresa11")]
        public async Task<ActionResult> Post([FromBody] FamiliaInsumoCreacionDTO parametros)
        {
            await _FamiliaInsumoProceso.Post(parametros);
            return NoContent();
        }
        /// <summary>
        /// Endpoit del tipo Put que accede al metodo "editaFamiliaInsumoAsync" y permite editar
        /// una Familia de Insumo cuyo Id coincIda con el Id recibIdo en el objeto recibIdo como 
        /// parametro del tipo FamiliaInsumoDTO y luego asignando el resto de valores al registro
        /// </summary>
        /// <param name="parametros">Objeto del tipo FamiliaInsumoDTO que contiene todos los datos del registro</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarFamiliaInsumo-Empresa11")]
        public async Task<ActionResult> Put([FromBody] FamiliaInsumoDTO parametros)
        {
            try
            {
                await _FamiliaInsumoProceso.Put(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
        /// <summary>
        /// Endpoint del tipo Delete que accede al método "eliminaFamiliaInsumoAsync" 
        /// y permite eliminar un insumo cuyo Id coincIda con el Id recibIdo como 
        /// parametro, en el caso de que exista el insumo se elimina
        /// </summary>
        /// <param name="Id">Id del registro a eliminar</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarFamiliaInsumo-Empresa11")]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                await _FamiliaInsumoProceso.Delete(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
        /// <summary>
        /// Endpoint del tipo Get que accede al método "obtenFamiliInsumoAsync" para obtener todos los registros 
        /// y los regresa paginados conforme a los parametros de paginación recibIdos como parametro
        /// </summary>
        /// <param name="paginacionDTO">Parametros de paginación</param>
        /// <returns>Lista de registros de la tabla de FamiliaInsumos paginados</returns>
        [HttpGet("todos")]
        public async Task<ActionResult<List<FamiliaInsumoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            return await _FamiliaInsumoProceso.Get();
        }

        /// <summary>
        /// Endpoint del tipo Get que accede al método "obtenFamiliaInsumoAsync" para obtener
        /// todos los registros sin paginar
        /// </summary>
        /// <returns>Lista con los registros sin paginar</returns>
        [HttpGet("sinpaginar")]
        public async Task<ActionResult<List<FamiliaInsumoDTO>>> Get()
        {
            return await _FamiliaInsumoProceso.GetSinPaginar();
        }

        /// <summary>
        /// Endponint del tipo Get que accede al método "obtenFamiliaInsumoXId" para obtener
        /// un registro cuyo Id sea igual al recibIdo como parametro
        /// </summary>
        /// <param name="Id">Id del registro que se quiere obtener</param>
        /// <returns>Registro cuyo Id coincIda con el Id recibIdo como parametro</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<FamiliaInsumoDTO>> Get(int Id)
        {
            return await _FamiliaInsumoProceso.GetXId(Id);
        }
    }
}
