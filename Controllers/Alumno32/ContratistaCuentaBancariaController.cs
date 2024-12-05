


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de las cuentas bancarias de los contratistas que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/contratistacuentabancaria/32")]
    [ApiController]
    public class ContratistaCuentaBancariaAlumno32Controller : ControllerBase
    {
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<ContratistaCuentaBancariaAlumno32Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno32Context Context;
        /// <summary>
        /// Constructor del controlador de las cuentas bancarias del contratista
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public ContratistaCuentaBancariaAlumno32Controller(
            ILogger<ContratistaCuentaBancariaAlumno32Controller> logger,
            Alumno32Context context)
        {
            Logger = logger;
            Context = context;
        }

        ///// <summary>
        ///// Método del controlador que ejecuta el Método para obtener los registros de la tabla de concepto
        ///// </summary>
        ///// <param name="paginacionDTO">Numero de pagina y Cantidad de registros</param>
        ///// <returns>Lista de los conceptos</returns>
        //[HttpGet("todos/{IdContratista:int}")]
        //public async Task<ActionResult<List<ContratistaCuentaBancariaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdContratista)
        //{
        //    var queryable = ContratistaCuentaBancariaSP.obtenContratistaCuentasBancariasAsync(IdContratista).Result.AsQueryable();
        //    await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
        //    var lista = queryable.Paginar(paginacionDTO).ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}

        ///// <summary>
        ///// Método del controlador que ejecuta el Método para obtener los registros de la tabla de concepto pero sin paginar
        ///// </summary>
        ///// <returns>Lista de conceptos sin paginar</returns>
        //[HttpGet("sinpaginar/{IdContratista:int}")]
        //public async Task<ActionResult<List<ContratistaCuentaBancariaDTO>>> GetContratistasPorEmpresa(int IdContratista)
        //{
        //    var queryable = ContratistaCuentaBancariaSP.obtenContratistaCuentasBancariasAsync(IdContratista).Result.AsQueryable();
        //    var lista = queryable.ToList();
        //    if (lista.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    return lista;
        //}

        ///// <summary>
        ///// Método del controlador que ejecuta el Método para obtener un registro a partir de un Id dado
        ///// </summary>
        ///// <param name="Id">Id</param>
        ///// <returns>Registro especifico a partir del Id</returns>
        //[HttpGet("{Id:int}")]
        //public async Task<ActionResult<ContratistaCuentaBancariaDTO>> GetContratista(int Id) //recibe un Id para ejecutar la acción
        //{
        //    var queryable = ContratistaCuentaBancariaSP.obtenCuentaBancariaXIdAsync(Id).Result.AsQueryable();
        //    var lista = queryable.Where(z => z.Id == Id).ToList();
        //    if (lista.Count <= 0) { return NoContent(); }
        //    return lista.FirstOrDefault()!;
        //}
        ////montoPorAfectarAlMovimiento
        ///// <summary>
        ///// Método del controlador que ejecuta el Método que permite crear un registro en la tabla
        ///// </summary>
        ///// <param name="CreacionDTO">Descripción, Unidad, Detalle y Agrupación</param>
        ///// <returns>NoContent</returns>
        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] ContratistaCuentaBancariaCreacionDTO CreacionDTO) //Recibe los parametros para la creación
        //{
        //    var queryable = ContratistaCuentaBancariaSP.obtenContratistaCuentasBancariasAsync(CreacionDTO.IdContratista).Result.AsQueryable();
        //    var lista = queryable.Where(z => z.Clabe == CreacionDTO.Clabe).ToList();
        //    if (lista.Count > 0)
        //    {
        //        return NoContent();
        //    }
        //    await ContratistaCuentaBancariaSP.creaNuevaCuentaBancariaAsync(CreacionDTO);
        //    return NoContent();
        //}

        ///// <summary>
        ///// Método del controlador que ejecuta el Método para editar un registro en tabla
        ///// </summary>
        ///// <param name="Edita">Id, Descripción, Unidad, Detalle y Agrupación</param>
        ///// <returns>NoContent</returns>
        //[HttpPut]
        //public async Task<ActionResult> Put([FromBody] ContratistaCuentaBancariaDTO Edita)
        //{
        //    var lista = ContratistaCuentaBancariaSP.obtenContratistaCuentasBancariasAsync(Edita.IdContratista).Result.ToList();
        //    if (lista.Count <= 0) { return NoContent(); }
        //    await ContratistaCuentaBancariaSP.editaCuentaBancaria(Edita);
        //    return NoContent();
        //}

        ///// <summary>
        ///// Método del controlador que ejecuta el Método para eliminar un registro en tabla
        ///// </summary>
        ///// <param name="Id">Id</param>
        ///// <returns>NoContent</returns>
        //[HttpDelete("{Id:int}")]
        //public async Task<ActionResult> Delete(int Id)
        //{
        //    var lista = ContratistaCuentaBancariaSP.obtenCuentaBancariaXIdAsync(Id).Result.ToList();
        //    if (lista.Count <= 0) { return NoContent(); }
        //    await ContratistaCuentaBancariaSP.eliminaCuentaBancaria(Id);
        //    return NoContent();
        //}
    }
}
