using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;











namespace ERP_TECKIO
{
    [Route("api/polizadetalle/26")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa26")]
    public class PolizaDetalleAlumno26Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno26Context> _Service;
        private readonly ICuentaContableService<Alumno26Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno26Controller> _Logger;
        private readonly Alumno26Context _Context;
        public PolizaDetalleAlumno26Controller(
            ILogger<PolizaDetalleAlumno26Controller> logger
            , Alumno26Context context
            , IPolizaDetalleService<Alumno26Context> service
            , ICuentaContableService<Alumno26Context> cuentaContableService)
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
