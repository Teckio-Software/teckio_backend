using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




using ERP_TECKIO.DTO;






namespace ERP_TECKIO
{
    [Route("api/polizadetalle/22")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa22")]
    public class PolizaDetalleAlumno22Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno22Context> _Service;
        private readonly ICuentaContableService<Alumno22Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno22Controller> _Logger;
        private readonly Alumno22Context _Context;
        public PolizaDetalleAlumno22Controller(
            ILogger<PolizaDetalleAlumno22Controller> logger
            , Alumno22Context context
            , IPolizaDetalleService<Alumno22Context> service
            , ICuentaContableService<Alumno22Context> cuentaContableService)
        {
            _Service = service;
            _Logger = logger;
            _Context = context;
            _CuentaContableService = cuentaContableService;
        }


        [HttpGet("{IdPoliza:int}")]
        public async Task<List<PolizaDetalleDTO>> Get(int IdPoliza)
        {
            try
            {
                var resultado = await _Service.ObtenTodosXIdPoliza(IdPoliza);
                if (resultado.Count > 0)
                {
                    for (int i = 0; i < resultado.Count; i++)
                    {
                        var cuentaContable = await _CuentaContableService.ObtenXId(resultado[i].IdCuentaContable);
                        resultado[i].CuentaContableCodigo = cuentaContable.Codigo;
                    }
                    return resultado;
                }
                else
                {
                    return new List<PolizaDetalleDTO>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return new List<PolizaDetalleDTO>();
        }
    }
}
