using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturaion;

namespace ERP_TECKIO
{
    public class MovimientoBancarioProceso<T> where T : DbContext
    {
        private readonly IMovimientoBancarioService<T> _movimientoBancarioService;
        private readonly IMBancarioContratistaService<T> _mBancarioContratistaService;
        private readonly IMovimientoBancarioClienteService<T> _movimientoBancarioClienteService;
        private readonly IMovimientoBancarioEmpresaService<T> _movimientoBancarioEmpresaService;
        private readonly IContratistaService<T> _contratistaService;
        private readonly IClientesService<T> _clientesService;
        private readonly MovimientoBancarioSaldoProeceso<T> _movimientoBancarioSaldoProceso;
        private readonly IMovimientoBancarioSaldoService<T> _movimientoBancarioSaldoService;
        private readonly IOrdenCompraService<T> _ordenCompraService;
        private readonly IInsumoXOrdenCompraService<T> _insumoXOrdenCompraService;
        private readonly IOrdenCompraXMovimientoBancarioService<T> _ordenCompraXMovimientoBancarioService;
        private readonly IFacturaXOrdenCompraService<T> _facturaXOrdenCompraService;
        private readonly IFacturaXOrdenCompraXMovimientoBancarioService<T> _facturaXOrdenCompraXMovimientoBancarioService;
        public MovimientoBancarioProceso(
            IMovimientoBancarioService<T> movimientoBancarioService,
            IMBancarioContratistaService<T> mBancarioContratistaService,
            IMovimientoBancarioClienteService<T> movimientoBancarioClienteService,
            IMovimientoBancarioEmpresaService<T> movimientoBancarioEmpresaService,
            IContratistaService<T> contratistaService,
            IClientesService<T> clientesService,
            MovimientoBancarioSaldoProeceso<T> movimientoBancarioSaldoProceso,
            IMovimientoBancarioSaldoService<T> movimientoBancarioSaldoService,
            IOrdenCompraService<T> ordenCompraService,
            IInsumoXOrdenCompraService<T> insumoXOrdenCompraService,
            IOrdenCompraXMovimientoBancarioService<T> ordenCompraXMovimientoBancarioService,
            IFacturaXOrdenCompraService<T> facturaXOrdenCompraService,
            IFacturaXOrdenCompraXMovimientoBancarioService<T> facturaXOrdenCompraXMovimientoBancarioService
            ) { 
            _mBancarioContratistaService = mBancarioContratistaService;
            _movimientoBancarioService = movimientoBancarioService;
            _movimientoBancarioClienteService = movimientoBancarioClienteService;
            _movimientoBancarioEmpresaService = movimientoBancarioEmpresaService;
            _contratistaService = contratistaService;
            _clientesService = clientesService;
            _movimientoBancarioSaldoProceso = movimientoBancarioSaldoProceso;
            _movimientoBancarioSaldoService = movimientoBancarioSaldoService;
            _ordenCompraService = ordenCompraService;
            _insumoXOrdenCompraService = insumoXOrdenCompraService;
            _ordenCompraXMovimientoBancarioService = ordenCompraXMovimientoBancarioService;
            _facturaXOrdenCompraService = facturaXOrdenCompraService;
            _facturaXOrdenCompraXMovimientoBancarioService = facturaXOrdenCompraXMovimientoBancarioService;
        }

        public async Task<RespuestaDTO> CrearMoviminetoBancario(MovimientoBancarioTeckioDTO moviminetoBancario) {
            RespuestaDTO respuesta = new RespuestaDTO();
            respuesta.Estatus = false;
            var mBancarioBeneficiario = new MBancarioBeneficiarioDTO();
            mBancarioBeneficiario.IdBeneficiario = moviminetoBancario.IdBeneficiario;
            mBancarioBeneficiario.IdCuentaBancaria = moviminetoBancario.IdCuentaBancaria;

            if (string.IsNullOrEmpty(moviminetoBancario.Folio) || moviminetoBancario.Modalidad <= 0 || moviminetoBancario.TipoDeposito <= 0 || moviminetoBancario.TipoCambio <= 0
                || moviminetoBancario.MontoTotal <= 0 || string.IsNullOrEmpty(moviminetoBancario.Concepto) || moviminetoBancario.IdCuentaBancaria <= 0 || moviminetoBancario.TipoBeneficiario <= 0
                || moviminetoBancario.IdCuentaBancariaEmpresa <= 0 || moviminetoBancario.FechaAplicacion == null || moviminetoBancario.FechaCobra == null) {
                respuesta.Descripcion = "Llene los campos correctamente";
                return respuesta;
            }
            if (moviminetoBancario.TipoBeneficiario != 3 && mBancarioBeneficiario.IdBeneficiario <= 0) {
                respuesta.Descripcion = "Llene los campos correctamente";
                return respuesta;
            }
            var ordenesCompraSelected = new List<OrdenCompraDTO>();
            var facturasSelected = new List<FacturaXOrdenCompraDTO>();

            if (moviminetoBancario.TipoBeneficiario == 1) {
                if (moviminetoBancario.EsOrdenCompra) {
                    ordenesCompraSelected = moviminetoBancario.OrdenCompras.Where(z => z.EsSeleccionado == true).ToList();
                    if (ordenesCompraSelected.Count() <= 0) {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No se han seleccionado ordenes de compra";
                        return respuesta;
                    }
                    foreach (var oc in ordenesCompraSelected) {
                        if (oc.MontoAPagar > oc.Saldo) {
                            respuesta.Estatus = false;
                            respuesta.Descripcion = "El total a pagar supera el saldo";
                            return respuesta;
                        }
                    }
                }
                if (moviminetoBancario.EsFactura)
                {
                    facturasSelected = moviminetoBancario.FacturasXOrdenCompra.Where(z => z.EsSeleccionado == true).ToList();
                    if (facturasSelected.Count() <= 0)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No se han seleccionado facturas";
                        return respuesta;
                    }
                    foreach (var factura in facturasSelected)
                    {
                        if (factura.MontoAPagar > factura.Saldo)
                        {
                            respuesta.Estatus = false;
                            respuesta.Descripcion = "El total a pagar supera el saldo";
                            return respuesta;
                        }
                    }
                }
            }


            moviminetoBancario.Estatus = 0;
            moviminetoBancario.FechaAlta = DateTime.Now;
            moviminetoBancario.IdFactura = 0;
            moviminetoBancario.IdPoliza = 0;

            var MBs = await _movimientoBancarioService.ObtenerXIdCuentaBancaria(moviminetoBancario.IdCuentaBancariaEmpresa);

            if (MBs.Count() <= 0) {
                moviminetoBancario.NoMovimientoBancario = "1";
            }
            else
            {
                moviminetoBancario.NoMovimientoBancario = (MBs.Count() + 1) + "";
            }

            var nuevoMB = await _movimientoBancarioService.CrearYObtener(moviminetoBancario);
            if (nuevoMB.Id <= 0)
            {
                respuesta.Descripcion = "No se guardo el registro";
                return respuesta;
            }

            if (moviminetoBancario.EsOrdenCompra) {
                await crearDetallesXOrdenCompra(ordenesCompraSelected, nuevoMB.Id, facturasSelected);
            }
            if (moviminetoBancario.EsFactura)
            {
                await crearDetallesXFacturaXOrdenCompra(ordenesCompraSelected, nuevoMB.Id, facturasSelected);
            }

            mBancarioBeneficiario.IdMovimientoBancario = nuevoMB.Id;

            var guardaDetalels = await CrearDetalleMovimientoBancario(mBancarioBeneficiario, moviminetoBancario.TipoBeneficiario);
            if (!guardaDetalels) {
                respuesta.Descripcion = "No se guardaron los detalles";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "El registro se creo con exito";
            return respuesta;
        }


        public async Task crearDetallesXOrdenCompra(List<OrdenCompraDTO> ordenesCompra, int IdMovimientoBancario, List<FacturaXOrdenCompraDTO> facturasXOC) {
            foreach (var oc in ordenesCompra) {
                var ordenCompra = await _ordenCompraService.ObtenXId(oc.Id);
                var insumosXOrdeCompra = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(oc.Id);
                decimal totalOC = insumosXOrdeCompra.Sum(z => z.ImporteConIva);
                ordenCompra.TotalSaldado += oc.MontoAPagar;
                await _ordenCompraXMovimientoBancarioService.Crear(new OrdenCompraXMovimientoBancarioDTO()
                {
                    IdMovimientoBancario = IdMovimientoBancario,
                    IdOrdenCompra = ordenCompra.Id,
                    Estatus = 1,
                    TotalSaldado = oc.MontoAPagar
                });
                if (ordenCompra.TotalSaldado == totalOC) {
                    ordenCompra.EstatusSaldado = 3;
                    await _ordenCompraService.Pagar(ordenCompra);
                }
                if (ordenCompra.TotalSaldado < totalOC) {
                    ordenCompra.EstatusSaldado = 2;
                    await _ordenCompraService.Pagar(ordenCompra);
                }

                //var FXOC = facturasXOC.Where(z => z.IdOrdenCompra == oc.Id).OrderBy(z => z.Saldo);
                //foreach (var fac in FXOC) {
                //    if (oc.MontoAPagar == 0) {
                //        continue;
                //    }
                //    if (fac.Saldo <= oc.MontoAPagar) {
                //        fac.TotalSaldado += fac.Saldo;
                //        fac.Estatus = 4;
                //        oc.MontoAPagar -= fac.Saldo;
                //    }else if (fac.Saldo > oc.MontoAPagar) {
                //        fac.TotalSaldado += oc.MontoAPagar;
                //        fac.Estatus = 3;
                //        oc.MontoAPagar = 0;
                //    }
                //    var editarFXOC = await _facturaXOrdenCompraService.Editar(fac);
                //}
            }
        }

        public async Task crearDetallesXFacturaXOrdenCompra(List<OrdenCompraDTO> ordenesCompra, int IdMovimientoBancario, List<FacturaXOrdenCompraDTO> facturasXOC)
        {
            foreach (var FXOC in facturasXOC) {
                var OCsxMB = await _ordenCompraXMovimientoBancarioService.ObtenXIdMovimientoBancario(IdMovimientoBancario);
                var OCMoviminesto = OCsxMB.FirstOrDefault(z => z.IdOrdenCompra == FXOC.IdOrdenCompra);
                if (OCMoviminesto == null) {
                    await _ordenCompraXMovimientoBancarioService.Crear(new OrdenCompraXMovimientoBancarioDTO()
                    {
                        IdMovimientoBancario = IdMovimientoBancario,
                        IdOrdenCompra = FXOC.IdOrdenCompra,
                        Estatus = 1,
                        TotalSaldado = FXOC.MontoAPagar
                    });
                }
                else
                {
                    OCMoviminesto.TotalSaldado += FXOC.MontoAPagar;
                    var editarOCMB = await _ordenCompraXMovimientoBancarioService.Editar(OCMoviminesto);
                }
                if (FXOC.MontoAPagar < FXOC.Saldo) {
                    FXOC.Estatus = 3;
                    FXOC.TotalSaldado += FXOC.MontoAPagar;
                }else if (FXOC.MontoAPagar == FXOC.Saldo)
                {
                    FXOC.Estatus = 4;
                    FXOC.TotalSaldado += FXOC.MontoAPagar;
                }
                var editarFXOC = await _facturaXOrdenCompraService.Editar(FXOC);

                var ordenCompra = await _ordenCompraService.ObtenXId(FXOC.IdOrdenCompra);
                var insumosXOrdeCompra = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(FXOC.IdOrdenCompra);
                decimal totalOC = insumosXOrdeCompra.Sum(z => z.ImporteConIva);
                ordenCompra.TotalSaldado += FXOC.MontoAPagar;
                if (ordenCompra.TotalSaldado >= totalOC)
                {
                    ordenCompra.EstatusSaldado = 3;
                }else if (ordenCompra.TotalSaldado < totalOC)
                {
                    ordenCompra.EstatusSaldado = 2;
                }
                await _ordenCompraService.Pagar(ordenCompra);

                await _facturaXOrdenCompraXMovimientoBancarioService.Crear(new FacturaXOrdenCompraXMovimientoBancarioDTO()
                {
                    IdFacturaXOrdenCompra = FXOC.Id,
                    Estatus = 1,
                    IdMovimientoBancario = IdMovimientoBancario,
                    TotalSaldado = FXOC.MontoAPagar,
                });
            }
        }

        public async Task<bool> CrearDetalleMovimientoBancario(MBancarioBeneficiarioDTO MBanciroBeneficiario, int TipoBeneficiario) {
            switch (TipoBeneficiario)
            {
                case 1:
                    var MBancarioContratista = await _mBancarioContratistaService.CrearYObtener(MBanciroBeneficiario);
                    if (MBancarioContratista.Id > 0)
                    {
                        return true;
                    }
                    break;
                case 2:
                    var MBancarioCliente = await _movimientoBancarioClienteService.CrearYObtener(MBanciroBeneficiario);
                    if (MBancarioCliente.Id > 0) { 
                        return true;
                    }
                    break;
                case 3:
                    var MBancarioEmpresa = await _movimientoBancarioEmpresaService.CrearYObtener(MBanciroBeneficiario);
                    if (MBancarioEmpresa.Id > 0) {
                        return true;
                    }
                    break;
                case 4:
                    break;
            } 
            return false;
        }

        public async Task<List<MovimientoBancarioTeckioDTO>> ObtenerXIdCuentaBancaria(int IdCuentaBancaria)
        {
            var contratistas = await _contratistaService.ObtenTodos();
            var clientes = await _clientesService.ObtenTodos();
            var MBsContratista = await _mBancarioContratistaService.ObtenerTodos();
            var MBsCliente = await _movimientoBancarioClienteService.ObtenerTodos();
            var movimientosBancarios = await _movimientoBancarioService.ObtenerXIdCuentaBancaria(IdCuentaBancaria);
            var MBAgrupados = from lista in movimientosBancarios
                              group lista by new { lista.FechaAplicacion.Year, lista.FechaAplicacion.Month } into grupo
                              select new
                              {
                                  fecha = grupo.Key,
                                  datos = grupo
                              };
            List<MovimientoBancarioTeckioDTO> nuevosMB = new List<MovimientoBancarioTeckioDTO>();
            var MBsaldo = await _movimientoBancarioSaldoService.ObtenerXIdCuentaBancaria(IdCuentaBancaria);
            decimal saldo = 0;

            foreach (var grupo in MBAgrupados)
            {
                saldo += await ObtenUltimoSaldo(IdCuentaBancaria, grupo.fecha.Year, grupo.fecha.Month, MBsaldo);
                foreach (var mb in grupo.datos)
                {
                    if (mb.Estatus == 1)
                    {
                        if (mb.TipoDeposito == 1)
                        {
                            saldo += mb.MontoTotal;
                        }
                        else
                        {
                            saldo -= mb.MontoTotal;
                        }
                    }

                    mb.saldo = saldo;
                    mb.descripcionModalidad = mb.Modalidad == 1 ? "Cheque" : "Transferencia";
                    mb.descripcionMoneda = mb.Moneda == 1 ? "MXN" : mb.Moneda == 2 ? "USD" : "EUR";
                    mb.descripcionEstatus = mb.Estatus == 1 ? "Autorizado" : mb.Estatus == 2 ? "Cancelado" : "Capturado";
                    switch (mb.TipoBeneficiario)
                    {
                        case 1:
                            var MBContratista = MBsContratista.Where(z => z.IdMovimientoBancario == mb.Id).ToList();
                            var contratista = contratistas.Where(z => z.Id == MBContratista[0].IdBeneficiario).ToList();
                            mb.beneficiario = contratista[0].RazonSocial;
                            break;
                        case 2:
                            var MBCliente = MBsCliente.Where(z => z.IdMovimientoBancario == mb.Id).ToList();
                            var cliente = clientes.Where(z => z.Id == MBCliente[0].IdBeneficiario).ToList();
                            mb.beneficiario = cliente[0].RazonSocial;
                            break;
                        case 3:
                            mb.beneficiario = "Teckio";
                            break;
                        case 4:
                            break;
                    }
                    nuevosMB.Add(mb);
                }
            }
            return nuevosMB;
        }

        public async Task<List<MovimientoBancarioTeckioDTO>> ObtenerXIdCuentaBancariaYFiltro(int IdCuentaBancaria, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var contratistas = await _contratistaService.ObtenTodos();
            var clientes = await _clientesService.ObtenTodos();
            var MBsContratista = await _mBancarioContratistaService.ObtenerTodos();
            var MBsCliente = await _movimientoBancarioClienteService.ObtenerTodos();
            var movimientosBancarios = await _movimientoBancarioService.ObtenerXIdCuentaBancaria(IdCuentaBancaria);
            if (fechaInicio != null && fechaFin != null) 
            {
                movimientosBancarios = movimientosBancarios.Where(z => z.FechaAplicacion >= fechaInicio && z.FechaAplicacion <= fechaFin).ToList();
            }
            else if (fechaInicio != null && fechaFin == null)
            {
                movimientosBancarios = movimientosBancarios.Where(z => z.FechaAplicacion >= fechaInicio).ToList();
            }
            var MBAgrupados = from lista in movimientosBancarios
                              group lista by new { lista.FechaAplicacion.Year, lista.FechaAplicacion.Month } into grupo
                              select new
                              {
                                  fecha = grupo.Key,
                                  datos = grupo
                              };
            List<MovimientoBancarioTeckioDTO> nuevosMB = new List<MovimientoBancarioTeckioDTO>();
            var MBsaldo = await _movimientoBancarioSaldoService.ObtenerXIdCuentaBancaria(IdCuentaBancaria);
            decimal saldo = 0;

            foreach (var grupo in MBAgrupados)
            {
                saldo = await ObtenUltimoSaldo(IdCuentaBancaria, grupo.fecha.Year, grupo.fecha.Month, MBsaldo);
                foreach (var mb in grupo.datos)
                {
                    if (mb.Estatus == 1)
                    {
                        if (mb.TipoDeposito == 1)
                        {
                            saldo += mb.MontoTotal;
                        }
                        else
                        {
                            saldo -= mb.MontoTotal;
                        }
                    }

                    mb.saldo = saldo;
                    mb.descripcionModalidad = mb.Modalidad == 1 ? "Cheque" : "Transferencia";
                    mb.descripcionMoneda = mb.Moneda == 1 ? "MXN" : mb.Moneda == 2 ? "USD" : "EUR";
                    mb.descripcionEstatus = mb.Estatus == 1 ? "Autorizado" : mb.Estatus == 2 ? "Cancelado" : "Capturado";
                    switch (mb.TipoBeneficiario)
                    {
                        case 1:
                            var MBContratista = MBsContratista.Where(z => z.IdMovimientoBancario == mb.Id).ToList();
                            var contratista = contratistas.Where(z => z.Id == MBContratista[0].IdBeneficiario).ToList();
                            mb.beneficiario = contratista[0].RazonSocial;
                            break;
                        case 2:
                            var MBCliente = MBsCliente.Where(z => z.IdMovimientoBancario == mb.Id).ToList();
                            var cliente = clientes.Where(z => z.Id == MBCliente[0].IdBeneficiario).ToList();
                            mb.beneficiario = cliente[0].RazonSocial;
                            break;
                        case 3:
                            mb.beneficiario = "Teckio";
                            break;
                        case 4:
                            break;
                    }
                    nuevosMB.Add(mb);
                }
            }
            return nuevosMB;
        }

        private async Task<decimal> ObtenUltimoSaldo(int IdCuentaBancaria, int anio, int mes, List<MovimientoBancarioSaldoDTO> saldos)
        {
            saldos = saldos.OrderBy(z => z.Anio).ThenBy(x => x.Mes).ToList();
            
            if (mes == 1)
            {
                anio -= 1;
                mes = 12;
            }
            else
            {
                mes -= 1;
            }
            saldos = saldos.Where(z => z.Anio <= anio && z.Mes <= mes).ToList();
            if (saldos.Count() <= 0)
            {
                return 0;
            }
            var ultimoSaldo = saldos.Last();
            return ultimoSaldo.MontoFinal;
        }
        public async Task<RespuestaDTO> AutorizarXIdMovimientoBancario(int IdMovimientoBancario) {
            RespuestaDTO respuesta = new RespuestaDTO();
            respuesta.Estatus = false;

            var actualizaMB = await _movimientoBancarioService.ActualizarEstatusXId(IdMovimientoBancario, 1);
            if (!actualizaMB) {
                respuesta.Descripcion = "No se autorizo el estatus";
                return respuesta;
            }

            var agruparSaldos = await AgruparMovimientosBancarios(IdMovimientoBancario);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se autorizo el estatus";
            return respuesta;
        }

        public async Task<bool> AgruparMovimientosBancarios(int IdMovimientoBancario)
        {
            var MB = await _movimientoBancarioService.ObtenerXId(IdMovimientoBancario);

            var movimientosbancarios = await _movimientoBancarioService.ObtenerXIdCuentaBancaria(MB.IdCuentaBancariaEmpresa);
            var MBAutorizados = movimientosbancarios.Where(z => z.Estatus == 1);
            var MBAgrupados = from lista in MBAutorizados
                              group lista by new { lista.FechaAplicacion.Year, lista.FechaAplicacion.Month } into grupo
                              select new
                              {
                                  fecha = grupo.Key,
                                  datos = grupo
                              };
            MovimientoBancarioSaldoDTO MBSaldoDTO = new MovimientoBancarioSaldoDTO();
            foreach (var grupo in MBAgrupados)
            {
                if (grupo.fecha.Year >= MB.FechaAplicacion.Year && grupo.fecha.Month >= MB.FechaAplicacion.Month)
                {
                    var Deposito = grupo.datos.Where(z => z.TipoDeposito == 1).Sum(z => z.MontoTotal);
                    var Retiro = grupo.datos.Where(z => z.TipoDeposito == 2).Sum(z => z.MontoTotal);
                    var Monto = Deposito - Retiro;

                    MBSaldoDTO.Anio = grupo.fecha.Year;
                    MBSaldoDTO.Mes = grupo.fecha.Month;
                    MBSaldoDTO.IdCuentaBancaria = MB.IdCuentaBancariaEmpresa;
                    MBSaldoDTO.MontoFinal = Monto;

                    var actualizaSaldo = await _movimientoBancarioSaldoProceso.ActualizarSaldos(MBSaldoDTO);
                }
            }
            return true;
        }

        public async Task<RespuestaDTO> CancelarXIdMovimientoBancario(int IdMovimientoBancario)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            respuesta.Estatus = false;

            var actualizaMB = await _movimientoBancarioService.ActualizarEstatusXId(IdMovimientoBancario, 2);
            if (!actualizaMB)
            {
                respuesta.Descripcion = "No se cancelo el estatus";
                return respuesta;
            }

            var OcXMBancario = await _ordenCompraXMovimientoBancarioService.ObtenXIdMovimientoBancario(IdMovimientoBancario);

            if (OcXMBancario.Count() > 0) {
                foreach (var OCXMB in OcXMBancario) {
                    var ordenCompra = await _ordenCompraService.ObtenXId(OCXMB.IdOrdenCompra);
                    ordenCompra.TotalSaldado -= (decimal) OCXMB.TotalSaldado;
                    ordenCompra.EstatusSaldado = 2;
                    await _ordenCompraService.Pagar(ordenCompra);

                    var facturasXOC = await _facturaXOrdenCompraService.ObtenerXIdOrdenCompra(ordenCompra.Id);
                    var facturasCancelar = facturasXOC.Where(z => z.Estatus == 3 || z.Estatus == 4).OrderBy(z => z.Estatus);
                    var totalFactura = facturasCancelar.Sum(z => z.TotalSaldado);

                    var FOCxMB = await _facturaXOrdenCompraXMovimientoBancarioService.ObtenXIdMovimientoBancario(IdMovimientoBancario);
                    decimal totalFMB = 0;
                    foreach (var fmb in FOCxMB) {
                        foreach (var foc in facturasCancelar) {
                            if (fmb.IdFacturaXOrdenCompra == foc.Id) {
                                totalFMB += fmb.TotalSaldado;
                                foc.TotalSaldado -= fmb.TotalSaldado;
                                foc.Estatus = 3;
                                var editaFacturaXOC = await _facturaXOrdenCompraService.Editar(foc);
                            }
                        }
                    }

                    facturasXOC = await _facturaXOrdenCompraService.ObtenerXIdOrdenCompra(ordenCompra.Id);
                    facturasCancelar = facturasXOC.Where(z => z.Estatus == 3 || z.Estatus == 4).OrderBy(z => z.Estatus);
                    totalFactura = facturasCancelar.Sum(z => z.TotalSaldado);
                    ///despues de restar los FMB restar lo sobrante de OCMB a las FXOC  

                    OCXMB.TotalSaldado -= totalFMB;
                    if (OCXMB.TotalSaldado > totalFMB) {
                        if (OCXMB.TotalSaldado <= totalFactura)
                        {
                            foreach (var fact in facturasCancelar)
                            {
                                if (OCXMB.TotalSaldado <= 0)
                                {
                                    continue;
                                }
                                if (OCXMB.TotalSaldado > fact.TotalSaldado)
                                {
                                    OCXMB.TotalSaldado -= fact.TotalSaldado;
                                    fact.Estatus = 3;
                                    fact.TotalSaldado = 0;
                                }
                                else
                                {
                                    fact.Estatus = 3;
                                    fact.TotalSaldado -= OCXMB.TotalSaldado;
                                    OCXMB.TotalSaldado = 0;
                                }
                                var editaFacturaXOC = await _facturaXOrdenCompraService.Editar(fact);
                            }
                        }
                        else
                        {
                            foreach (var fact in facturasCancelar)
                            {
                                fact.Estatus = 3;
                                fact.TotalSaldado = 0;
                                var editaFacturaXOC = await _facturaXOrdenCompraService.Editar(fact);
                            }
                        }
                    }
                }
            }

            var agruparSaldos = await AgruparMovimientosBancarios(IdMovimientoBancario);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se cancelo el estatus";
            return respuesta;
        }
    }
}
