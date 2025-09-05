using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class OrdenVentaProceso<T> where T : DbContext
    {
        private readonly IOrdenVentaService<T> _ordenVentaService;
        private readonly IDetalleOrdenVentaService<T> _detalleOrdenVentaService;
        private readonly IImpuestoDetalleOrdenVentaService<T> _impuestoDetalleOrdenVentaService;
        private readonly SalidaProduccionAlmacenProceso<T> _salidaProduccionAlmacenProceso;
        private readonly IClientesService<T> _clienteService;
        private readonly EntradaProduccionAlmacenProceso<T> _entradaProduccionAlmacenProceso;
        private readonly IFacturaService<T> _facturaService;
        private readonly IFacturaDetalleService<T> _detalleFacturaService;

        public OrdenVentaProceso(
            IOrdenVentaService<T> ordenVentaService,
            IDetalleOrdenVentaService<T> detalleOrdenVentaService,
            IImpuestoDetalleOrdenVentaService<T> impuestoDetalleOrdenVentaService,
            SalidaProduccionAlmacenProceso<T> salidaProduccionAlmacenProceso,
            IClientesService<T> clienteService,
            EntradaProduccionAlmacenProceso<T> entradaProduccionAlmacenProceso,
            IFacturaService<T> facturaService,
            IFacturaDetalleService<T> detalleFacturaService
            ) { 
            _ordenVentaService = ordenVentaService;
            _detalleOrdenVentaService = detalleOrdenVentaService;
            _impuestoDetalleOrdenVentaService = impuestoDetalleOrdenVentaService;
            _salidaProduccionAlmacenProceso = salidaProduccionAlmacenProceso;
            _clienteService = clienteService;
            _entradaProduccionAlmacenProceso = entradaProduccionAlmacenProceso;
            _facturaService = facturaService;
            _detalleFacturaService = detalleFacturaService;
        }

        public async Task<RespuestaDTO> CrearOrdenVenta(OrdenVentaDTO ordenVenta, List<System.Security.Claims.Claim> claims) { 
            var respuesta = new RespuestaDTO();

            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            var ordenesVenta = await _ordenVentaService.ObtenerTodos();

            //OV (Probablemente el nombre de la empresa) + El Mes + EL número de factura.
            ordenVenta.NumeroOrdenVenta = "OV_"+(DateTime.Now.ToString("MM"))+"_"+(ordenesVenta.Count()+1).ToString();
            ordenVenta.Elaboro = usuarioNombre[0].Value;
            ordenVenta.FechaRegistro = DateTime.Now;
            ordenVenta.Estatus = 1;
            ordenVenta.EstatusSaldado = 1;
            ordenVenta.TotalSaldado = 0;
            ordenVenta.Observaciones = "";
            ordenVenta.Autorizo = "";

            decimal subtotal = 0;
            decimal descuento = 0;
            decimal totalTraslados = 0;
            decimal totalRetenciones = 0;
            foreach (var detalle in ordenVenta.DetalleOrdenVenta) {
                var Base = (detalle.Cantitdad * detalle.PrecioUnitario) - detalle.Descuento;
                subtotal += Base;
                descuento += detalle.Descuento;
                detalle.IdEstimacion = null;
                foreach (var impuesto in detalle.ImpuestosDetalleOrdenVenta) {
                    impuesto.ImporteTotal = impuesto.TasaCuota * Base;
                    if (impuesto.IdCategoriaImpuesto == 1) {
                        totalTraslados += impuesto.ImporteTotal;
                    }
                    if (impuesto.IdCategoriaImpuesto == 2)
                    {
                        totalRetenciones += impuesto.ImporteTotal;
                    }
                }
            }
            ordenVenta.Descuento = descuento;
            ordenVenta.Subtotal = subtotal;
            ordenVenta.ImporteTotal = subtotal + totalTraslados - totalRetenciones;

            var nuevaOrdenVenta = await _ordenVentaService.CrearYObtener(ordenVenta);
            if (nuevaOrdenVenta.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se genero la orden de venta";
                return respuesta;
            }

            foreach (var detalle in ordenVenta.DetalleOrdenVenta)
            {
                detalle.IdOrdenVenta = nuevaOrdenVenta.Id;
                detalle.ImporteTotal = detalle.Cantitdad * detalle.PrecioUnitario;
                var nuevoDetalle = await _detalleOrdenVentaService.CrearYObtener(detalle);
                if (nuevoDetalle.Id <= 0) {
                    continue;
                }

                foreach (var impuesto in detalle.ImpuestosDetalleOrdenVenta)
                {
                    var crearImpuesto = await _impuestoDetalleOrdenVentaService.Crear(impuesto);
                }
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se genero la orden de venta";
            return respuesta;
        }

        public async Task<RespuestaDTO> EditarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                respuesta = await _ordenVentaService.Editar(ordenVenta);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar la orden de venta";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> EliminarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                respuesta = await _ordenVentaService.Eliminar(ordenVenta);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar la orden de venta";
                return respuesta;
            }
        }

        public async Task<List<OrdenVentaDTO>> ObtenerTodos()
        {
            try
            {
                var lista = await _ordenVentaService.ObtenerTodos();
                if (lista.Count > 0)
                {
                    
                    return lista;
                }
                else
                {
                    return new List<OrdenVentaDTO>();
                }
            }
            catch
            {
                return new List<OrdenVentaDTO>();
            }
        }

        public async Task<RespuestaDTO> Autorizar(OrdenVentaDTO orden, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
                orden.Autorizo = usuarioNombre[0].Value;
                orden.Estatus = 1;
                respuesta = await _ordenVentaService.Editar(orden);
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar autorizar la orden de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Cancelar(OrdenVentaDTO orden)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                orden.Estatus = 2;
                respuesta = await _ordenVentaService.Editar(orden);
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar autorizar la orden de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Autorizar(SalidaProduccionAlmacenAutorizarOrdenVDTO orden, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
                var ordenVenta = await _ordenVentaService.ObtenerOrdenVentaXId(orden.IdOrdenVenta);
                if (ordenVenta.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro la orden de venta";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                var cliente = await _clienteService.ObtenXId(ordenVenta.IdCliente);
                if (cliente.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro el cliente asociado a la orden de venta";
                    respuesta.Estatus = false;
                    return respuesta;
                }

                var salidas = new List<SalidaProduccionAlmacenDTO>();
                foreach (var salida in orden.Salidas)
                {
                    salida.FechaEntrada = DateTime.Now;
                    salida.Recibio = cliente.RazonSocial;
                    var resultSalida = await _salidaProduccionAlmacenProceso.CrearYObtenerSalida(salida);
                    if (resultSalida.Id > 0)
                    {
                        salidas.Add(resultSalida);
                    }
                    else
                    {
                        //Eliminar las salidas que se hayan creado

                        foreach (var salidaCreada in salidas)
                        {
                            var eliminar = await _salidaProduccionAlmacenProceso.Eliminar(salidaCreada.Id);
                        }

                        ///////////////
                        respuesta.Descripcion = "Ocurrio un error al intentar crear la salida de producción al almacén";
                        respuesta.Estatus = false;
                        return respuesta;
                    }
                }
                ordenVenta.Autorizo = usuarioNombre[0].Value;
                ordenVenta.Estatus = 1; //Autorizada y con salidas
                respuesta = await _ordenVentaService.Editar(ordenVenta);
                if(respuesta.Estatus == false)
                {
                    //Eliminar las salidas que se hayan creado

                    foreach (var salidaCreada in salidas)
                    {
                        var eliminar = await _salidaProduccionAlmacenProceso.Eliminar(salidaCreada.Id);
                    }

                    ///////////////
                    respuesta.Descripcion = "Ocurrio un error al intentar autorizar la orden de venta";
                    respuesta.Estatus = false;
                    return respuesta;
                }

                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar autorizar la orden de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Cancelar(OrdenVentaCancelarDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var ordenVenta = await _ordenVentaService.ObtenerOrdenVentaXId(parametro.IdOrdenVenta);
                if (ordenVenta.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro la orden de venta";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                var productosDetalle = await _detalleOrdenVentaService.ObtenerXIdOrdenVenta(ordenVenta.Id);
                EntradaProduccionAlmacenDTO entrada = new EntradaProduccionAlmacenDTO
                {
                    IdAlmacen = parametro.IdAlmacenDestino,
                    FechaEntrada = DateTime.Now,
                    //Recibio = ,
                    Observaciones = "Cancelación de orden de venta"
                };
                foreach (var productos in productosDetalle)
                {
                    //Agrega los detalles
                    entrada.Detalles.Add(new ProductosXEntradaProduccionAlmacenDTO
                    {
                        IdEntradaProduccionAlmacen = 0,
                        IdProductoYservicio = productos.IdProductoYservicio,
                        Cantidad = productos.Cantitdad,
                        TipoOrigen = 2
                    });

                }
                var resultEntrada = await _entradaProduccionAlmacenProceso.CrearYObtener(entrada);
                if (resultEntrada.Id>0)
                {
                    
                    ordenVenta.Estatus = 2; //Cancelada
                    respuesta = await _ordenVentaService.Editar(ordenVenta);
                    if (!respuesta.Estatus)
                    {
                        //Elimina la entrada
                        await _entradaProduccionAlmacenProceso.Eliminar(resultEntrada.Id);
                    }
                    return respuesta;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar crear la entrada de producción al almacen";
                    return respuesta;

                }

            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar cancelar la orden de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> FacturarOrdenVenta(FacturaCreacionDTO factura)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                if (factura.IdOrdenesVenta.Count <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se recibieron órdenes de venta para facturar";
                    return respuesta;
                }
                else
                {
                    var c = false;
                    foreach (var idOrden in factura.IdOrdenesVenta)
                    {
                        var ordenVenta = await _ordenVentaService.ObtenerOrdenVentaXId(idOrden);
                        ordenVenta.DetalleOrdenVenta = await _detalleOrdenVentaService.ObtenerXIdOrdenVenta(ordenVenta.Id);

                        FacturaDTO objeto = new FacturaDTO
                        {
                            Uuid = "",
                            //Provisionalmente setee la fecha de emision y timbrado como la fecha actual aunque esta sujeto a cambios.
                            FechaEmision = DateTime.Now,
                            FechaTimbrado = DateTime.Now,
                            FechaValidacion = DateTime.Now,
                            RfcEmisor = factura.RFCEmisor,
                            Subtotal = ordenVenta.Subtotal,
                            Total = ordenVenta.ImporteTotal,
                            Descuento = ordenVenta.Descuento,
                            SerieCfdi = "",
                            FolioCfdi = "",
                            Estatus = 1,
                            Tipo = factura.Tipo,
                            Modalidad = factura.Modalidad,
                            //provisionalmente el archivo se pone en 0, ya que no se ha generado el XML
                            IdArchivo = 0,
                            MetodoPago = factura.MetodoPago,
                            IdArchivoPdf = null,
                            EstatusEnviadoCentroCostos = false,
                            VersionFactura = "4.0",
                            CodigoPostal = factura.CodigoPostal,
                            TipoCambio = factura.TipoCambio,
                            IdCliente = factura.IdCliente,
                            IdFormaPago = factura.IdFormaPago,
                            IdRegimenFiscalSat = factura.IdRegimenFiscalSat,
                            IdUsoCfdi = factura.IdUsoCfdi,
                            IdMonedaSat = factura.IdMonedaSat,
                        };

                        //Obtiene el folio para la factura obteniendo el último, convirtiendolo a entero y sumandole 1.
                        var facturas = await _facturaService.ObtenTodos();
                        var facturaAnterior = facturas.Last();
                        if (facturaAnterior != null && facturas.Count > 0)
                        {
                            if (facturaAnterior.FolioCfdi != null && facturaAnterior.FolioCfdi != "")
                            {
                                int folio = 0;
                                if (int.TryParse(facturaAnterior.FolioCfdi, out folio))
                                {
                                    objeto.FolioCfdi = (folio + 1).ToString();
                                }
                                else
                                {
                                    objeto.FolioCfdi = "1";
                                }
                            }
                            else
                            {
                                objeto.FolioCfdi = "1";
                            }
                        }
                        else
                        {
                            objeto.FolioCfdi = "1";
                        }
                        objeto.Subtotal = ordenVenta.Subtotal;
                        objeto.Total = ordenVenta.ImporteTotal;
                        objeto.Descuento = ordenVenta.Descuento;
                        var resultFactura = await _facturaService.CrearYObtener(objeto);

                        if (resultFactura.Id <= 0)
                        {
                            respuesta.Estatus = false;
                            respuesta.Descripcion = "Ocurrio un error al intentar facturar la orden de venta";
                            return respuesta;
                        }

                        foreach (var detalle in ordenVenta.DetalleOrdenVenta)
                        {
                            FacturaDetalleDTO detalleFactura = new FacturaDetalleDTO
                            {
                                //De momento se deja en 0 ya que no se ha implementado la funcionalidad de guardar el detalle de la factura
                                IdFactura = 0,
                                Cantidad = detalle.Cantitdad,
                                PrecioUnitario = detalle.PrecioUnitario,
                                Importe = (detalle.Cantitdad * detalle.PrecioUnitario) - detalle.Descuento,
                                Descuento = detalle.Descuento,
                                IdProductoYservicio = detalle.IdProductoYservicio,
                            };
                            var resultDetalle = await _detalleFacturaService.CrearYObtener(detalleFactura);
                            if (resultDetalle.Id <= 0)
                            {
                                respuesta.Estatus = false;
                                respuesta.Descripcion = "Ocurrio un error al intentar facturar la orden de venta";
                                return respuesta;
                            }
                        }
                        c = true;
                        
                    }

                    if (c)
                    {
                        respuesta.Estatus = true;
                        respuesta.Descripcion = "Se facturo la orden de venta";
                        return respuesta;
                    }
                    else
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "Ocurrio un error al intentar facturar la orden de venta";
                        return respuesta;
                    }
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar facturar la orden de venta";
                return respuesta;
            }
        }
    }
}
