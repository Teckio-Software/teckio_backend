using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;











namespace ERP_TECKIO
{
    [Route("api/polizadetalle/19")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa19")]
    public class PolizaDetalleAlumno19Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno19Context> _Service;
        private readonly ICuentaContableService<Alumno19Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno19Controller> _Logger;
        private readonly Alumno19Context _Context;
        public PolizaDetalleAlumno19Controller(
            ILogger<PolizaDetalleAlumno19Controller> logger
            , Alumno19Context context
            , IPolizaDetalleService<Alumno19Context> service
            , ICuentaContableService<Alumno19Context> cuentaContableService)
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
