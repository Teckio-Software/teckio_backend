using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los insumos por contratista que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/insumoxcontratista/40")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionInsumoXContratista-Empresa1")]
    public class InsumoXContratistaAlumno40Controller : ControllerBase
    {
        /// <summary>
        /// Para mostrar los errores en consola
        /// </summary>
        private readonly ILogger<InsumoXContratistaAlumno40Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>  
        private readonly Alumno40Context Context;
        /// <summary>
        /// Constructor del controlador de los insumos por contratista
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public InsumoXContratistaAlumno40Controller(
            ILogger<InsumoXContratistaAlumno40Controller> logger,
            Alumno40Context context)
        {
            Logger = logger;
            Context = context;
        }
        ///// <summary>
        ///// Obtiene un listado de la relación de insumos por contratista de manera paginada
        ///// </summary>
        ///// <param name="paginacionDTO">Un objeto del tipo <see cref="PaginacionDTO"/></param>
        ///// <param name="IdContratista">Identificador único del contratista</param>
        ///// <returns>Una lista de objetos del tipo <see cref="InsumoXContratistaDTO"/></returns>
        //[HttpGet("todos/{IdContratista:int}")]
        //public async Task<ActionResult<List<InsumoXContratistaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdContratista)
        //{
        //    var queryable = InsumoXContratistaSP.obtenInsumosXIdContratista(IdContratista).Result.AsQueryable();
        //    await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
        //    var lista = queryable.OrderBy(x => x.Codigo).Paginar(paginacionDTO).ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}
        ///// <summary>
        ///// Obtiene un listado entre la relación de los insumos por contratista
        ///// </summary>
        ///// <param name="IdContratista">Identificador único del contratista</param>
        ///// <returns>Una lista de objetos del tipo <see cref="InsumoXContratistaDTO"/></returns>
        //[HttpGet("sinpaginar/{IdContratista:int}")]
        //public async Task<ActionResult<List<InsumoXContratistaDTO>>> getInsumosXContratistaCatalogo(int IdContratista)
        //{
        //    var queryable = InsumoXContratistaSP.obtenInsumosXIdContratista(IdContratista).Result.AsQueryable();
        //    var lista = queryable.OrderBy(x => x.Codigo).ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}

        ///// <summary>
        ///// EndPoint para obtener un objeto del tipo <see cref="InsumoXContratistaDTO"/>
        ///// </summary>
        ///// <param name="Id">Identificador único de la relación entre el insumo y el contratista</param>
        ///// <returns>Un solo objeto del tipo <see cref="InsumoXContratistaDTO"/></returns>
        //[HttpGet("{Id:int}")]
        //public async Task<ActionResult<InsumoXContratistaDTO>> getInsumoXContratista(int Id) //recibe un Id para ejecutar la acción
        //{
        //    var lista = InsumoXContratistaSP.obtenInsumoXContratistaXIdInsumoContratista(Id).Result.ToList();
        //    if (lista.Count <= 0) { return NoContent(); }
        //    return lista.FirstOrDefault()!;
        //}

        ///// <summary>
        ///// EndPoint para crear una nueva relación de un insumo por contratista
        ///// </summary>
        ///// <param name="Edita">Un objeto del tipo <see cref="InsumoXContratistaCreacionDTO"/></param>
        ///// <returns>Un código de <see cref="NoContentResult"/></returns>
        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] InsumoXContratistaCreacionDTO CreacionDTO) //Recibe los parametros para la creación
        //{
        //    var queryable = InsumoXContratistaSP.obtenInsumosXIdContratista(CreacionDTO.IdContratista).Result.AsQueryable();
        //    var lista = queryable.Where(z => z.IdInsumo == CreacionDTO.IdInsumo).ToList();
        //    if (lista.Count > 0)
        //    {
        //        return NoContent();
        //    }
        //    await InsumoXContratistaSP.creaInsumoXContratista(CreacionDTO);
        //    return NoContent();
        //}

        ///// <summary>
        ///// EndPoint para editar la relación de un insumo por contratista (costo)
        ///// </summary>
        ///// <param name="Edita">Un objeto del tipo <see cref="InsumoXContratistaDTO"/></param>
        ///// <returns>Un código de <see cref="NoContentResult"/></returns>
        //[HttpPut]
        //public async Task<ActionResult> Put([FromBody] InsumoXContratistaDTO Edita)
        //{
        //    var lista = InsumoXContratistaSP.obtenInsumoXContratistaXIdInsumoContratista(Edita.Id).Result.ToList();
        //    if (lista.Count <= 0) { return NoContent(); }
        //    await InsumoXContratistaSP.editaInsumoXContratista(Edita);
        //    return NoContent();
        //}

        ///// <summary>
        ///// EndPoint para eliminar la relación entre un insumo por contratista
        ///// </summary>
        ///// <param name="Id">Identificador único de la relación entre el insumo por contratista</param>
        ///// <returns>Un código de <see cref="NoContentResult"/></returns>
        //[HttpDelete("{Id:int}")]
        //public async Task<ActionResult> Delete(int Id)
        //{
        //    var lista = InsumoXContratistaSP.obtenInsumoXContratistaXIdInsumoContratista(Id).Result.ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    await InsumoXContratistaSP.eliminaInsumoXContratista(Id);
        //    return NoContent();
        //}
    }
}
