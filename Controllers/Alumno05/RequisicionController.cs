using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador para las Requisiciones a las que accede el usuario
    /// </summary>
    [Route("api/requisicion/5")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RequisicionAlumno05Controller : ControllerBase
    {
        private readonly IRequisicionService<Alumno05Context> _RequisicionService;
        private readonly RequisicionProceso<Alumno05Context> _Proceso;
        private readonly ObjetoRequisicionProceso<Alumno05Context> _ObjetoRequisicionProceso;
        private readonly IProyectoService<Alumno05Context> _ProyectoService;
        //private readonly IMapper _mapper;
        /// <summary>
        /// Se usa para mostrar errores en la consola
        /// </summary>
        private readonly ILogger<RequisicionAlumno05Controller> _Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno05Context _Context;
        /// <summary>
        /// Constructor del controlador de Requisiciones
        /// </summary>
        /// <param name="Logger"></param>
        /// <param name="_Context"></param>
        public RequisicionAlumno05Controller(
            ILogger<RequisicionAlumno05Controller> Logger
            , Alumno05Context Context
            , IRequisicionService<Alumno05Context> RequisicionService
            , RequisicionProceso<Alumno05Context> Proceso
            , ObjetoRequisicionProceso<Alumno05Context> ObjetoRequisicionProceso
            , IProyectoService<Alumno05Context> ProyectoService
            )
        {
            _Logger = Logger;
            _Context = Context;
            _RequisicionService = RequisicionService;
            _Proceso = Proceso;
            _ObjetoRequisicionProceso = ObjetoRequisicionProceso;
            _ProyectoService = ProyectoService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CrearRequisicion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> Post([FromBody] RequisicionCreacionDTO parametro)
        {
            var authen = HttpContext.User;
            return await _Proceso.CrearRequisicion(parametro, authen.Claims.ToList());
        }

        [HttpPut]
        [Route("EditarRequisicion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> EditarRequisicion([FromBody] RequisicionDTO parametro)
        {
            return await _Proceso.EditarRequisicion(parametro);
        }

        [HttpGet]
        [Route("CrearObjetoRequisicion/{idRequisicion:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ObjetoRequisicionDTO>> CrearObjetoRequisicion(int IdRequisicion)
        {
            return await _ObjetoRequisicionProceso.CrearObjetoRequisicion(IdRequisicion);
        }

        [HttpGet]
        [Route("AutorizarTodos/{idRequisicion:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> AutorizarTodos(int idRequisicion)
        {
            return await _Proceso.AutorizarTodos(idRequisicion);
        }

        /// <summary>
        /// Endpoint del tipo Put que permite editar un registro de la tabla de Requisición, a partir del
        /// Id que recibe como parte del objeto que recibe como parametro, una vez que lo Identifica, edita
        /// estos valores en ese registro
        /// </summary>
        /// <param name="parametros">objeto del tipo <seealso cref="Requisicion"/> el cual contiene todos los
        /// parametros necesarios para editar el registro</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put([FromBody] RequisicionDTO parametros)
        {
            try
            {
                var resultado = await _RequisicionService.Editar(parametros);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno05Context>(resultado.Estatus, resultado.Descripcion);
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
        [HttpPut("autorizar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "AutorizarRequisicion-Empresa1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutAutorizar([FromBody] RequisicionDTO Edita)
        {
            try
            {
                var resultado = await _RequisicionService.ActualizarEstatusAutorizar(Edita.Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno05Context>(resultado.Estatus, resultado.Descripcion);
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
        [HttpPut("removerautorizacion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "RemoverAutorizacionRequisicion-Empresa1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutRemoverAutorizacion([FromBody] RequisicionDTO Edita)
        {
            try
            {
                var resultado = await _RequisicionService.ActualizarEstatusRemoverAutorizar(Edita.Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno05Context>(resultado.Estatus, resultado.Descripcion);
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarRequisicion-Empresa1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutCancelar([FromBody] RequisicionDTO Edita)
        {
            try
            {
                var resultado = await _RequisicionService.ActualizarEstatusCancelar(Edita.Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno05Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        /// <summary>
        /// Endpoint del tipo Get para llamar al metodo "obtenRequisicionAsync" que obtiene todos los registros
        /// cuyo cuyo IdProyecto coincIda con el Identificador de algun registro del tipo <c>Proyecto</c> y 
        /// mediante los parametros de paginación recibIdos paginarlos
        /// </summary>
        /// <param name="paginacionDTO">Parametros de paginación</param>
        /// <param name="IdProyecto">Identificador de un proyecto <seealso cref="ProyectoDTO"/></param>
        /// <returns>Lista con todos las requisiciones realizadas por proyecto paginados</returns>
        [HttpGet]
        [Route("ObtenerXIdProyecto/{IdProyecto:int}")]
        public async Task<ActionResult<List<RequisicionDTO>>> ObtenXIdProyecto(int IdProyecto)
        {
            var lista = await _Proceso.ListarRequisicionesXIdProyecto(IdProyecto);
            return lista;
        }

        /// <summary>
        /// Endpoint del tipo Get para llamar al método "obtenRequisicionAsync" que permite obtener
        /// los registros cuyo IdProyecto coincIda con el Identificador de algun registro del tipo
        /// <c>Proyecto</c> y devuelve la lista sin paginar
        /// </summary>
        /// <param name="IdProyecto">Identificador de un proyecto <seealso cref="ProyectoDTO"/></param>
        /// <returns>Lista con todos las requisiciones realizadas por proyecto</returns>
        [HttpGet("sinpaginar/{IdProyecto:int}")]
        public async Task<ActionResult<List<RequisicionDTO>>> Get(int IdProyecto)
        {
            var lista = await _RequisicionService.ObtenTodos();
            var query = lista.AsQueryable().Where(z => z.IdProyecto == IdProyecto);
            var listaResult = query.OrderBy(z => z.Id).ToList();
            return listaResult;
        }
        /// <summary>
        /// Endpoint del tipo Get para llamar al método "obtenRequisicionAsync" que permite obtener
        /// los registros cuyo IdProyecto coincIda con el Identificador de algun registro del tipo
        /// <c>Proyecto</c> y devuelve la lista sin paginar
        /// </summary>
        /// <param name="IdProyecto">Identificador de un proyecto <seealso cref="ProyectoDTO"/></param>
        /// <returns>Lista con todos las requisiciones realizadas por proyecto</returns>
        [HttpGet("sinpaginartodos")]
        public async Task<ActionResult<List<RequisicionDTO>>> GetTodos()
        {
            var lista = await _RequisicionService.ObtenTodos();
            var query = lista.AsQueryable();
            var listaResult = query.OrderBy(z => z.Id).ToList();
            return listaResult;
        }
        /// <summary>
        /// Obtiene las requisiciones capturadas y autorizadas
        /// </summary>
        [HttpGet("sinpaginarcapturadosyautorizados")]
        public async Task<ActionResult<List<RequisicionDTO>>> GetTodosCapturadosYAutorizados()
        {
            var lista = await _RequisicionService.ObtenTodos();
            var query = lista.AsQueryable().Where(z => z.EstatusRequisicion == 1 || z.EstatusRequisicion == 2 || z.EstatusRequisicion == 6 || z.EstatusRequisicion == 8);
            var listaResult = query.OrderBy(z => z.Id).ToList();
            return listaResult;
        }
        /// <summary>
        /// Obtiene la requisicion por número y proyecto
        /// </summary>
        /// <returns>Lista con todos las requisiciones realizadas por proyecto</returns>
        [HttpPost("filtradosinpaginar")]
        public async Task<ActionResult<List<RequisicionDTO>>> GetTodos([FromBody] RequisicionBuscarDTO buscarDTO)
        {
            var lista = await _RequisicionService.ObtenTodos();
            var query = lista.AsQueryable().Where(z => z.NoRequisicion == buscarDTO.NoRequisicion);
            var listaResult = query.OrderBy(z => z.Id).ToList();
            return listaResult;
        }
        /// <summary>
        /// Endpoint del tipo Get para llamar al método "obtenRequisicionXIdAsync" que permite obtener
        /// un registro de requisicion cuyo Id coincIda con el Id recibIdo como parametro
        /// </summary>
        /// <param name="Id">Id de la requisición solicitada</param>
        /// <returns>Requisición cuyo Id es igual al recibIdo como parametro</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<RequisicionDTO>> GetXId(int Id)
        {
            var lista = await _RequisicionService.ObtenXId(Id);
            return lista;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FiltrarDTO"></param>
        /// <returns></returns>
        [HttpPost("busquedaextensa")]
        public async Task<ActionResult<List<RequisicionDTO>>> filtrar([FromBody] RequisicionBusquedaExtensaDTO FiltrarDTO)
        {
            //var queryableRequisiciones = RequisicionSP.obtenRequisicionesAsync().Result.AsQueryable();
            var queryableRequisiciones = _RequisicionService.ObtenTodos().Result.AsQueryable();

            if (Convert.ToInt32(FiltrarDTO.IdProyecto) > 0)
            {
                queryableRequisiciones = queryableRequisiciones.Where(z => z.IdProyecto == Convert.ToInt32(FiltrarDTO.IdProyecto));
            }
            if (Convert.ToDateTime(FiltrarDTO.FechaInicio) != Convert.ToDateTime(null) && Convert.ToDateTime(FiltrarDTO.FechaInicio) > Convert.ToDateTime(null) && FiltrarDTO.EsBusquedaPorFechas)
            {
                DateTime dateTime = Convert.ToDateTime(FiltrarDTO.FechaInicio);
                int dia = dateTime.Day;
                int mes = dateTime.Month;
                int anio = dateTime.Year;
                dateTime = Convert.ToDateTime(dia + "-" + mes + "-" + anio);
                queryableRequisiciones = queryableRequisiciones.Where(z => z.FechaRegistro >= Convert.ToDateTime(dateTime));
            }
            if (Convert.ToDateTime(FiltrarDTO.FechaFinal) != Convert.ToDateTime(null)
                && Convert.ToDateTime(FiltrarDTO.FechaFinal) > Convert.ToDateTime(null)
                && FiltrarDTO.EsBusquedaPorFechas
                && Convert.ToDateTime(FiltrarDTO.FechaFinal) > Convert.ToDateTime("01/01/1/970"))
            {
                DateTime dateTime = Convert.ToDateTime(FiltrarDTO.FechaFinal);
                int dia = dateTime.Day;
                int mes = dateTime.Month;
                int anio = dateTime.Year;
                dateTime = Convert.ToDateTime(dia + "-" + mes + "-" + anio).AddHours(24);
                queryableRequisiciones = queryableRequisiciones.Where(z => z.FechaRegistro <= Convert.ToDateTime(dateTime));
            }
            if (Convert.ToInt32(FiltrarDTO.Estatus) > 0)
            {
                queryableRequisiciones = queryableRequisiciones.Where(z => z.EstatusRequisicion == Convert.ToInt32(FiltrarDTO.Estatus));
            }

            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryableRequisiciones);

            var listaInsumos = queryableRequisiciones.Paginar(FiltrarDTO.PaginacionDTO).OrderByDescending(z => z.NoRequisicion).ToList();

            return listaInsumos;
        }

    }
}
