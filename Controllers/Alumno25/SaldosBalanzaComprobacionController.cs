using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;










namespace ERP_TECKIO
{
    [Route("api/saldosbalanzacomprobacion/25")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionSaldosBalanza-Empresa25")]
    public class SaldosBalanzaComprobacionAlumno25Controller : ControllerBase
    {
        private readonly ISaldosBalanzaComprobacionService<Alumno25Context> _Service;
        private readonly ICuentaContableService<Alumno25Context> _CuentaContableService;
        private readonly ILogger<SaldosBalanzaComprobacionAlumno25Controller> _Logger;
        private readonly Alumno25Context _Context;
        public SaldosBalanzaComprobacionAlumno25Controller(
            ILogger<SaldosBalanzaComprobacionAlumno25Controller> logger
            , Alumno25Context context
            , ISaldosBalanzaComprobacionService<Alumno25Context> service
            , ICuentaContableService<Alumno25Context> cuentaContableService
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
