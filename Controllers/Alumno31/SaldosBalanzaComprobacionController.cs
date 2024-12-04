using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;










namespace ERP_TECKIO
{
    [Route("api/saldosbalanzacomprobacion/31")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionSaldosBalanza-Empresa31")]
    public class SaldosBalanzaComprobacionAlumno31Controller : ControllerBase
    {
        private readonly ISaldosBalanzaComprobacionService<Alumno31Context> _Service;
        private readonly ICuentaContableService<Alumno31Context> _CuentaContableService;
        private readonly ILogger<SaldosBalanzaComprobacionAlumno31Controller> _Logger;
        private readonly Alumno31Context _Context;
        public SaldosBalanzaComprobacionAlumno31Controller(
            ILogger<SaldosBalanzaComprobacionAlumno31Controller> logger
            , Alumno31Context context
            , ISaldosBalanzaComprobacionService<Alumno31Context> service
            , ICuentaContableService<Alumno31Context> cuentaContableService
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
