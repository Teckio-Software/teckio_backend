

using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los conceptos que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/compradirecta/28")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionOrdenCompra-Empresa28")]
    public class CompraDirectaAlumno28Controller : ControllerBase
    {
        private readonly ICompraDirectaService<Alumno28Context> _Service;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<CompraDirectaAlumno28Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno28Context Context;
        /// <summary>
        /// Constructor del controlador de Conceptos
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public CompraDirectaAlumno28Controller(
            ILogger<CompraDirectaAlumno28Controller> logger,
            Alumno28Context context
            , ICompraDirectaService<Alumno28Context> Service)
        {
            Logger = logger;
            Context = context;
            _Service = Service;
        }

        /// <summary>
        /// Método para obtener los registros de la tabla de concepto
        /// </summary>
        /// <param name="paginacionDTO">Numero de pagina y Cantidad de registros</param>
        /// <returns>Lista de los conceptos</returns>
        [HttpGet("todos/{IdRequisicion:int}")]
        public async Task<ActionResult<List<CompraDirectaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdRequisicion)
        {
            var lista = await _Service.ObtenXIdRequisicion(IdRequisicion);
            var queryable = lista.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var listaResult = queryable.OrderByDescending(z => z.Id).Paginar(paginacionDTO).ToList();
            if (listaResult.Count <= 0)
            {
                return NoContent();
            }
            return listaResult;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para obtener los registros de la tabla de concepto pero sin paginar
        /// </summary>
        /// <returns>Lista de conceptos sin paginar</returns>
        [HttpGet("sinpaginar/{IdRequisicion:int}")]
        public async Task<ActionResult<List<CompraDirectaDTO>>> ObtenXIdRequision(int IdRequisicion)
        {
            var lista = await _Service.ObtenXIdRequisicion(IdRequisicion);
            var queryable = lista.AsQueryable();
            var listaResult = queryable.OrderByDescending(z => z.Id).ToList();
            if (listaResult.Count <= 0)
            {
                return NoContent();
            }
            return listaResult;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para obtener un registro a partir de un Id dado
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Registro especifico a partir del Id</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CompraDirectaDTO>> ObtenXId(int Id) //recibe un Id para ejecutar la acción
        {
            var objeto = await _Service.ObtenXId(Id);
            return objeto;
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método que permite crear un registro en la tabla
        /// </summary>
        /// <param name="CreacionDTO">Descripción, Unidad, Detalle y Agrupación</param>
        /// <returns>NoContent</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearOrdenCompra-Empresa28")]
        public async Task<ActionResult> Post([FromBody] CompraDirectaCreacionDTO CreacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(CreacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno28Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }

            return NoContent();
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para editar un registro en tabla
        /// </summary>
        /// <param name="Edita">Id, Descripción, Unidad, Detalle y Agrupación</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarOrdenCompra-Empresa28")]
        public async Task<ActionResult> Put([FromBody] CompraDirectaDTO Edita)
        {
            try
            {
                var resultado = await _Service.Editar(Edita);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno28Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        /// <summary>
        /// Método del controlador que ejecuta el Método para editar un registro en tabla
        /// </summary>
        /// <param name="Edita">Id, Descripción, Unidad, Detalle y Agrupación</param>
        /// <returns>NoContent</returns>
        [HttpPut("cancelar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarOrdenCompra-Empresa28")]
        public async Task<ActionResult> PutCancelar([FromBody] CompraDirectaDTO Edita)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusCancelar(Edita.Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno28Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
