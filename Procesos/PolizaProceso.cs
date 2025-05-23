using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class PolizaProceso<T> where T : DbContext
    {
        private readonly IOrdenCompraService<T> _ordenCompraService;
        private readonly IPolizaService<T> _polizaService;
        private readonly IPolizaDetalleService<T> _DetalleService;
        private readonly ICuentaContableService<T> _CuentaContableService;
        private readonly ISaldosBalanzaComprobacionService<T> _SaldosService;
        private readonly IContratistaService<T> _ContratistaService;
        private readonly ContratistaCuentasContablesProceso<T> _contratistaCuentasContablesProceso;
        private readonly ITipoPolizaService<T> _TipoPolizaService;
        private readonly IInsumoXOrdenCompraService<T> _insumoXOrdenCompraService;
        private readonly IFacturaService<T> _facturaService;
        private readonly IFacturaImpuestosService<T> _facturaImpuestosService;
        private readonly IOrdenCompraXMovimientoBancarioService<T> _ordenCompraXMovimientoBancarioService;

        public PolizaProceso(
            IOrdenCompraService<T> ordenCompraService,
            IPolizaService<T> polizaService,
            IPolizaDetalleService<T> DetalleService,
            ICuentaContableService<T> CuentaContableService,
            ISaldosBalanzaComprobacionService<T> SaldosService,
            IContratistaService<T> ContratistaService,
            ContratistaCuentasContablesProceso<T> contratistaCuentasContablesProceso,
            ITipoPolizaService<T> TipoPolizaService,
            IInsumoXOrdenCompraService<T> insumoXOrdenCompraService,
            IFacturaService<T> facturaService,
            IFacturaImpuestosService<T> facturaImpuestosService,
            IOrdenCompraXMovimientoBancarioService<T> ordenCompraXMovimientoBancarioService
            ) { 
            _ordenCompraService = ordenCompraService;
            _polizaService = polizaService;
            _DetalleService = DetalleService;
            _CuentaContableService = CuentaContableService;
            _SaldosService = SaldosService;
            _ContratistaService = ContratistaService;
            _contratistaCuentasContablesProceso = contratistaCuentasContablesProceso;
            _TipoPolizaService = TipoPolizaService;
            _insumoXOrdenCompraService = insumoXOrdenCompraService;
            _facturaService = facturaService;
            _facturaImpuestosService = facturaImpuestosService;
            _ordenCompraXMovimientoBancarioService = ordenCompraXMovimientoBancarioService;
        }

        public async Task<RespuestaDTO> validaProcesoPolizaEgreso(List<CuentaContableDTO> cuentasContablesProveedores, FacturaImpuestosDTO retencion, bool existeIVA) {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = true;
            respuesta.Descripcion = "";
            if (cuentasContablesProveedores.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "no hay cuentas asignadas al proveedor";
                return respuesta;
            }
            var CuentaContableProveedor = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "Cuenta Contable").ToList();
            if (CuentaContableProveedor.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No hay cuenta contable asignada al proveedor";
                return respuesta;
            }
            if (existeIVA) {
                var ivaPorAcreditar = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "IVA Por Acreditar").ToList();
                if (ivaPorAcreditar.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta iva por acreditar asignada al proveedor";
                    return respuesta;
                }
                var ivaAcreditable = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "IVA Por Acreditar").ToList();
                if (ivaAcreditable.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta iva acreditable asignada al proveedor";
                    return respuesta;
                }
            }
            if (retencion != null)
            {
                var ivaAcreditableFiscalProveedor = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "IVA Acreditable Fiscal").ToList();
                if (ivaAcreditableFiscalProveedor.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta iva acreditable fiscal asignada al proveedor";
                    return respuesta;
                }
                var retencionISR = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "Retención ISR").ToList();
                if (retencionISR.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta retención ISR asignada al proveedor";
                    return respuesta;
                }
                var retencionIVA = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "Retencón IVA").ToList();
                if (retencionIVA.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta retención IVA fiscal asignada al proveedor";
                    return respuesta;
                }
            }

            return respuesta;
        }

        public async Task<ActionResult<RespuestaDTO>> GenerarPolizaXFacturaXOrdenCompra(FacturaXOrdenCompraDTO facturaXOrdenCompra)
        {
            var respuesta = new RespuestaDTO();

            if (facturaXOrdenCompra.Estatus != 1) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Solo se autorizan factura capturadas";
                return respuesta;
            }

            var existeOCMovimientoBancario = await _ordenCompraXMovimientoBancarioService.ObtenXIdOrdenCompra(facturaXOrdenCompra.IdOrdenCompra);
            if (existeOCMovimientoBancario.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No existe un movimiento bancario relacionado a esta orden de compra";
                return respuesta;
            }



            var factura = await _facturaService.ObtenXId(facturaXOrdenCompra.IdFactura);
            var facturaImpuestos = await _facturaImpuestosService.ObtenerXIdFactura(factura.Id);
            var existeRetenciones = facturaImpuestos.FirstOrDefault(z => z.IdClasificacionImpuesto == 1);
            bool existeIva = factura.Subtotal != factura.Total ? true : false ; 
            var ordenCompra = await _ordenCompraService.ObtenXId(facturaXOrdenCompra.IdOrdenCompra);
            var cuentasContablesProveedores = await _contratistaCuentasContablesProceso.obtenerXContratista((int)ordenCompra.IdContratista);

            var validaProcesoPoliza = await validaProcesoPolizaEgreso(cuentasContablesProveedores, existeRetenciones, existeIva);
            if (!validaProcesoPoliza.Estatus) {
                return validaProcesoPoliza;
            }


            var nuevaPoliza = new PolizaDTO();
            nuevaPoliza.FechaAlta = DateTime.Now;
            nuevaPoliza.FechaPoliza = DateTime.Now;
            nuevaPoliza.IdTipoPoliza = 5;

            var folioNumero = await GenerarFolio(nuevaPoliza);

            nuevaPoliza.Folio = folioNumero.folio;
            nuevaPoliza.NumeroPoliza = folioNumero.numeroPoliza;
            nuevaPoliza.Concepto = "Pago de Facturas a Proveedor";
            nuevaPoliza.Estatus = 1;
            nuevaPoliza.Observaciones = "";
            nuevaPoliza.OrigenDePoliza = 2;
            nuevaPoliza.EsPolizaCierre = false;

            var crearPoliza = await _polizaService.CrearYObtener(nuevaPoliza);
            if (crearPoliza.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creo la poliza";
                return respuesta;
            }

            var listDetallesPoliza = new List<PolizaDetalleDTO>();

            foreach (var cuentasContables in cuentasContablesProveedores) {
                var detallePoliza = new PolizaDetalleDTO();
                detallePoliza.IdPoliza = crearPoliza.Id;
                detallePoliza.IdCuentaContable = cuentasContables.Id;
                detallePoliza.Concepto = "";
                if (cuentasContables.TipoCuentaContableDescripcion == "Cuenta Contable") {
                    detallePoliza.Haber = 0;
                    detallePoliza.Debe = factura.Subtotal;
                    listDetallesPoliza.Add(detallePoliza);
                }
                if (cuentasContables.TipoCuentaContableDescripcion == "IVA Acreditable Fiscal")
                {
                    detallePoliza.Haber = 0;
                    detallePoliza.Debe = factura.Total - factura.Subtotal;
                    listDetallesPoliza.Add(detallePoliza);
                }
            }

            foreach (var detallePoliza in listDetallesPoliza) {
                var saldos = await _SaldosService.ObtenTodos();
                var existeSaldo = saldos.Where(z => z.Anio == crearPoliza.FechaPoliza.Year && z.Mes == crearPoliza.FechaPoliza.Month && z.IdCuentaContable == detallePoliza.IdCuentaContable);
                if (existeSaldo.Count() <= 0)
                {
                    SaldosBalanzaComprobacionDTO saldoCreacion = new SaldosBalanzaComprobacionDTO();
                    saldoCreacion.Anio = crearPoliza.FechaPoliza.Year;
                    saldoCreacion.Mes = crearPoliza.FechaPoliza.Month;
                    saldoCreacion.IdCuentaContable = detallePoliza.IdCuentaContable;
                    var creado = await _SaldosService.Crear(saldoCreacion);
                }
                saldos = await _SaldosService.ObtenTodos();
                var saldoCreado = saldos.Where(z => z.Anio == crearPoliza.FechaPoliza.Year && z.Mes == crearPoliza.FechaPoliza.Month && z.IdCuentaContable == detallePoliza.IdCuentaContable).FirstOrDefault();

                var creaDetallePoliza = await _DetalleService.Crear(detallePoliza);

                saldoCreado!.Debe = saldoCreado!.Debe + detallePoliza.Debe;
                saldoCreado!.Haber = saldoCreado!.Haber + detallePoliza.Haber;
                saldoCreado!.SaldoFinal = saldoCreado!.SaldoInicial - saldoCreado.Debe + saldoCreado.Haber; ///hacer servicios
                var saldosEditados = saldos.Where(z => (z.Anio > crearPoliza.FechaPoliza.Year || (z.Mes > crearPoliza.FechaPoliza.Month && z.Anio == crearPoliza.FechaPoliza.Year)) && z.IdCuentaContable == detallePoliza.IdCuentaContable)
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
                var cuentaContable = await _CuentaContableService.ObtenXId(detallePoliza.IdCuentaContable);
                if (cuentaContable.ExistePoliza == false)
                {
                    cuentaContable.ExistePoliza = true;
                    await _CuentaContableService.Editar(cuentaContable);
                }
            }

            return respuesta;
        }

        public async Task<PolizaFolioCodigoDTO> GenerarFolio(PolizaDTO datos)
        {
            var resultado = await _polizaService.ObtenTodosXEmpresa();
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
            if (mes.Count() == 1)
            {
                mes = '0' + mes;
            }
            numeroPoliza = datos.FechaPoliza.Year.ToString() + '-' + mes + '-' + CodigotipoPoliza + '-' + folio;
            PolizaFolioCodigoDTO polizaFolioCodigo = new PolizaFolioCodigoDTO();
            polizaFolioCodigo.folio = folio;
            polizaFolioCodigo.numeroPoliza = numeroPoliza;
            return polizaFolioCodigo;
        }
    }
}
