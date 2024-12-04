using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;


using ERP_TECKIO;


using ERP_TECKIO;




namespace SistemaERP.API.Alumno43Controllers.Procomi
{
    [Route("api/saldosbalanzacomprobacion/43")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionSaldosBalanza-Empresa35")]
    public class SaldosBalanzaComprobacionAlumno43Controller : ControllerBase
    {
        private readonly ISaldosBalanzaComprobacionService<Alumno43Context> _Service;
        private readonly ICuentaContableService<Alumno43Context> _CuentaContableService;
        private readonly ILogger<SaldosBalanzaComprobacionAlumno43Controller> _Logger;
        private readonly Alumno43Context _Context;
        public SaldosBalanzaComprobacionAlumno43Controller(
            ILogger<SaldosBalanzaComprobacionAlumno43Controller> logger
            , Alumno43Context context
            , ISaldosBalanzaComprobacionService<Alumno43Context> service
            , ICuentaContableService<Alumno43Context> cuentaContableService
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
