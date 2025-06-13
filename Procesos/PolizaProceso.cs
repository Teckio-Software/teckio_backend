using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturaion;
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
        private readonly IMovimientoBancarioService<T> _movimientoBancarioService;
        private readonly IFacturaXOrdenCompraXMovimientoBancarioService<T> _facturaXOrdenCompraXMovimientoBancarioService;
        private readonly IFacturaXOrdenCompraService<T> _facturaXOrdenCompraService;
        private readonly IMBancarioContratistaService<T> _mbancarioContratistaService;
        private readonly ICuentaBancariaEmpresaService<T> _cuentaBancariaEmpresaService;
        private readonly IBancoService<T> _bancoService;

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
            IOrdenCompraXMovimientoBancarioService<T> ordenCompraXMovimientoBancarioService,
            IMovimientoBancarioService<T> movimientoBancarioService,
            IFacturaXOrdenCompraXMovimientoBancarioService<T> facturaXOrdenCompraXMovimientoBancarioService,
            IFacturaXOrdenCompraService<T> facturaXOrdenCompraService,
            IMBancarioContratistaService<T> mbancarioContratistaService,
            ICuentaBancariaEmpresaService<T> cuentaBancariaEmpresaService,
            IBancoService<T> bancoService
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
            _movimientoBancarioService = movimientoBancarioService;
            _facturaXOrdenCompraXMovimientoBancarioService = facturaXOrdenCompraXMovimientoBancarioService;
            _facturaXOrdenCompraService = facturaXOrdenCompraService;
            _mbancarioContratistaService = mbancarioContratistaService;
            _cuentaBancariaEmpresaService = cuentaBancariaEmpresaService;
            _bancoService = bancoService;
        }

        public async Task<RespuestaDTO> validaProcesoPolizaEgreso(List<CuentaContableDTO> cuentasContablesProveedores, FacturaImpuestosDTO existeRetencionIVA, FacturaImpuestosDTO existeRetencionISR, FacturaImpuestosDTO existeIVA) {
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
            if (existeIVA != null) {
                var ivaAcreditableFiscalProveedor = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "IVA Acreditable Fiscal").ToList();
                if (ivaAcreditableFiscalProveedor.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta iva acreditable fiscal asignada al proveedor";
                    return respuesta;
                }
                var ivaAcreditable = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "IVA Acreditable").ToList();
                if (ivaAcreditable.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta iva acreditable asignada al proveedor";
                    return respuesta;
                }
            }
            if (existeRetencionIVA != null)
            {
                var ivaPorAcreditar = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "IVA Por Acreditar").ToList();
                if (ivaPorAcreditar.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta iva por acreditar asignada al proveedor";
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
            if (existeRetencionISR != null)
            {
                var ivaPorAcreditar = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "IVA Por Acreditar").ToList();
                if (ivaPorAcreditar.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta iva por acreditar asignada al proveedor";
                    return respuesta;
                }
                var retencionISR = cuentasContablesProveedores.Where(z => z.TipoCuentaContableDescripcion == "Retención ISR").ToList();
                if (retencionISR.Count() <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay cuenta retención ISR asignada al proveedor";
                    return respuesta;
                }
            }

            return respuesta;
        }

        public async Task<ActionResult<RespuestaDTO>> PolizaXMovimientoBancario(int IdMovimientoBancario)
        {
            var respuesta = new RespuestaDTO();

            var movimientoBancario = await _movimientoBancarioService.ObtenerXId(IdMovimientoBancario);
            if (movimientoBancario.IdPoliza != 0 && movimientoBancario.IdPoliza != null) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ya existe una poliza relacionada a este movimiento";
                return respuesta;
            }

            var cuentaBancariaEmpresa = await _cuentaBancariaEmpresaService.ObtenXId(movimientoBancario.IdCuentaBancariaEmpresa);
            if (cuentaBancariaEmpresa.IdCuentaContable == 0 || cuentaBancariaEmpresa.IdCuentaContable == null) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No existe una cuenta contable realacionada a la cuenta bancario de la empresa";
                return respuesta;
            }
            var cuentaContableBancoEmpresa = await _CuentaContableService.ObtenXId((int)cuentaBancariaEmpresa.IdCuentaContable);
            var banco = await _bancoService.ObtenXId(cuentaBancariaEmpresa.IdBanco);

            var OCxMB = await _ordenCompraXMovimientoBancarioService.ObtenXIdMovimientoBancario(IdMovimientoBancario);
            var FxMB = await _facturaXOrdenCompraXMovimientoBancarioService.ObtenXIdMovimientoBancario(IdMovimientoBancario);

            if (FxMB.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No existen factura relacionada al movimiento bancario";
                return respuesta;
            }

            decimal totalOCMB = (decimal)OCxMB.Sum(z => z.TotalSaldado);
            decimal totalFMB = (decimal)FxMB.Sum(z => z.TotalSaldado);

            if (totalFMB < totalOCMB) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El total de lo facturado no coincide con la orden de compra";
                return respuesta;
            }

            var MBProveedor = await _mbancarioContratistaService.ObtenerXIdMovimientoBancario(IdMovimientoBancario);
            var cuentasContablesProveedores = await _contratistaCuentasContablesProceso.obtenerXContratista(MBProveedor.IdBeneficiario);
            var proveedor = await _ContratistaService.ObtenXId(MBProveedor.IdBeneficiario);

            var listFXOC = new List<FacturaXOrdenCompraDTO>();

            foreach (var OCMB in OCxMB) {
                var FXOC = await _facturaXOrdenCompraService.ObtenerXIdOrdenCompra(OCMB.IdOrdenCompra);
                listFXOC.AddRange(FXOC);
            }

            var listDetallesPoliza = new List<PolizaDetalleDTO>();
            var bancoProveedor = cuentasContablesProveedores.FirstOrDefault(z => z.TipoCuentaContableDescripcion == "Cuenta Contable");
            var ivaPorAcreditar = cuentasContablesProveedores.FirstOrDefault(z => z.TipoCuentaContableDescripcion == "IVA Por Acreditar");
            var ivaAcreditableFiscal = cuentasContablesProveedores.FirstOrDefault(z => z.TipoCuentaContableDescripcion == "IVA Acreditable Fiscal");
            var restencionISR = cuentasContablesProveedores.FirstOrDefault(z => z.TipoCuentaContableDescripcion == "Retención ISR");
            var restencionIVA = cuentasContablesProveedores.FirstOrDefault(z => z.TipoCuentaContableDescripcion == "Retencón IVA");
            var ivaAcreditable = cuentasContablesProveedores.FirstOrDefault(z => z.TipoCuentaContableDescripcion == "IVA Acreditable");

            decimal totalPagar = 0;

            foreach (var FMB in FxMB) {
                var FxOC = listFXOC.FirstOrDefault(z => z.Id == FMB.IdFacturaXOrdenCompra);
                var factura = await _facturaService.ObtenXId(FxOC.IdFactura);
                var facturaImpuestos = await _facturaImpuestosService.ObtenerXIdFactura(factura.Id);
                var existeRetencionIVA = facturaImpuestos.FirstOrDefault(z => z.IdCategoriaImpuesto == 2 && z.IdTipoImpuesto == 1);
                var existeRetencionISR = facturaImpuestos.FirstOrDefault(z => z.IdCategoriaImpuesto == 2 && z.IdTipoImpuesto == 2);
                var existeRetencionIEPS = facturaImpuestos.FirstOrDefault(z => z.IdCategoriaImpuesto == 2 && z.IdTipoImpuesto == 3);
                var existeIva = facturaImpuestos.FirstOrDefault(z => z.IdCategoriaImpuesto == 1 && z.IdTipoImpuesto == 1);
                var validaProcesoPoliza = await validaProcesoPolizaEgreso(cuentasContablesProveedores, existeRetencionIVA, existeRetencionISR, existeIva);
                if (!validaProcesoPoliza.Estatus)
                {
                    return validaProcesoPoliza;
                }

                decimal porcentajePagado = FMB.TotalSaldado / factura.Total;

                if (existeIva != null) {
                    totalPagar += ((factura.Total) * porcentajePagado);

                    var detPoliza = new PolizaDetalleDTO();
                    detPoliza.IdCuentaContable = bancoProveedor.Id;
                    detPoliza.Concepto = "PAGO FACTURA " + factura.Uuid +" " +proveedor.RepresentanteLegal;
                    detPoliza.Debe = ((factura.Subtotal + existeIva.TotalImpuesto) * porcentajePagado);
                    listDetallesPoliza.Add(detPoliza);

                    var existeIvaPorAcreditar = listDetallesPoliza.FirstOrDefault(z => z.IdCuentaContable == ivaPorAcreditar.Id);
                    if (existeIvaPorAcreditar != null)
                    {
                        existeIvaPorAcreditar.Debe += (existeIva.TotalImpuesto * porcentajePagado);
                    }
                    else
                    {
                        var nuevoDetallePoliza = new PolizaDetalleDTO();
                        nuevoDetallePoliza.IdCuentaContable = ivaPorAcreditar.Id;
                        nuevoDetallePoliza.Concepto = "IVA POR ACREDITAR " + proveedor.RepresentanteLegal;
                        nuevoDetallePoliza.Debe = (existeIva.TotalImpuesto * porcentajePagado);
                        listDetallesPoliza.Add(nuevoDetallePoliza);
                    }

                    

                    if (existeRetencionIVA != null) {
                        
                        var ivaFiscal = new PolizaDetalleDTO();
                        ivaFiscal.IdCuentaContable = ivaAcreditableFiscal.Id;
                        ivaFiscal.Concepto = "IVA ACREDITABLE FISCAL" + proveedor.RepresentanteLegal;
                        ivaFiscal.Debe = (existeIva.TotalImpuesto - existeRetencionIVA.TotalImpuesto) * porcentajePagado;
                        listDetallesPoliza.Add(ivaFiscal);

                        existeIvaPorAcreditar = listDetallesPoliza.FirstOrDefault(z => z.IdCuentaContable == ivaPorAcreditar.Id);
                        if (existeIvaPorAcreditar != null)
                        {
                            existeIvaPorAcreditar.Debe -= (existeIva.TotalImpuesto - existeRetencionIVA.TotalImpuesto) * porcentajePagado;
                        }

                        var existerestencionIVA = listDetallesPoliza.FirstOrDefault(z => z.IdCuentaContable == restencionIVA.Id);
                        if (existerestencionIVA != null)
                        {
                            existerestencionIVA.Haber += (existeRetencionIVA.TotalImpuesto * porcentajePagado);
                        }
                        else
                        {
                            var nuevoDetallePoliza = new PolizaDetalleDTO();
                            nuevoDetallePoliza.IdCuentaContable = restencionIVA.Id;
                            nuevoDetallePoliza.Concepto = "IVA RETENCION " + proveedor.RepresentanteLegal;
                            nuevoDetallePoliza.Haber = (existeRetencionIVA.TotalImpuesto * porcentajePagado);
                            listDetallesPoliza.Add(nuevoDetallePoliza);
                        }
                    }
                    if (existeRetencionISR != null)
                    {
                        var existerestencionISR = listDetallesPoliza.FirstOrDefault(z => z.IdCuentaContable == restencionISR.Id);
                        if (existerestencionISR != null)
                        {
                            existerestencionISR.Haber += (existeRetencionISR.TotalImpuesto * porcentajePagado);
                        }
                        else
                        {
                            var nuevoDetallePoliza = new PolizaDetalleDTO();
                            nuevoDetallePoliza.IdCuentaContable = restencionISR.Id;
                            nuevoDetallePoliza.Concepto = "ISR RETENCION " + proveedor.RepresentanteLegal;
                            nuevoDetallePoliza.Haber = (existeRetencionISR.TotalImpuesto * porcentajePagado);
                            listDetallesPoliza.Add(nuevoDetallePoliza);

                        }
                    }

                    var existeIvaAcreditable = listDetallesPoliza.FirstOrDefault(z => z.IdCuentaContable == ivaAcreditable.Id);
                    if (existeIvaAcreditable != null)
                    {
                        existeIvaAcreditable.Haber += (existeIva.TotalImpuesto * porcentajePagado);
                    }
                    else
                    {
                        var nuevoDetallePoliza = new PolizaDetalleDTO();
                        nuevoDetallePoliza.IdCuentaContable = restencionISR.Id;
                        nuevoDetallePoliza.Concepto = "IVA ACREDITABLE " + proveedor.RepresentanteLegal;
                        nuevoDetallePoliza.Haber = (existeIva.TotalImpuesto * porcentajePagado);
                        listDetallesPoliza.Add(nuevoDetallePoliza);
                    }
                }
                else
                {
                    totalPagar = FMB.TotalSaldado;

                    var detPoliza = new PolizaDetalleDTO();
                    detPoliza.IdCuentaContable = bancoProveedor.Id;
                    detPoliza.Concepto = "PAGO FACTURA " + factura.Uuid + " " + proveedor.RepresentanteLegal;
                    detPoliza.Debe = FMB.TotalSaldado;
                    listDetallesPoliza.Add(detPoliza);
                }
            }

            var listaOrdenada = listDetallesPoliza.OrderBy(a => a.Debe).ToList();

            listaOrdenada.Add(new PolizaDetalleDTO()
            {
                IdCuentaContable = cuentaContableBancoEmpresa.Id,
                Concepto = banco.Nombre +" "+ cuentaBancariaEmpresa.NumeroCuenta,
                Haber = totalPagar,
            });


            


            var nuevaPoliza = new PolizaDTO();
            nuevaPoliza.FechaAlta = DateTime.Now;
            nuevaPoliza.FechaPoliza = DateTime.Now;
            nuevaPoliza.IdTipoPoliza = 3;

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

            foreach (var detallePoliza in listaOrdenada) {
                detallePoliza.IdPoliza = crearPoliza.Id;
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

            movimientoBancario.IdPoliza = crearPoliza.Id;
            var editarMB = await _movimientoBancarioService.Editar(movimientoBancario);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Poliza generada";
            return respuesta;
        }


        public async Task<ActionResult<RespuestaDTO>> EliminarPolizaXMovimientoBancario(int IdMovimientoBancario)
        {
            var respuesta = new RespuestaDTO();

            var movimientoBancario = await _movimientoBancarioService.ObtenerXId(IdMovimientoBancario);

            var poliza = await _polizaService.ObtenXId((int) movimientoBancario.IdPoliza);
            var polizaDetalles = await _DetalleService.ObtenTodosXIdPoliza(poliza.Id);

            foreach (var detalle in polizaDetalles) {
                var saldos = await _SaldosService.ObtenTodos();
                var existeSaldo = saldos.FirstOrDefault(z => z.Anio == poliza.FechaPoliza.Year && z.Mes == poliza.FechaPoliza.Month && z.IdCuentaContable == detalle.IdCuentaContable);
                var existenSaldosPosteriores = saldos.Where(z => (z.Anio == poliza.FechaPoliza.Year && z.Mes > poliza.FechaPoliza.Month && z.IdCuentaContable == detalle.IdCuentaContable) 
                || (z.Anio > poliza.FechaPoliza.Year && z.IdCuentaContable == detalle.IdCuentaContable));

                if (detalle.Debe > 0) {
                    existeSaldo.Debe -= detalle.Debe;
                    existeSaldo.SaldoFinal += detalle.Debe;
                    var edidarSaldo = await _SaldosService.Editar(existeSaldo);

                    foreach (var saldo in existenSaldosPosteriores) {
                        saldo.Debe -= detalle.Debe;
                        saldo.SaldoFinal += detalle.Debe;
                        var editaSaldoPosterior = await _SaldosService.Editar(saldo);
                    }
                }else if (detalle.Haber > 0) {
                    existeSaldo.Haber -= detalle.Haber;
                    existeSaldo.SaldoFinal -= detalle.Haber;
                    var edidarSaldo = await _SaldosService.Editar(existeSaldo);

                    foreach (var saldo in existenSaldosPosteriores)
                    {
                        saldo.Haber -= detalle.Haber;
                        saldo.SaldoFinal -= detalle.Haber;
                        var editaSaldoPosterior = await _SaldosService.Editar(saldo);
                    }
                }

            }

            poliza.Estatus = 3;
            var editarPoliza = await _polizaService.Editar(poliza);

            movimientoBancario.IdPoliza = null;
            var editaMoviminetoB = await _movimientoBancarioService.Editar(movimientoBancario);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Poliza Eliminada";
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
