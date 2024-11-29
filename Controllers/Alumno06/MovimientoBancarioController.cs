using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los movimientos bancarios que hereda de <see cref="ControllerBase"/>
    /// </summary>
    //[Route("api/6/movimientobancario")]
    //[ApiController]
    //public class MovimientoBancarioAlumno06Controller : ControllerBase
    //{
    //    /// <summary>
    //    /// Para mostrar los errores en consola
    //    /// </summary>
    //    private readonly ILogger<MovimientoBancarioAlumno06Controller> Logger;
    //    /// <summary>
    //    /// Se usa para mandar en "headers" los registros totales de los registros
    //    /// </summary> 
    //    private readonly Alumno06Context Context;
    //    /// <summary>
    //    /// Constructor del controlador del movimiento bancario
    //    /// </summary>
    //    /// <param name="logger">Para mostrar errores en consola</param>
    //    /// <param name="context">Para mandar información de los registros</param>
    //    public MovimientoBancarioAlumno06Controller(
    //        ILogger<MovimientoBancarioAlumno06Controller> logger,
    //        Alumno06Context context)
    //    {
    //        Logger = logger;
    //        Context = context;
    //    }

    //    /// <summary>
    //    /// Para obtener todos los movimientos bancarios de manera paginada.
    //    /// </summary>
    //    /// <param name="paginacionDTO">Un objeto de tipo <see cref="PaginacionDTO"/></param>
    //    /// <param name="IdCuentaBancaria">El Identificador único de la <see cref="CuentaBancariaEmpresaDTO"/></param>
    //    /// <returns>Una lista de objetos del tipo <see cref="MovimientoBancarioDTO"/> de manera paginada</returns>
    //    [HttpGet("todos/{IdCuentaBancaria:int}")]
    //    public async Task<ActionResult<List<MovimientoBancarioDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdCuentaBancaria)
    //    {
    //        var queryable = MovimientoBancarioSP.obtenMovimientoBancarioAsync(IdCuentaBancaria).Result.AsQueryable();
    //        await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
    //        var lista = queryable.OrderBy(z => z.NoMovBancario).Paginar(paginacionDTO).ToList();
    //        if (lista.Count <= 0)
    //        {
    //            return NoContent();
    //        }
    //        return lista;
    //    }

    //    /// <summary>
    //    /// Para obtener todos los movimientos bancarios sin paginar.
    //    /// </summary>
    //    /// <param name="IdCuentaBancaria">Identificador de la cuenta bancaria</param>
    //    /// <returns>Una lista de objetos de <see cref="MovimientoBancarioDTO"/> sin paginar</returns>
    //    [HttpGet("sinpaginar/{IdCuentaBancaria:int}")]
    //    public async Task<ActionResult<List<MovimientoBancarioDTO>>> Get(int IdCuentaBancaria)
    //    {
    //        var queryable = MovimientoBancarioSP.obtenMovimientoBancarioAsync(IdCuentaBancaria).Result.AsQueryable();
    //        var lista = queryable.OrderBy(z => z.NoMovBancario).ToList();
    //        if (lista.Count <= 0)
    //        {
    //            return NoContent();
    //        }
    //        return lista;
    //    }

    //    /// <summary>
    //    /// Obtiene un movimiento bancario por su Identificador único
    //    /// </summary>
    //    /// <param name="Id">Identificador único del Identificador único</param>
    //    /// <returns>Un único objeto de tipo <see cref="MovimientoBancarioDTO"/></returns>
    //    [HttpGet("{Id:int}")]
    //    public async Task<ActionResult<MovimientoBancarioDTO>> GetXId(int Id)
    //    {
    //        var queryable = MovimientoBancarioSP.obtenMovimientoBancarioXIdAsync(Id).Result.AsQueryable();
    //        var lista = queryable.OrderBy(z => z.NoMovBancario).Where(z => z.Id == Id).ToList();
    //        if (lista.Count <= 0)
    //        {
    //            return NoContent();
    //        }
    //        return lista.FirstOrDefault()!;
    //    }

    //    /// <summary>
    //    /// Crea un nuevo movimiento bancario
    //    /// </summary>
    //    /// <param name="CreacionDTO">Un objeto de tipo <see cref="MovimientoBancarioCreacionDTO"/></param>
    //    /// <returns>Un código de <seealso cref="NoContentResult"/></returns>
    //    [HttpPost]
    //    public async Task<ActionResult> Post([FromBody] MovimientoBancarioCreacionDTO CreacionDTO)
    //    {
    //        var queryable = MovimientoBancarioSP.obtenMovimientoBancarioAsync(CreacionDTO.IdCuentaBancariaEmpresa).Result.AsQueryable();
    //        var UltimoMovimiento = 0;
    //        if (queryable.Count() > 0)
    //        {
    //            UltimoMovimiento = queryable.Select(row => row.NoMovBancario).Max();
    //        }

    //        //var lista = queryable.Where(z => z.NoMovBancario == UltimoMovimiento).ToList();
    //        //if (lista.Count > 0)
    //        //{
    //        //    return NoContent();
    //        //}
    //        CreacionDTO.NoMovBancario = UltimoMovimiento + 1;
    //        await MovimientoBancarioSP.creaNuevoMovimientoBancarioAsync(CreacionDTO);
    //        return NoContent();
    //    }

    //    /// <summary>
    //    /// Edita un movimiento bancario conforme a su Identificador único
    //    /// </summary>
    //    /// <param name="Edita">Un objeto de tipo <see cref="MovimientoBancarioDTO"/></param>
    //    /// <returns>Un código de <seealso cref="NoContentResult"/></returns>
    //    [HttpPut]
    //    public async Task<ActionResult> Put([FromBody] MovimientoBancarioDTO Edita)
    //    {
    //        var lista = MovimientoBancarioSP.obtenMovimientoBancarioXIdAsync(Edita.Id).Result.ToList();
    //        if (lista.Count <= 0)
    //        {
    //            return NoContent();
    //        }
    //        await MovimientoBancarioSP.editaMovimientoBancarioAsync(Edita);
    //        return NoContent();
    //    }

    //    /// <summary>
    //    /// Borra un movimiento bancario
    //    /// </summary>
    //    /// <param name="Id">Identificador único del <see cref="MovimientoBancarioDTO"/></param>
    //    /// <returns>Un código de <seealso cref="NoContentResult"/></returns>
    //    [HttpDelete]
    //    public async Task<ActionResult> Delete(int Id)
    //    {
    //        var lista = MovimientoBancarioSP.obtenMovimientoBancarioXIdAsync(Id).Result.ToList();
    //        if (lista.Count <= 0)
    //        {
    //            return NoContent();
    //        }
    //        await MovimientoBancarioSP.eliminaMovimientoBancarioAsync(Id);
    //        return NoContent();
    //    }
    //}


}
