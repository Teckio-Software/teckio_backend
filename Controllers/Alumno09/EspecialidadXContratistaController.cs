using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de las especialIdades por contratista que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/especialIdadxcontratista/9")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionEspecialidadContratista-Empresa9")]
    public class EspecialIdadXContratistaAlumno09Controller : ControllerBase
    {
        /// <summary>
        /// Para mostrar los errores en consola
        /// </summary>
        private readonly ILogger<EspecialIdadXContratistaAlumno09Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno09Context Context;
        /// <summary>
        /// Constructor del controlador de las especialIdades por contratista
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public EspecialIdadXContratistaAlumno09Controller(
            ILogger<EspecialIdadXContratistaAlumno09Controller> logger,
            Alumno09Context context)
        {
            Logger = logger;
            Context = context;
        }

        ///// <summary>
        ///// EndPoint para obtener las especialIdades de cada contratista de manera paginada
        ///// </summary>
        ///// <param name="IdContratista">Identificador único del contratista</param>
        ///// <param name="paginacionDTO">Objeto del tipo <see cref="PaginacionDTO"/></param>
        ///// <returns>Una lista de objetos del tipo <see cref="EspecialIdadXContratistaDTO"/></returns>
        //[HttpGet("todos/{IdContratista:int}")]
        //public async Task<ActionResult<List<EspecialIdadXContratistaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdContratista)
        //{
        //    var queryable = EspecialIdadXContratistaSP.obtenEspecialIdadesXIdContratista(IdContratista).Result.AsQueryable();
        //    await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
        //    var lista = queryable.OrderBy(x => x.Codigo).Paginar(paginacionDTO).ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}
        ///// <summary>
        ///// EndPoint para obtener las especialIdades por contratista
        ///// </summary>
        ///// <param name="IdContratista">Identificador único del contratista</param>
        ///// <returns>Una lista de objetos del tipo <see cref="EspecialIdadXContratistaDTO"/></returns>
        //[HttpGet("sinpaginar/{IdContratista:int}")]
        //public async Task<ActionResult<List<EspecialIdadXContratistaDTO>>> getEspecialIdadesXContratista(int IdContratista)
        //{
        //    var queryable = EspecialIdadXContratistaSP.obtenEspecialIdadesXIdContratista(IdContratista).Result.AsQueryable();
        //    var lista = queryable.OrderBy(x => x.Codigo).ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}

        ///// <summary>
        ///// EndPoint para obtener una relación entre una especialIdad con un contratista
        ///// </summary>
        ///// <param name="Id">Identificador único de la relación entre la especialIdad y el contratista</param>
        ///// <returns>Un solo objeto del tipo <see cref="EspecialIdadXContratistaDTO"/></returns>
        //[HttpGet("{Id:int}")]
        //public async Task<ActionResult<EspecialIdadXContratistaDTO>> getEspecialIdadXContratista(int Id) //recibe un Id para ejecutar la acción
        //{
        //    var queryable = EspecialIdadXContratistaSP.obtenEspecialIdadXContratistaXIdEspecialIdadContratista(Id).Result.AsQueryable();
        //    var lista = queryable.Where(z => z.Id == Id).ToList();
        //    if (lista.Count <= 0) { return NoContent(); }
        //    return lista.FirstOrDefault()!;
        //}

        ///// <summary>
        ///// EndPoint para crear una nueva relación de una especialIdad por contratista
        ///// </summary>
        ///// <param name="Edita">Un objeto del tipo <see cref="EspecialIdadXContratistaCreacionDTO"/></param>
        ///// <returns>Un código de <see cref="NoContentResult"/></returns>
        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] EspecialIdadXContratistaCreacionDTO CreacionDTO) //Recibe los parametros para la creación
        //{
        //    var queryable = EspecialIdadXContratistaSP.obtenEspecialIdadesXIdContratista(CreacionDTO.IdContratista).Result.AsQueryable();
        //    var lista = queryable.Where(z => z.IdContratista == CreacionDTO.IdEspecialIdad).ToList();
        //    if (lista.Count > 0)
        //    {
        //        return NoContent();
        //    }
        //    await EspecialIdadXContratistaSP.creaEspecialIdadXContratista(CreacionDTO);
        //    return NoContent();
        //}

        ///// <summary>
        ///// EndPoint para editar la relación de una especialIdad por contratista (costo)
        ///// </summary>
        ///// <param name="Edita">Un objeto del tipo <see cref="EspecialIdadXContratistaDTO"/></param>
        ///// <returns>Un código de <see cref="NoContentResult"/></returns>
        //[HttpPut]
        //public async Task<ActionResult> Put([FromBody] EspecialIdadXContratistaDTO Edita)
        //{
        //    var lista = EspecialIdadXContratistaSP.obtenEspecialIdadXContratistaXIdEspecialIdadContratista(Edita.Id).Result.ToList();
        //    if (lista.Count <= 0) { return NoContent(); }
        //    await EspecialIdadXContratistaSP.editaEspecialIdadXContratista(Edita);
        //    return NoContent();
        //}

        ///// <summary>
        ///// EndPoint para eliminar la relación entre una especialIdad por contratista
        ///// </summary>
        ///// <param name="Id">Identificador único de la relación entre la especialIdad por contratista</param>
        ///// <returns>Un código de <see cref="NoContentResult"/></returns>
        //[HttpDelete("{Id:int}")]
        //public async Task<ActionResult> Delete(int Id)
        //{
        //    var lista = EspecialIdadXContratistaSP.obtenEspecialIdadXContratistaXIdEspecialIdadContratista(Id).Result.ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    await EspecialIdadXContratistaSP.eliminaEspecialIdadXContratista(Id);
        //    return NoContent();
        //}
    }
}
