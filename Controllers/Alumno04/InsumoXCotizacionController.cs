using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    [Route("api/insumoxcotizacion/4")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InsumoXCotizacionAlumno04Controller : ControllerBase
    {
        private readonly IInsumoXCotizacionService<Alumno04Context> _Service;
        private readonly ILogger<InsumoXCotizacionAlumno04Controller> _Logger;
        private readonly CotizacionProceso<Alumno04Context> _Proceso;
        private readonly Alumno04Context Context;
        public InsumoXCotizacionAlumno04Controller(
            ILogger<InsumoXCotizacionAlumno04Controller> Logger,
            Alumno04Context Context
            , IInsumoXCotizacionService<Alumno04Context> Service
            , CotizacionProceso<Alumno04Context> Proceso)
        {
            _Logger = Logger;
            this.Context = Context;
            _Service = Service;
            _Proceso = Proceso;
        }

        [HttpPost("CrearInsumoCotizado")]
        public async Task<ActionResult<RespuestaDTO>> CrearInsumoCotizado(InsumoXCotizacionCreacionDTO objeto)
        {
            return await _Proceso.CrearInsumoCotizado(objeto);
        }

        [HttpGet("ObtenTodos/{idRequisicion:int}/{idProyecto:int}")]
        public async Task<ActionResult<List<ListaInsumoXCotizacionDTO>>> ObtenTodos(int idRequisicion, int idProyecto)
        {
            return await _Proceso.ListarInsumosCotizadosXIdRequisicion(idRequisicion, idProyecto);
        }

        [HttpGet("ObtenXIdCotizacion/{idCotizacion:int}")]
        public async Task<ActionResult<List<ListaInsumoXCotizacionDTO>>> ObtenXIdCotizacion(int idCotizacion)
        {
            return await _Proceso.ListarInsumosXCotizacion(idCotizacion);
        }

        [HttpGet("ObtenXIdCotizacionNoComprados/{idCotizacion:int}")]
        public async Task<ActionResult<List<ListaInsumoXCotizacionDTO>>> ObtenXIdCotizacionNoComprados(int idCotizacion)
        {
            return await _Proceso.ListarInsumosXCotizacionNoComprados(idCotizacion);
        }

        [HttpGet("AutorizarXId/{idInusmoCotizado:int}")]
        public async Task<ActionResult<RespuestaDTO>> AutorizarXId(int idInusmoCotizado)
        {
            return await _Proceso.AutorizarXIdInsumoCotizado(idInusmoCotizado);
        }

        [HttpGet("RemoverAutorizacion/{idInusmoCotizado:int}")]
        public async Task<ActionResult<RespuestaDTO>> RemoverAutorizacion(int idInusmoCotizado)
        {
            return await _Proceso.RemoverAutorizacion(idInusmoCotizado);
        }

        [HttpPost("AutorizarInsumosCotizadosSeleccionados")]
        public async Task<ActionResult<RespuestaDTO>> AutorizarInsumosCotizadosSeleccionados([FromBody] List<ListaInsumoXCotizacionDTO> seleccionados)
        {
            return await _Proceso.AutorizarInsumosCotizadosSeleccionados(seleccionados);
        }

        [HttpPut("ActualizarInsumoXCotizacion")]
        public async Task<ActionResult<RespuestaDTO>> ActualizarInsumoXCotizacion(InsumosXCotizacionObjetoRequisicionDTO InsumoXCotizacion)
        {
            return await _Proceso.ActualizarInsumoXCotizacion(InsumoXCotizacion);
        }

        [HttpPost]
        [Route("CrearImpuestosInsumoCotizado/{IdInsumoCotizado:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> CrearImpuestosInsumoCotizado([FromBody] List<ImpuestoInsumoCotizadoDTO> parametro, int IdInsumoCotizado)
        {
            return await _Proceso.CrearImpuestosInsumoCotizado(parametro, IdInsumoCotizado);
        }

        [HttpDelete]
        [Route("EliminarImpuestoInsumoCotizado/{IdInsumoCotizado:int}/{idTipoImpuesto}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaDTO>> EliminarImpuestoInsumoCotizado(int IdInsumoCotizado, int idTipoImpuesto)
        {
            return await _Proceso.EliminarImpuestoInsumoCotizado(IdInsumoCotizado, idTipoImpuesto);
        }

        [HttpGet("todos/{IdCotizacion:int}")]
        public async Task<ActionResult<List<InsumoXCotizacionDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdCotizacion)
        {
            var lista = await _Service.ObtenXIdCotizacion(IdCotizacion);
            var query = lista.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(query);
            var listaResult = query.OrderBy(z => z.Id).Paginar(paginacionDTO).ToList();
            return listaResult;
        }

        [HttpGet("sinpaginar/{IdCotizacion:int}")]
        public async Task<ActionResult<List<InsumoXCotizacionDTO>>> Get(int IdCotizacion)
        {
            var lista = await _Service.ObtenXIdCotizacion(IdCotizacion);
            var query = lista.AsQueryable();
            var listaResult = query.OrderBy(z => z.Id).ToList();
            return listaResult;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<InsumoXCotizacionDTO>> GetXId(int Id)
        {
            var lista = await _Service.ObtenXId(Id);
            return lista;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarCotizacion-Empresa1")]
        public async Task<ActionResult> Put([FromBody] InsumoXCotizacionDTO Edita)
        {
            try
            {
                var resultado = await _Service.Editar(Edita);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno04Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("autorizar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "AutorizarCotizacion-Empresa1")]
        public async Task<ActionResult> PutAutorizar(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusAutorizar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno04Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("cancelar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarCotizacion-Empresa1")]
        public async Task<ActionResult> PutCancelar(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusCancelar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno04Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("remover/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "RemoverAutorizacionCotizacion-Empresa1")]
        public async Task<ActionResult> PutRemover(int Id)
        {
            try
            {
                var resultado = await _Service.ActualizarEstatusRemoverAutorizar(Id);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno04Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
