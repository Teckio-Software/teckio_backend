using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




using ERP_TECKIO.DTO;






namespace ERP_TECKIO
{
    [Route("api/polizadetalle/10")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa10")]
    public class PolizaDetalleAlumno10Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno10Context> _Service;
        private readonly ICuentaContableService<Alumno10Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno10Controller> _Logger;
        private readonly Alumno10Context _Context;
        public PolizaDetalleAlumno10Controller(
            ILogger<PolizaDetalleAlumno10Controller> logger
            , Alumno10Context context
            , IPolizaDetalleService<Alumno10Context> service
            , ICuentaContableService<Alumno10Context> cuentaContableService)
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
