using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;











namespace ERP_TECKIO
{
    [Route("api/polizadetalle/13")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa13")]
    public class PolizaDetalleAlumno13Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno13Context> _Service;
        private readonly ICuentaContableService<Alumno13Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno13Controller> _Logger;
        private readonly Alumno13Context _Context;
        public PolizaDetalleAlumno13Controller(
            ILogger<PolizaDetalleAlumno13Controller> logger
            , Alumno13Context context
            , IPolizaDetalleService<Alumno13Context> service
            , ICuentaContableService<Alumno13Context> cuentaContableService)
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
