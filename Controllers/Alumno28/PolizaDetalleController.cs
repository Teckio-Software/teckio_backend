using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;











namespace ERP_TECKIO
{
    [Route("api/polizadetalle/28")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa28")]
    public class PolizaDetalleAlumno28Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno28Context> _Service;
        private readonly ICuentaContableService<Alumno28Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno28Controller> _Logger;
        private readonly Alumno28Context _Context;
        public PolizaDetalleAlumno28Controller(
            ILogger<PolizaDetalleAlumno28Controller> logger
            , Alumno28Context context
            , IPolizaDetalleService<Alumno28Context> service
            , ICuentaContableService<Alumno28Context> cuentaContableService)
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
