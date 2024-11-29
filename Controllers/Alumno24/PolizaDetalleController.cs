using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;











namespace ERP_TECKIO
{
    [Route("api/polizadetalle/24")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa24")]
    public class PolizaDetalleAlumno24Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno24Context> _Service;
        private readonly ICuentaContableService<Alumno24Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno24Controller> _Logger;
        private readonly Alumno24Context _Context;
        public PolizaDetalleAlumno24Controller(
            ILogger<PolizaDetalleAlumno24Controller> logger
            , Alumno24Context context
            , IPolizaDetalleService<Alumno24Context> service
            , ICuentaContableService<Alumno24Context> cuentaContableService)
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
