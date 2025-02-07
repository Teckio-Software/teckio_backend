using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/poliza/20")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa1")]
    public class PolizaAlumno20Controller : ControllerBase
    {
        private readonly IPolizaService<Alumno20Context> _Service;
        private readonly IPolizaDetalleService<Alumno20Context> _DetalleService;
        private readonly ITipoPolizaService<Alumno20Context> _TipoPolizaService;
        private readonly ICuentaContableService<Alumno20Context> _CuentaContableService;
        private readonly ISaldosBalanzaComprobacionService<Alumno20Context> _SaldosService;
        private readonly ILogger<PolizaAlumno20Controller> _Logger;
        private readonly Alumno20Context _Context;
        public PolizaAlumno20Controller(
            ILogger<PolizaAlumno20Controller> logger
            , Alumno20Context context
            , IPolizaService<Alumno20Context> service
            , IPolizaDetalleService<Alumno20Context> detalleService
            , ITipoPolizaService<Alumno20Context> tipoPolizaService
            , ISaldosBalanzaComprobacionService<Alumno20Context> saldosService
            , ICuentaContableService<Alumno20Context> cuentaContableService
            )
        {
            _DetalleService = detalleService;
            _Service = service;
            _Logger = logger;
            _Context = context;
            _TipoPolizaService = tipoPolizaService;
            _CuentaContableService = cuentaContableService;
            _SaldosService = saldosService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPoliza-Empresa1")]
        public async Task<ActionResult> Post([FromBody] PolizaDTO polizaCreacionDTO)
        {
            try
            {
                var resultado = await _Service.CrearYObtener(polizaCreacionDTO);
                if (resultado.Id > 0)
                {
                    for (int i = 0; i < polizaCreacionDTO.Detalles.Count; i++)
                    {
                        polizaCreacionDTO.Detalles[i].IdPoliza = resultado.Id;
                        var saldos = await _SaldosService.ObtenTodos();
                        var existeSaldo = saldos.Where(z => z.Anio == polizaCreacionDTO.FechaPoliza.Year && z.Mes == polizaCreacionDTO.FechaPoliza.Month && z.IdCuentaContable == polizaCreacionDTO.Detalles[i].IdCuentaContable);
                        if (existeSaldo.Count() <= 0)
                        {
                            SaldosBalanzaComprobacionDTO saldoCreacion = new SaldosBalanzaComprobacionDTO();
                            saldoCreacion.Anio = polizaCreacionDTO.FechaPoliza.Year;
                            saldoCreacion.Mes = polizaCreacionDTO.FechaPoliza.Month;
                            saldoCreacion.IdCuentaContable = polizaCreacionDTO.Detalles[i].IdCuentaContable;
                            var creado = await _SaldosService.Crear(saldoCreacion);
                        }
                        saldos = await _SaldosService.ObtenTodos();
                        var saldoCreado = saldos.Where(z => z.Anio == polizaCreacionDTO.FechaPoliza.Year && z.Mes == polizaCreacionDTO.FechaPoliza.Month && z.IdCuentaContable == polizaCreacionDTO.Detalles[i].IdCuentaContable).FirstOrDefault();
                        var resultadoDetalle = await _DetalleService.Crear(polizaCreacionDTO.Detalles[i]);
                        saldoCreado!.Debe = saldoCreado!.Debe + polizaCreacionDTO.Detalles[i].Debe;
                        saldoCreado!.Haber = saldoCreado!.Haber + polizaCreacionDTO.Detalles[i].Haber;
                        saldoCreado!.SaldoFinal = saldoCreado!.SaldoInicial - saldoCreado.Debe + saldoCreado.Haber; ///hacer servicios
                        var saldosEditados = saldos.Where(z => (z.Anio > polizaCreacionDTO.FechaPoliza.Year || (z.Mes > polizaCreacionDTO.FechaPoliza.Month && z.Anio == polizaCreacionDTO.FechaPoliza.Year)) && z.IdCuentaContable == polizaCreacionDTO.Detalles[i].IdCuentaContable)
                            .OrderBy(z => z.Mes).OrderBy(z => z.Anio).ToList();
                        if (saldosEditados.Count > 0)
                        {
                            var diferenciaAux = saldosEditados[0].SaldoFinal - saldosEditados[0].SaldoInicial;
                            saldosEditados[0].SaldoInicial = saldoCreado.SaldoFinal;
                            saldosEditados[0].SaldoFinal = saldosEditados[0].SaldoInicial + diferenciaAux;
                            await _SaldosService.Editar(saldosEditados[0]);
                            for (int j = 1; j < saldosEditados.Count; j++)
                            {
                                var aux = saldosEditados[j].SaldoFinal - saldosEditados[j].SaldoInicial;
                                saldosEditados[j].SaldoInicial = saldosEditados[j - 1].SaldoFinal;
                                saldosEditados[j].SaldoFinal = saldosEditados[j].SaldoInicial + aux;
                                if (j == saldosEditados.Count - 1)
                                {
                                    saldosEditados[j].EsUltimo = true;
                                }
                                else
                                {
                                    saldosEditados[j].EsUltimo = false;
                                }
                                await _SaldosService.Editar(saldosEditados[j]);
                            }
                        }
                        else
                        {
                            saldoCreado.EsUltimo = true;
                        }
                        await _SaldosService.Editar(saldoCreado);
                        var cuentaContable = await _CuentaContableService.ObtenXId(polizaCreacionDTO.Detalles[i].IdCuentaContable);
                        if(cuentaContable.ExistePoliza == false)
                        {
                            cuentaContable.ExistePoliza = true;
                            await _CuentaContableService.Editar(cuentaContable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPost("generarfolio")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPoliza-Empresa1")]
        public async Task<PolizaFolioCodigoDTO> GenerarFolio(PolizaDTO datos)
        {
            try
            {
                var resultado = await _Service.ObtenTodosXEmpresa();
                var polizasXMesXTipoPoliza = resultado.Where(z => z.FechaPoliza.Month == datos.FechaPoliza.Month).Where(z => z.IdTipoPoliza == datos.IdTipoPoliza);
                var cantidad = polizasXMesXTipoPoliza.Count() + 1;
                var cantidadCheck = cantidad.ToString();
                var folio = "";
                switch (cantidadCheck.Length)
                {
                    case 1:
                        folio = "00000" + cantidadCheck;
                        break;
                    case 2:
                        folio = "0000" + cantidadCheck;
                        break;
                    case 3:
                        folio = "000" + cantidadCheck;
                        break;
                    case 4:
                        folio = "00" + cantidadCheck;
                        break;
                    case 5:
                        folio = "0" + cantidadCheck;
                        break;
                    case 6:
                        folio = cantidadCheck;
                        break;
                    default:
                        break;
                }
                var numeroPoliza = "";
                var CodigotipoPoliza = _TipoPolizaService.ObtenXId(datos.IdTipoPoliza).Result.Codigo;
                var mes = datos.FechaPoliza.Month.ToString();
                if(mes.Count() == 1)
                {
                    mes = '0' + mes;
                }
                numeroPoliza = datos.FechaPoliza.Year.ToString() + '-' + mes + '-' + CodigotipoPoliza + '-' + folio;
                PolizaFolioCodigoDTO polizaFolioCodigo = new PolizaFolioCodigoDTO();
                polizaFolioCodigo.folio = folio;
                polizaFolioCodigo.numeroPoliza = numeroPoliza;
                return polizaFolioCodigo;
            }
            catch
            {
                return new PolizaFolioCodigoDTO();
            }
        }

        [HttpGet]
        public async Task<List<PolizaDTO>> GetXIdEmpresa()
        {
            try
            {
                var resultado = await _Service.ObtenTodosXEmpresa();
                if (resultado.Count > 0)
                {
                    return resultado;
                }
                else
                {
                    return new List<PolizaDTO>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return new List<PolizaDTO>();
        }

        [HttpPost("filtro")]
        public async Task<List<PolizaDTO>> ObtenerXFiltro(PolizaFiltroEspecificoDTO filtro)
        {
            try
            {
                var polizas = await _Service.ObtenTodosXEmpresa();
                if(filtro.mes > 0)
                {
                    var filtroXMes = polizas.Where(z => z.FechaPoliza.Month == filtro.mes).ToList();
                    polizas = filtroXMes;
                }
                if(filtro.anio > 0)
                {
                    var filtroXAnio = polizas.Where(z => z.FechaPoliza.Month == filtro.anio).ToList();
                    polizas = filtroXAnio;
                }
                if(filtro.IdTipoPoliza > 0)
                {
                    var filtroXTipoPoliza = polizas.Where(z => z.IdTipoPoliza == filtro.IdTipoPoliza).ToList();
                    polizas = filtroXTipoPoliza;
                }
                return polizas;
            }
            catch (Exception ex)
            {
                return new List<PolizaDTO>();
            }
        }

        //[HttpPost("intervaloFechas")]
        //public async Task<List<PolizaDTO>> ObtenerXIntervaloDeFechas(PolizaFiltroIntervaloDTO filtro)
        //{
        //    try
        //    {
        //        var polizas = await _Service.ObtenTodosXEmpresa(filtro.IdEmpresa);

        //    }
        //    catch
        //    {
        //        return new List<PolizaDTO>();
        //    }
        //}

        [HttpPut("editar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPoliza-Empresa1")]
        public async Task<ActionResult> Put([FromBody] PolizaDTO parametros)
        {
            try
            {
                var resultado = await _Service.Editar(parametros);
                if (resultado.Estatus == true)
                {
                    var detallesExistentes = await _DetalleService.ObtenTodosXIdPoliza(parametros.Id);
                    for (int i = 0; i < detallesExistentes.Count; i++)
                    {
                        var resultadoDetalle = await _DetalleService.Editar(parametros.Detalles[i]);
                    }
                    if(parametros.Detalles.Count > detallesExistentes.Count)
                    {
                        for(int i = detallesExistentes.Count; i < parametros.Detalles.Count; i++)
                        {
                            parametros.Detalles[i].IdPoliza = parametros.Id;
                            var resultadoDetalle = await _DetalleService.Crear(parametros.Detalles[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("cancelar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarPoliza-Empresa1")]
        public async Task<ActionResult> Cancelar([FromBody] PolizaDTO parametros)
        {
            try
            {
                var resultado = await _Service.Cancelar(parametros);
                var detalles = await _DetalleService.ObtenTodosXIdPoliza(parametros.Id);
                for (int i = 0; i < detalles.Count; i++)
                {
                    var saldosBalanza = await _SaldosService.ObtenTodos();
                    var saldoEspecifico = saldosBalanza.Where(z => z.Anio == parametros.FechaPoliza.Year && z.Mes == parametros.FechaPoliza.Month && z.IdCuentaContable == parametros.Detalles[i].IdCuentaContable).FirstOrDefault();
                    saldoEspecifico.Debe = saldoEspecifico.Debe - detalles[i].Debe;
                    saldoEspecifico.Haber = saldoEspecifico.Haber - detalles[i].Haber;
                    saldoEspecifico.SaldoFinal = saldoEspecifico.SaldoInicial + saldoEspecifico.Debe - saldoEspecifico.Haber;
                    await _SaldosService.Editar(saldoEspecifico);
                    detalles[i].Debe = 0;
                    detalles[i].Haber = 0;
                    await _DetalleService.Editar(detalles[i]);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut("auditar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPoliza-Empresa1")]
        public async Task<ActionResult> Auditar([FromBody] PolizaDTO parametros)
        {
            try
            {
                var resultado = await _Service.Auditar(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}