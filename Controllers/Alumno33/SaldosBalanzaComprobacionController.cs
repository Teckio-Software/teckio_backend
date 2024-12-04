using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;










namespace ERP_TECKIO
{
    [Route("api/saldosbalanzacomprobacion/33")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionSaldosBalanza-Empresa33")]
    public class SaldosBalanzaComprobacionAlumno33Controller : ControllerBase
    {
        private readonly ISaldosBalanzaComprobacionService<Alumno33Context> _Service;
        private readonly ICuentaContableService<Alumno33Context> _CuentaContableService;
        private readonly ILogger<SaldosBalanzaComprobacionAlumno33Controller> _Logger;
        private readonly Alumno33Context _Context;
        public SaldosBalanzaComprobacionAlumno33Controller(
            ILogger<SaldosBalanzaComprobacionAlumno33Controller> logger
            , Alumno33Context context
            , ISaldosBalanzaComprobacionService<Alumno33Context> service
            , ICuentaContableService<Alumno33Context> cuentaContableService
            )
        {
            _Service = service;
            _Logger = logger;
            _Context = context;
            _CuentaContableService = cuentaContableService;
        }

        [HttpPost("obtenerxrangofecha")]
        public async Task<List<VistaBalanzaComprobacionDTO>> ObtenerSaldosXRangoFecha([FromBody] SaldosBalanzaComproblacionXRangoFechaDTO rangoFecha)
        {
            try
            {
                var registrosFiltrados = await _Service.ObtenTodosXRangoFecha(rangoFecha);
                var cuentasContables = await _CuentaContableService.ObtenTodos();
                var balanzaDeComprobacionSinOrdenar = _Service.CrearVistaXRangoFecha(cuentasContables, registrosFiltrados, rangoFecha).Result.ToList();
                var Balanza = _Service.EstructurarRegistros(balanzaDeComprobacionSinOrdenar).Result.ToList();
                return Balanza;
            }
            catch
            {
                return new List<VistaBalanzaComprobacionDTO>();
            }
        }

        [HttpPost("obtenxperiodo")]
        public async Task<List<VistaBalanzaComprobacionDTO>> ObtenerSaldosXPeriodo([FromBody] SaldosBalanzaComproblacionXPeriodoDTO periodo)
        {
            try
            {
                var registrosFiltrados = await _Service.ObtenTodosXPeriodo(periodo);
                var cuentasContables = await _CuentaContableService.ObtenTodos();
                var balanzaDeComprobacionSinOrdenar = _Service.CrearVistaXPeriodo(cuentasContables, registrosFiltrados, periodo).Result.ToList();
                var Balanza = _Service.EstructurarRegistros(balanzaDeComprobacionSinOrdenar).Result.ToList();
                return Balanza;
            }
            catch
            {
                return new List<VistaBalanzaComprobacionDTO>();
            }
        }
    }
}
