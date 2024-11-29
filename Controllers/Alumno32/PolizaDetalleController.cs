using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




using ERP_TECKIO.DTO;






namespace ERP_TECKIO
{
    [Route("api/polizadetalle/32")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa32")]
    public class PolizaDetalleAlumno32Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno32Context> _Service;
        private readonly ICuentaContableService<Alumno32Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno32Controller> _Logger;
        private readonly Alumno32Context _Context;
        public PolizaDetalleAlumno32Controller(
            ILogger<PolizaDetalleAlumno32Controller> logger
            , Alumno32Context context
            , IPolizaDetalleService<Alumno32Context> service
            , ICuentaContableService<Alumno32Context> cuentaContableService)
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
