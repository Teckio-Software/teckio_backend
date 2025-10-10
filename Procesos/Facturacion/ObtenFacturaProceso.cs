using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace ERP_TECKIO.Procesos.Facturacion
{
    public class ObtenFacturaProceso<T> where T : DbContext
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IArchivoService<T> _ArchivoService;
        private readonly IClientesService<T> _clientesService;
        private readonly IFormaPagoSatService<T> _formaPagoSatService;
        private readonly IRegimenFiscalSatService<T> _regimenFiscalSatService;
        private readonly IUsoCfdiSatService<T> _usoCfdiSatService;
        private readonly IMonedaSatService<T> _monedaSatService;
        private readonly IFacturaService<T> _facturaService;
        private readonly ITipoImpuestoService<T> _tipoImpuestoService;
        private readonly ITipoFactorService<T> _factorService;
        private readonly IClasificacionImpuestoService<T> _clasificacionImpuestoService;
        private readonly ICategoriaImpuestoService<T> _categoriaImpuestoService;
        private readonly IFacturaDetalleImpuestoService<T> _facturaDetalleImpuestoService;
        private readonly IFacturaDetalleService<T> _facturaDetalleService;
        private readonly IFacturaEmisorService<T> _factsuraEmisorService;
        private readonly IFacturaImpuestosService<T> _facturaImpuestosService;
        private readonly IFacturaComplementoPagoService<T> _facturaComplementoPagoService;
        private readonly IProductoYservicioService<T> _productoYservicioService;
        private readonly IProductoYServicioSatService<T> _productoYservicioSatService;
        private readonly IUnidadService<T> _unidadService;
        private readonly IUnidadSatService<T> _unidadSatService;
        private readonly IFacturaXOrdenCompraService<T> _facturaXOrdenCompraService;
        private readonly IOrdenCompraService<T> _ordenCompraService;
        private readonly IContratistaService<T> _contratistaService;
        private readonly OrdenCompraProceso<T> _ordenCompraProceso;
        private readonly IOrdenCompraXMovimientoBancarioService<T> _ordenCompraXMovimientoBancarioService;
        private readonly IInsumoXOrdenCompraService<T> _insumoXOrdenCompraService;
        private readonly IFacturaXOrdenCompraXMovimientoBancarioService<T> _facturaXOrdenCompraXMovimientoBancarioService;

        public ObtenFacturaProceso(
            IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor,
            IArchivoService<T> ArchivoService,
            IClientesService<T> clientesService,
            IFormaPagoSatService<T> formaPagoSatService,
            IRegimenFiscalSatService<T> regimenFiscalSatService,
            IUsoCfdiSatService<T> usoCfdiSatService,
            IMonedaSatService<T> monedaSatService,
            IFacturaService<T> facturaService,
            ITipoImpuestoService<T> tipoImpuestoService,
            ITipoFactorService<T> factorService,
            IClasificacionImpuestoService<T> clasificacionImpuestoService,
            ICategoriaImpuestoService<T> categoriaImpuestoService,
            IFacturaDetalleService<T> facturaDetalleService,
            IFacturaDetalleImpuestoService<T> facturaDetalleImpuestoService,
            IFacturaEmisorService<T> factsuraEmisorService,
            IFacturaImpuestosService<T> facturaImpuestosService,
            IFacturaComplementoPagoService<T> facturaComplementoPagoService,
            IProductoYservicioService<T> productoYservicioService,
            IProductoYServicioSatService<T> productoYservicioSatService,
            IUnidadService<T> unidadService,
            IUnidadSatService<T> unidadSatService,
            IFacturaXOrdenCompraService<T> facturaXOrdenCompraService,
            IOrdenCompraService<T> ordenCompraService,
            IContratistaService<T> contratistaService,
            OrdenCompraProceso<T> ordenCompraProceso,
            IOrdenCompraXMovimientoBancarioService<T> ordenCompraXMovimientoBancarioService,
            IInsumoXOrdenCompraService<T> insumoXOrdenCompraService,
            IFacturaXOrdenCompraXMovimientoBancarioService<T> facturaXOrdenCompraXMovimientoBancarioService
            )
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
            _ArchivoService = ArchivoService;
            _clientesService = clientesService;
            _formaPagoSatService = formaPagoSatService;
            _regimenFiscalSatService = regimenFiscalSatService;
            _usoCfdiSatService = usoCfdiSatService;
            _monedaSatService = monedaSatService;
            _facturaService = facturaService;
            _tipoImpuestoService = tipoImpuestoService;
            _factorService = factorService;
            _clasificacionImpuestoService = clasificacionImpuestoService;
            _categoriaImpuestoService = categoriaImpuestoService;
            _facturaDetalleService = facturaDetalleService;
            _facturaDetalleImpuestoService = facturaDetalleImpuestoService;
            _factsuraEmisorService = factsuraEmisorService;
            _facturaImpuestosService = facturaImpuestosService;
            _facturaComplementoPagoService = facturaComplementoPagoService;
            _productoYservicioService = productoYservicioService;
            _productoYservicioSatService = productoYservicioSatService;
            _unidadService = unidadService;
            _unidadSatService = unidadSatService;
            _facturaXOrdenCompraService = facturaXOrdenCompraService;
            _ordenCompraService = ordenCompraService;
            _contratistaService = contratistaService;
            _ordenCompraProceso = ordenCompraProceso;
            _ordenCompraXMovimientoBancarioService = ordenCompraXMovimientoBancarioService;
            _insumoXOrdenCompraService = insumoXOrdenCompraService;
            _facturaXOrdenCompraXMovimientoBancarioService = facturaXOrdenCompraXMovimientoBancarioService;
        }

        public class ConceptosExcelDTO
        {
            public string ClaveProducto { get; set; }
            public string Descripcion { get; set; }
            public string Unidad { get; set; }
            public string ClaveUnidadSat { get; set; }
            public decimal CostoUnitario { get; set; }
        }

        public class DatosFacturaDTO
        {
            public string nombreDocumento { get; set; }
            public string EstatusFactura { get; set; }
        }

        public async Task<List<FacturaDTO>> ObtenerFacturas()
        {
            var facturas = new List<FacturaDTO>();
            var clientes = await _clientesService.ObtenTodos();
            var formaPagos = await _formaPagoSatService.ObtenerTodos();
            var regimenFiscales = await _regimenFiscalSatService.ObtenerTodos();
            var usosCdfi = await _usoCfdiSatService.ObtenerTodos();
            var monedas = await _monedaSatService.ObtenerTodos();
            facturas = await _facturaService.ObtenTodos();
            foreach (var factura in facturas)
            {
                if (factura.IdCliente != null && factura.IdCliente != 0) {
                    var cliente = clientes.Where(z => z.Id == factura.IdCliente).FirstOrDefault();
                    factura.RazonSocialCliente = cliente.RazonSocial;
                    factura.RfcReceptor = cliente.Rfc;
                }
                
                if (factura.IdFormaPago == 0 || factura.IdFormaPago == null)
                {
                    factura.FormaPago = "";
                }
                else
                {
                    var formaPago = formaPagos.Where(z => z.Id == factura.IdFormaPago).FirstOrDefault();
                    factura.FormaPago = formaPago.Clave;
                }

                var regimenFiscal = regimenFiscales.Where(z => z.Id == factura.IdRegimenFiscalSat).FirstOrDefault();
                factura.RegimenFiscal = regimenFiscal.Descripcion;

                var usoCdfi = usosCdfi.Where(z => z.Id == factura.IdUsoCfdi).FirstOrDefault();
                factura.UsoCfdi = usoCdfi.Descripcion;

                var moneda = monedas.Where(z => z.Id == factura.IdMonedaSat).FirstOrDefault();
                factura.MonedaSat = moneda.Codigo;
            }

            return facturas;
        }

        public async Task<RespuestaDTO> validaFactura(ContratistaDTO proveedor, XElement comprobante, XElement facturaEmisor)
        {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = true;
            if (facturaEmisor.Attribute("Rfc")?.Value != proveedor.Rfc)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El rfc del proveedor no coincide con la factura";
                return respuesta;
            }
            if (comprobante.Attribute("TipoDeComprobante")?.Value != "I")
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El tipo de comprobante no coincide";
                return respuesta;
            }

            return respuesta;
        }

        public async Task<RespuestaDTO> validaTotalFacturaXOC(int IdOrdenCompra, decimal TotalOC, decimal TotalFactura, string uuid) {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = true;

            var facturaXOC = await _facturaXOrdenCompraService.ObtenerXIdOrdenCompra(IdOrdenCompra);
            foreach (var fact in facturaXOC) {
                var factura = await _facturaService.ObtenXId(fact.IdFactura);
                if (factura.Uuid == uuid)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Esta factura ya se ha cargado en esta orden de compra";
                    return respuesta;
                }
            }

            var facturaTotales = facturaXOC.Where(z => z.Estatus != 5);
            decimal totalFacturas = 0;
            foreach (var fact in facturaTotales) {
                var factura = await _facturaService.ObtenXId(fact.IdFactura);
                totalFacturas += factura.Total;
            }

            if ((totalFacturas + TotalFactura) > (TotalOC+10)) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Se esta superando el total de la orden de compra";
                return respuesta;
            }

            return respuesta;
        }

        public async Task<RespuestaDTO> CargarFacturaXOrdenCompra(List<IFormFile> archivos, int IdOrdenCompra)
        {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se ha cargado correctamente la factura";
            var xmls = archivos.FirstOrDefault(file => string.Equals(Path.GetExtension(file.FileName), ".xml", StringComparison.OrdinalIgnoreCase));
            if (xmls == null)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se han cargado comprobante";
                return respuesta;
            }

            var pdfs = archivos.FirstOrDefault(file => string.Equals(Path.GetExtension(file.FileName), ".pdf", StringComparison.OrdinalIgnoreCase));

            var ordenCompra = await _ordenCompraService.ObtenXId(IdOrdenCompra);
            var insumosxOC = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(ordenCompra.Id);
            decimal totalOC = insumosxOC.Sum(z => z.ImporteConIva);

            var proveedor = await _contratistaService.ObtenXId((int)ordenCompra.IdContratista);

            using (var memorystream = new MemoryStream())
            {
                await xmls.CopyToAsync(memorystream);
                byte[] xmlFile = memorystream.ToArray();

                XDocument documento = XDocument.Load(new MemoryStream(xmlFile), System.Xml.Linq.LoadOptions.None);

                var ns = documento.Root.GetNamespaceOfPrefix("cfdi");
                DateTime fechaValidacion = DateTime.Now;
                var comprobante = documento.Descendants(ns + "Comprobante").FirstOrDefault();
                var complemento = documento.Descendants(ns + "Complemento").FirstOrDefault();
                var nodoTimbre = complemento.Elements().FirstOrDefault(e => e.Name.LocalName == "TimbreFiscalDigital");
                XNamespace tfd = nodoTimbre.Name.Namespace;
                var timbreFiscalDigital = complemento.Element(tfd + "TimbreFiscalDigital");
                var uuid = timbreFiscalDigital.Attribute("UUID")?.Value;

                decimal TotalFactura = Convert.ToDecimal(comprobante.Attribute("Total")?.Value);
                var respuestaTotales = await validaTotalFacturaXOC(IdOrdenCompra, totalOC, TotalFactura, uuid);
                if (!respuestaTotales.Estatus) {
                    return respuestaTotales;
                }

                var existeFactura = await _facturaService.ObtenXUuid(uuid);
                var existeFacturaValida = existeFactura.Where(z => z.Estatus == 1);
                if (existeFacturaValida.Count() > 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Alguna factura valida ya habia sido cargada con el mismo identificador";
                    return respuesta;
                }

                var fechaTimbrado = timbreFiscalDigital.Attribute("FechaTimbrado")?.Value;

                var numArchivos = await _ArchivoService.ObtenXContenido(uuid);
                var numeroFacturas = numArchivos.Count() + 1;

                var tipoComprobante = comprobante.Attribute("TipoDeComprobante")?.Value;
                var facturaEmisor = documento.Descendants(ns + "Emisor").FirstOrDefault();
                var respuestaValidacion = await validaFactura(proveedor, comprobante, facturaEmisor);
                if (!respuestaValidacion.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La factura no coincide con la orden de compra";
                    return respuesta;
                }

                var facturaReceptor = documento.Descendants(ns + "Receptor").FirstOrDefault();
                var descuento = documento.Descendants(ns + "Descuento").FirstOrDefault();

                var resultadoRutaArchivo = await GuardarArchivoFactura(facturaReceptor.Attribute("Rfc")?.Value, facturaEmisor.Attribute("Rfc")?.Value,
                fechaValidacion.Year.ToString(), fechaValidacion.Month.ToString(), uuid, numeroFacturas, xmls);
                if (string.IsNullOrEmpty(resultadoRutaArchivo))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se guardo el archivo xml";
                    return respuesta;
                }
                var extension = Path.GetExtension(xmls.FileName);
                var nombreArchivo = $"{uuid}{extension}";
                var resultadoArchivoXml = await _ArchivoService.CrearYObtener(new ArchivoDTO()
                {
                    Nombre = nombreArchivo,
                    Ruta = resultadoRutaArchivo
                });
                if (resultadoArchivoXml.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se guardó la ruta del archivo";
                    return respuesta;
                }
                int estatusFactura = 1;

                var registrarFactura = await RegistrarFacturaXOrdenCompra(comprobante, ns, resultadoArchivoXml.Id, uuid, fechaValidacion, Convert.ToDateTime(fechaTimbrado),
                    facturaEmisor, facturaReceptor, estatusFactura, descuento, proveedor);

                if (registrarFactura.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se guardó la factura";
                    return respuesta;
                }

                //RegistrarRelacion con Factura y Orden de Compra
                var nuevaFacturaXOrdenCompra = await _facturaXOrdenCompraService.Crear(new FacturaXOrdenCompraDTO()
                {
                    IdOrdenCompra = IdOrdenCompra,
                    IdFactura = registrarFactura.Id,
                    Estatus = 1
                });

                int IdFactura = registrarFactura.Id;

                var facturaDetalles = documento.Descendants(ns + "Conceptos").FirstOrDefault();
                await leerFacturaDetalleOrdenCompra(facturaDetalles, ns, IdFactura);

                await leerFacturaEmisor(facturaEmisor, IdFactura);

                var facturaImpuestos = documento.Descendants(ns + "Impuestos").FirstOrDefault();
                await leerFacturaImpuestos(facturaImpuestos, ns, IdFactura);

                var facturaComplementoPago = documento.Descendants(ns + "Complemento").FirstOrDefault();
                var nodoPagos = complemento.Elements().FirstOrDefault(e => e.Name.LocalName == "Pagos");
                XNamespace pago20 = nodoTimbre.Name.Namespace;
                await leerFacturaComplementoPago(facturaComplementoPago, pago20, IdFactura);

            }

            return respuesta;
        }

        public async Task<bool> leerFacturas()
        {
            string rutaExcel = @"C:\Users\dev_8\Downloads\ListadoCfdi.xlsx";

            var nombresDocumentos = LeerExcelConClosedXml(rutaExcel);

            foreach (var nombreDocumento in nombresDocumentos)
            {
                string path = @"C:\Users\dev_8\Downloads\facturas 2\" + nombreDocumento.nombreDocumento;
                IFormFile archivo = ConvertirRutaAIFormFileConExtensionXml(path);

                using (var memorystream = new MemoryStream())
                {
                    await archivo.CopyToAsync(memorystream);
                    byte[] xmlFile = memorystream.ToArray();

                    XDocument documento = XDocument.Load(new MemoryStream(xmlFile), System.Xml.Linq.LoadOptions.None);

                    var ns = documento.Root.GetNamespaceOfPrefix("cfdi");
                    DateTime fechaValidacion = DateTime.Now;
                    var comprobante = documento.Descendants(ns + "Comprobante").FirstOrDefault();
                    var complemento = documento.Descendants(ns + "Complemento").FirstOrDefault();
                    var nodoTimbre = complemento.Elements().FirstOrDefault(e => e.Name.LocalName == "TimbreFiscalDigital");
                    XNamespace tfd = nodoTimbre.Name.Namespace;
                    var timbreFiscalDigital = complemento.Element(tfd + "TimbreFiscalDigital");
                    var uuid = timbreFiscalDigital.Attribute("UUID")?.Value;
                    var fechaTimbrado = timbreFiscalDigital.Attribute("FechaTimbrado")?.Value;

                    var numArchivos = await _ArchivoService.ObtenXContenido(uuid);
                    var numeroFacturas = numArchivos.Count() + 1;

                    var tipoComprobante = comprobante.Attribute("TipoDeComprobante")?.Value;
                    var facturaEmisor = documento.Descendants(ns + "Emisor").FirstOrDefault();
                    var facturaReceptor = documento.Descendants(ns + "Receptor").FirstOrDefault();
                    var descuento = documento.Descendants(ns + "Descuento").FirstOrDefault();

                    var resultadoRutaArchivo = await GuardarArchivoFactura(facturaReceptor.Attribute("Rfc")?.Value, facturaEmisor.Attribute("Rfc")?.Value,
                    fechaValidacion.Year.ToString(), fechaValidacion.Month.ToString(), uuid, numeroFacturas, archivo);
                    if (string.IsNullOrEmpty(resultadoRutaArchivo))
                    {
                        continue;
                    }
                    var extension = Path.GetExtension(archivo.FileName);
                    var nombreArchivo = $"{uuid}{extension}";
                    var resultadoArchivoXml = await _ArchivoService.CrearYObtener(new ArchivoDTO()
                    {
                        Nombre = nombreArchivo,
                        Ruta = resultadoRutaArchivo
                    });
                    if (resultadoArchivoXml.Id <= 0)
                    {
                        continue;
                    }
                    int estatusFactura = 0;
                    if (nombreDocumento.EstatusFactura == "Vigente")
                    {
                        estatusFactura = 1;
                    }
                    if (nombreDocumento.EstatusFactura == "Cancelada")
                    {
                        estatusFactura = 2;
                    }
                    if (nombreDocumento.EstatusFactura == "Cancelación Rechazada")
                    {
                        estatusFactura = 3;
                    }

                    var registrarFactura = await RegistrarFactura(comprobante, ns, resultadoArchivoXml.Id, uuid, fechaValidacion, Convert.ToDateTime(fechaTimbrado),
                        facturaEmisor, facturaReceptor, estatusFactura, descuento);

                    if (registrarFactura.Id <= 0)
                    {
                        continue;
                    }


                    int IdFactura = registrarFactura.Id;

                    var facturaDetalles = documento.Descendants(ns + "Conceptos").FirstOrDefault();
                    await leerFacturaDetalle(facturaDetalles, ns, IdFactura);

                    await leerFacturaEmisor(facturaEmisor, IdFactura);

                    //await leerFacturaReceptor(facturaReceptor, IdFactura);

                    var facturaImpuestos = documento.Descendants(ns + "Impuestos").FirstOrDefault();
                    await leerFacturaImpuestos(facturaImpuestos, ns, IdFactura);

                    var facturaComplementoPago = documento.Descendants(ns + "Complemento").FirstOrDefault();
                    var nodoPagos = complemento.Elements().FirstOrDefault(e => e.Name.LocalName == "Pagos");
                    XNamespace pago20 = nodoTimbre.Name.Namespace;
                    await leerFacturaComplementoPago(facturaComplementoPago, pago20, IdFactura);
                }
            }

            return true;
        }

        public async Task<RespuestaDTO> cargarFactura(List<IFormFile> archivos) {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se ha cargado correctamente la factura";
            var xmls = archivos.FirstOrDefault(file => string.Equals(Path.GetExtension(file.FileName), ".xml", StringComparison.OrdinalIgnoreCase));
            if (xmls == null)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se han cargado comprobante";
                return respuesta;
            }

            var pdfs = archivos.FirstOrDefault(file => string.Equals(Path.GetExtension(file.FileName), ".pdf", StringComparison.OrdinalIgnoreCase));

            using (var memorystream = new MemoryStream())
            {
                await xmls.CopyToAsync(memorystream);
                byte[] xmlFile = memorystream.ToArray();

                XDocument documento = XDocument.Load(new MemoryStream(xmlFile), System.Xml.Linq.LoadOptions.None);

                var ns = documento.Root.GetNamespaceOfPrefix("cfdi");
                DateTime fechaValidacion = DateTime.Now;
                var comprobante = documento.Descendants(ns + "Comprobante").FirstOrDefault();
                var complemento = documento.Descendants(ns + "Complemento").FirstOrDefault();
                var nodoTimbre = complemento.Elements().FirstOrDefault(e => e.Name.LocalName == "TimbreFiscalDigital");
                XNamespace tfd = nodoTimbre.Name.Namespace;
                var timbreFiscalDigital = complemento.Element(tfd + "TimbreFiscalDigital");
                var uuid = timbreFiscalDigital.Attribute("UUID")?.Value;
                var fechaTimbrado = timbreFiscalDigital.Attribute("FechaTimbrado")?.Value;

                var numArchivos = await _ArchivoService.ObtenXContenido(uuid);
                var numeroFacturas = numArchivos.Count() + 1;

                var tipoComprobante = comprobante.Attribute("TipoDeComprobante")?.Value;
                var facturaEmisor = documento.Descendants(ns + "Emisor").FirstOrDefault();
                var facturaReceptor = documento.Descendants(ns + "Receptor").FirstOrDefault();
                var descuento = documento.Descendants(ns + "Descuento").FirstOrDefault();

                var resultadoRutaArchivo = await GuardarArchivoFactura(facturaReceptor.Attribute("Rfc")?.Value, facturaEmisor.Attribute("Rfc")?.Value,
                fechaValidacion.Year.ToString(), fechaValidacion.Month.ToString(), uuid, numeroFacturas, xmls);
                if (string.IsNullOrEmpty(resultadoRutaArchivo))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se guardo el archivo";
                    return respuesta;
                }
                var extension = Path.GetExtension(xmls.FileName);
                var nombreArchivo = $"{uuid}{extension}";
                var resultadoArchivoXml = await _ArchivoService.CrearYObtener(new ArchivoDTO()
                {
                    Nombre = nombreArchivo,
                    Ruta = resultadoRutaArchivo
                });
                if (resultadoArchivoXml.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se guardo el archivo";
                    return respuesta;
                }
                int estatusFactura = 1;
                //if (nombreDocumento.EstatusFactura == "Vigente")
                //{
                //    estatusFactura = 1;
                //}
                //if (nombreDocumento.EstatusFactura == "Cancelada")
                //{
                //    estatusFactura = 2;
                //}
                //if (nombreDocumento.EstatusFactura == "Cancelación Rechazada")
                //{
                //    estatusFactura = 3;
                //}

                var registrarFactura = await RegistrarFactura(comprobante, ns, resultadoArchivoXml.Id, uuid, fechaValidacion, Convert.ToDateTime(fechaTimbrado),
                    facturaEmisor, facturaReceptor, estatusFactura, descuento);

                if (registrarFactura.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se guardo la factura";
                    return respuesta;
                }


                int IdFactura = registrarFactura.Id;

                var facturaDetalles = documento.Descendants(ns + "Conceptos").FirstOrDefault();
                await leerFacturaDetalle(facturaDetalles, ns, IdFactura);

                await leerFacturaEmisor(facturaEmisor, IdFactura);

                //await leerFacturaReceptor(facturaReceptor, IdFactura);

                var facturaImpuestos = documento.Descendants(ns + "Impuestos").FirstOrDefault();
                await leerFacturaImpuestos(facturaImpuestos, ns, IdFactura);

                var facturaComplementoPago = documento.Descendants(ns + "Complemento").FirstOrDefault();
                var nodoPagos = complemento.Elements().FirstOrDefault(e => e.Name.LocalName == "Pagos");
                XNamespace pago20 = nodoTimbre.Name.Namespace;
                await leerFacturaComplementoPago(facturaComplementoPago, pago20, IdFactura);
            }

            return respuesta;
        }

        public async Task<FacturaDTO> RegistrarFacturaXOrdenCompra(XElement comprobante, XNamespace ns, long IdArchivo, string uuid, DateTime fechaValidacion, DateTime fechaTimbrado
            , XElement FacturaEmisor, XElement FacturaReceptor, int estatus, XElement descuento, ContratistaDTO proveedor)
        {
            var nuevaFactura = new FacturaDTO();
            nuevaFactura.Uuid = uuid;
            nuevaFactura.FechaValidacion = fechaValidacion;
            nuevaFactura.FechaTimbrado = fechaTimbrado;
            nuevaFactura.FechaEmision = Convert.ToDateTime(comprobante.Attribute("Fecha")?.Value);
            nuevaFactura.RfcEmisor = FacturaEmisor.Attribute("Rfc")?.Value;
            nuevaFactura.Subtotal = Convert.ToDecimal(comprobante.Attribute("SubTotal")?.Value);
            nuevaFactura.Total = Convert.ToDecimal(comprobante.Attribute("Total")?.Value);
            nuevaFactura.SerieCfdi = comprobante.Attribute("Serie")?.Value;
            nuevaFactura.FolioCfdi = comprobante.Attribute("Folio")?.Value;
            nuevaFactura.Estatus = estatus;
            nuevaFactura.CodigoPostal = proveedor.CodigoPostal;


            if (comprobante.Attribute("TipoDeComprobante")?.Value == "I")
            {
                nuevaFactura.Tipo = 1;
                nuevaFactura.MetodoPago = comprobante.Attribute("MetodoPago")?.Value;

                var FormaPago = await _formaPagoSatService.ObtenerXClave(comprobante.Attribute("FormaPago")?.Value);
                nuevaFactura.IdFormaPago = FormaPago.Id;
            }
            if (comprobante.Attribute("TipoDeComprobante")?.Value == "E")
            {
                nuevaFactura.Tipo = 2;
                nuevaFactura.MetodoPago = comprobante.Attribute("MetodoPago")?.Value;
            }
            if (comprobante.Attribute("TipoDeComprobante")?.Value == "P")
            {
                nuevaFactura.Tipo = 5;
                nuevaFactura.MetodoPago = "PUE";
                nuevaFactura.IdFormaPago = null;
            }
            if (comprobante.Attribute("TipoDeComprobante")?.Value == "T")
            {
                nuevaFactura.Tipo = 4;
                nuevaFactura.MetodoPago = "NA";
                if (comprobante.Attribute("FormaPago")?.Value != null)
                {
                    var FormaPago = await _formaPagoSatService.ObtenerXClave(comprobante.Attribute("FormaPago")?.Value);
                    nuevaFactura.IdFormaPago = FormaPago.Id;
                }
                else
                {
                    nuevaFactura.IdFormaPago = null;
                }
            }


            //1 = Cargada por proveedor
            //2 = Carga masiva ftp
            //3 = Descarga SAT
            nuevaFactura.Modalidad = 1;
            nuevaFactura.IdArchivo = IdArchivo;
            nuevaFactura.Descuento = descuento != null ? Convert.ToDecimal(descuento.Attribute("Descuento")?.Value) : 0;
            nuevaFactura.IdArchivoPdf = null;
            nuevaFactura.EstatusEnviadoCentroCostos = false;

            nuevaFactura.VersionFactura = "4.0";
            nuevaFactura.TipoCambio = 1;

            var regimenFiscal = await _regimenFiscalSatService.ObtenerXClave(FacturaReceptor.Attribute("RegimenFiscalReceptor")?.Value);
            nuevaFactura.IdRegimenFiscalSat = regimenFiscal.Id;

            var usoCfdi = await _usoCfdiSatService.ObtenerXClave(FacturaReceptor.Attribute("UsoCFDI")?.Value);
            nuevaFactura.IdUsoCfdi = usoCfdi.Id;

            var moneda = await _monedaSatService.ObtenerXClave(comprobante.Attribute("Moneda")?.Value);
            nuevaFactura.IdMonedaSat = moneda.Id;

            var guardarFactura = await _facturaService.CrearYObtener(nuevaFactura);
            return guardarFactura;
        }

        public async Task<FacturaDTO> RegistrarFactura(XElement comprobante, XNamespace ns, long IdArchivo, string uuid, DateTime fechaValidacion, DateTime fechaTimbrado
            , XElement FacturaEmisor, XElement FacturaReceptor, int estatus, XElement descuento)
        {
            var nuevaFactura = new FacturaDTO();
            nuevaFactura.Uuid = uuid;
            nuevaFactura.FechaValidacion = fechaValidacion;
            nuevaFactura.FechaTimbrado = fechaTimbrado;
            nuevaFactura.FechaEmision = Convert.ToDateTime(comprobante.Attribute("Fecha")?.Value);
            nuevaFactura.RfcEmisor = FacturaEmisor.Attribute("Rfc")?.Value;
            //nuevaFactura.RfcReceptor = FacturaReceptor.Attribute("Rfc")?.Value;
            nuevaFactura.Subtotal = Convert.ToDecimal(comprobante.Attribute("SubTotal")?.Value);
            nuevaFactura.Total = Convert.ToDecimal(comprobante.Attribute("Total")?.Value);
            nuevaFactura.SerieCfdi = comprobante.Attribute("Serie")?.Value;
            nuevaFactura.FolioCfdi = comprobante.Attribute("Folio")?.Value;
            nuevaFactura.Estatus = estatus;


            if (comprobante.Attribute("TipoDeComprobante")?.Value == "I")
            {
                nuevaFactura.Tipo = 1;
                nuevaFactura.MetodoPago = comprobante.Attribute("MetodoPago")?.Value;

                var FormaPago = await _formaPagoSatService.ObtenerXClave(comprobante.Attribute("FormaPago")?.Value);
                nuevaFactura.IdFormaPago = FormaPago.Id;
            }
            if (comprobante.Attribute("TipoDeComprobante")?.Value == "E")
            {
                nuevaFactura.Tipo = 2;
                nuevaFactura.MetodoPago = comprobante.Attribute("MetodoPago")?.Value;
            }
            if (comprobante.Attribute("TipoDeComprobante")?.Value == "P")
            {
                nuevaFactura.Tipo = 5;
                nuevaFactura.MetodoPago = "PUE";
                nuevaFactura.IdFormaPago = null;
            }
            if (comprobante.Attribute("TipoDeComprobante")?.Value == "T")
            {
                nuevaFactura.Tipo = 4;
                nuevaFactura.MetodoPago = "NA";
                if (comprobante.Attribute("FormaPago")?.Value != null)
                {
                    var FormaPago = await _formaPagoSatService.ObtenerXClave(comprobante.Attribute("FormaPago")?.Value);
                    nuevaFactura.IdFormaPago = FormaPago.Id;
                }
                else
                {
                    nuevaFactura.IdFormaPago = null;
                }
            }


            //1 = Cargada por proveedor
            //2 = Carga masiva ftp
            //3 = Descarga SAT
            nuevaFactura.Modalidad = 1;
            nuevaFactura.IdArchivo = IdArchivo;
            nuevaFactura.Descuento = descuento != null ? Convert.ToDecimal(descuento.Attribute("Descuento")?.Value) : 0;
            nuevaFactura.IdArchivoPdf = null;
            nuevaFactura.EstatusEnviadoCentroCostos = false;

            var clientes = await _clientesService.ObtenTodos();
            var existeCliente = clientes.Where(z => z.Rfc == FacturaReceptor.Attribute("Rfc")?.Value).FirstOrDefault();
            if (existeCliente == null)
            {
                var nuevoCliente = new ClienteDTO();
                nuevoCliente.RazonSocial = FacturaReceptor.Attribute("Nombre")?.Value;
                nuevoCliente.Rfc = FacturaReceptor.Attribute("Rfc")?.Value;
                nuevoCliente.RepresentanteLegal = FacturaReceptor.Attribute("Nombre")?.Value;
                nuevoCliente.Email = "";
                nuevoCliente.Telefono = "";
                nuevoCliente.Domicilio = "";
                nuevoCliente.Colonia = "";
                nuevoCliente.Municipio = "";
                nuevoCliente.NoExterior = "";
                nuevoCliente.CodigoPostal = FacturaReceptor.Attribute("DomicilioFiscalReceptor")?.Value;
                var respuestaCliente = await _clientesService.CrearYObtener(nuevoCliente);
                nuevaFactura.IdCliente = respuestaCliente.Id;
            }
            else
            {
                nuevaFactura.IdCliente = existeCliente.Id;
            }

            nuevaFactura.VersionFactura = "4.0";
            nuevaFactura.TipoCambio = 1;

            var regimenFiscal = await _regimenFiscalSatService.ObtenerXClave(FacturaReceptor.Attribute("RegimenFiscalReceptor")?.Value);
            nuevaFactura.IdRegimenFiscalSat = regimenFiscal.Id;

            var usoCfdi = await _usoCfdiSatService.ObtenerXClave(FacturaReceptor.Attribute("UsoCFDI")?.Value);
            nuevaFactura.IdUsoCfdi = usoCfdi.Id;

            var moneda = await _monedaSatService.ObtenerXClave(comprobante.Attribute("Moneda")?.Value);
            nuevaFactura.IdMonedaSat = moneda.Id;

            var guardarFactura = await _facturaService.CrearYObtener(nuevaFactura);
            return guardarFactura;
        }

        public async Task<string> GuardarArchivoFactura(string rfcEmpresa, string rfcProveedor, string anio, string mes, string uuid, int numeroRepetido, IFormFile archivo)
        {
            // Usar WebRootPath seguro
            string webRootPath = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot");

            // Construir la ruta de carpetas físicas
            string folder = Path.Combine(webRootPath, rfcEmpresa, rfcProveedor, anio, mes);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // Nombre único para el archivo
            var extension = Path.GetExtension(archivo.FileName);
            var nombreArchivo = $"{uuid}-{numeroRepetido}{extension}";

            string rutaFisica = Path.Combine(folder, nombreArchivo);

            // Copiar el contenido del archivo a la ruta
            using (var memoryStream = new MemoryStream())
            {
                await archivo.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray();
                await File.WriteAllBytesAsync(rutaFisica, contenido);
            }

            // Armar URL para guardar en base de datos
            if (httpContextAccessor.HttpContext == null)
                throw new InvalidOperationException("No hay HttpContext disponible.");

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

            // Armamos la ruta pública relativa
            var rutaRelativa = Path.Combine(rfcEmpresa, rfcProveedor, anio, mes, nombreArchivo).Replace("\\", "/");
            var rutaParaDB = $"{urlActual}/{rutaRelativa}";

            return rutaRelativa;
        }

        public async Task leerFacturaEmisor(XElement facturaEmisor, int IdFactura)
        {
            var nuevaFacturaEmisor = new FacturaEmisorDTO();
            nuevaFacturaEmisor.IdFactura = IdFactura;
            var regimenFiscal = await _regimenFiscalSatService.ObtenerXClave(facturaEmisor.Attribute("RegimenFiscal")?.Value);
            nuevaFacturaEmisor.IdRegimenFiscalSat = regimenFiscal.Id;
            nuevaFacturaEmisor.Rfc = facturaEmisor.Attribute("Rfc")?.Value;

            await _factsuraEmisorService.Crear(nuevaFacturaEmisor);
        }
        public async Task leerFacturaReceptor(XElement facturaReceptor, int IdFactura)
        {

        }

        public async Task leerFacturaImpuestos(XElement facturaImpuestos, XNamespace ns, int IdFactura)
        {
            if (facturaImpuestos != null)
            {
                var traslados = facturaImpuestos.Descendants(ns + "Traslados").FirstOrDefault();
                if (traslados != null)
                {
                    var impuestos = traslados.Elements(ns + "Traslado");
                    foreach (var impuesto in impuestos)
                    {
                        var facturaImpuesto = new FacturaImpuestosDTO();
                        facturaImpuesto.IdFactura = IdFactura;
                        facturaImpuesto.IdClasificacionImpuesto = 2;
                        facturaImpuesto.IdCategoriaImpuesto = 1;
                        var tipoImpuesto = await _tipoImpuestoService.ObtenXClave(impuesto.Attribute("Impuesto")?.Value);
                        facturaImpuesto.IdTipoImpuesto = tipoImpuesto.Id;
                        facturaImpuesto.TotalImpuesto = Convert.ToDecimal(impuesto.Attribute("Importe")?.Value);
                        await _facturaImpuestosService.CrearYObtener(facturaImpuesto);
                    }
                }
                var Retenciones = facturaImpuestos.Descendants(ns + "Retenciones").FirstOrDefault();
                if (Retenciones != null)
                {
                    var impuestos = Retenciones.Elements(ns + "Retencion");
                    foreach (var impuesto in impuestos)
                    {
                        var facturaImpuesto = new FacturaImpuestosDTO();
                        facturaImpuesto.IdFactura = IdFactura;
                        facturaImpuesto.IdClasificacionImpuesto = 2;
                        facturaImpuesto.IdCategoriaImpuesto = 2;
                        var tipoImpuesto = await _tipoImpuestoService.ObtenXClave(impuesto.Attribute("Impuesto")?.Value);
                        facturaImpuesto.IdTipoImpuesto = tipoImpuesto.Id;
                        facturaImpuesto.TotalImpuesto = Convert.ToDecimal(impuesto.Attribute("Importe")?.Value);
                        await _facturaImpuestosService.CrearYObtener(facturaImpuesto);
                    }
                }
            }
        }
        public async Task leerFacturaDetalle(XElement facturaDetalles, XNamespace ns, int IdFactura)
        {
            if (facturaDetalles != null)
            {
                foreach (var facturaDetalle in facturaDetalles.Elements(ns + "Concepto"))
                {
                    var nuevaFacturaDetalle = new FacturaDetalleDTO();
                    nuevaFacturaDetalle.IdFactura = IdFactura;
                    nuevaFacturaDetalle.Cantidad = Convert.ToDecimal(facturaDetalle.Attribute("Cantidad")?.Value);
                    nuevaFacturaDetalle.PrecioUnitario = Convert.ToDecimal(facturaDetalle.Attribute("ValorUnitario")?.Value);
                    nuevaFacturaDetalle.Importe = Convert.ToDecimal(facturaDetalle.Attribute("Importe")?.Value);
                    var existeDescuento = facturaDetalle.Attribute("Descuento")?.Value;
                    nuevaFacturaDetalle.Descuento = existeDescuento != null ? Convert.ToDecimal(existeDescuento) : 0;
                    nuevaFacturaDetalle.Descripcion = facturaDetalle.Attribute("Descripcion")?.Value;
                    var claveProductos = await _productoYservicioSatService.ObtenerXClave(facturaDetalle.Attribute("ClaveProdServ")?.Value);
                    var productoOServicio = await _productoYservicioService.ObtenerXDescripcionYClave(facturaDetalle.Attribute("Descripcion")?.Value, claveProductos.Id);

                    nuevaFacturaDetalle.IdProductoYservicio = productoOServicio.Id;
                    if (nuevaFacturaDetalle.IdProductoYservicio == 0)
                    {
                        var mensaje = "objeto vacio";
                    }
                    var crearFacturaDetalle = await _facturaDetalleService.CrearYObtener(nuevaFacturaDetalle);
                    if (crearFacturaDetalle.Id > 0)
                    {
                        int IdFacturaDetalle = crearFacturaDetalle.Id;
                        var facturaDetalleImpuestos = facturaDetalle.Descendants(ns + "Impuestos").FirstOrDefault();
                        await leerFacturaDetalleImpuestos(facturaDetalleImpuestos, ns, IdFacturaDetalle);
                    }
                }
            }
        }

        public async Task leerFacturaDetalleOrdenCompra(XElement facturaDetalles, XNamespace ns, int IdFactura)
        {
            if (facturaDetalles != null)
            {
                foreach (var facturaDetalle in facturaDetalles.Elements(ns + "Concepto"))
                {
                    var nuevaFacturaDetalle = new FacturaDetalleDTO();
                    nuevaFacturaDetalle.IdFactura = IdFactura;
                    nuevaFacturaDetalle.Cantidad = Convert.ToDecimal(facturaDetalle.Attribute("Cantidad")?.Value);
                    nuevaFacturaDetalle.PrecioUnitario = Convert.ToDecimal(facturaDetalle.Attribute("ValorUnitario")?.Value);
                    nuevaFacturaDetalle.Importe = Convert.ToDecimal(facturaDetalle.Attribute("Importe")?.Value);
                    var existeDescuento = facturaDetalle.Attribute("Descuento")?.Value;
                    nuevaFacturaDetalle.Descuento = existeDescuento != null ? Convert.ToDecimal(existeDescuento) : 0;
                    nuevaFacturaDetalle.Descripcion = facturaDetalle.Attribute("Descripcion")?.Value;
                    var claveProductos = await _productoYservicioSatService.ObtenerXClave(facturaDetalle.Attribute("ClaveProdServ")?.Value);
                    var claveUnidadSat = await _unidadSatService.ObtenerXClave(facturaDetalle.Attribute("ClaveUnidad")?.Value);

                    var productoOServicio = await _productoYservicioService.ObtenerXDescripcionYClave(facturaDetalle.Attribute("Descripcion")?.Value, claveProductos.Id);
                    if (productoOServicio.Id <= 0)
                    {
                        productoOServicio = await _productoYservicioService.CrearYObtener(new ProductoYservicioDTO()
                        {
                            Codigo = claveProductos.Clave,
                            Descripcion = nuevaFacturaDetalle.Descripcion,
                            IdUnidad = 1,
                            IdProductoYservicioSat = claveProductos.Id,
                            IdUnidadSat = claveUnidadSat.Id,
                            IdCategoriaProductoYServicio = 100,
                            IdSubategoriaProductoYServicio = 100
                        });
                    }

                    nuevaFacturaDetalle.IdProductoYservicio = productoOServicio.Id;
                    if (nuevaFacturaDetalle.IdProductoYservicio == 0)
                    {
                        var mensaje = "objeto vacio";
                    }
                    var crearFacturaDetalle = await _facturaDetalleService.CrearYObtener(nuevaFacturaDetalle);
                    if (crearFacturaDetalle.Id > 0)
                    {
                        int IdFacturaDetalle = crearFacturaDetalle.Id;
                        var facturaDetalleImpuestos = facturaDetalle.Descendants(ns + "Impuestos").FirstOrDefault();
                        await leerFacturaDetalleImpuestos(facturaDetalleImpuestos, ns, IdFacturaDetalle);
                    }
                }
            }
        }

        public async Task leerFacturaDetalleImpuestos(XElement facturaDetalleImpuestos, XNamespace ns, int IdFacturaDetalle)
        {
            if (facturaDetalleImpuestos != null)
            {
                var traslados = facturaDetalleImpuestos.Descendants(ns + "Traslados").FirstOrDefault();
                if (traslados != null)
                {
                    var impuestos = traslados.Elements(ns + "Traslado");
                    foreach (var impuesto in impuestos)
                    {
                        var facturaDetalleImpuesto = new FacturaDetalleImpuestoDTO();
                        facturaDetalleImpuesto.IdFacturaDetalle = IdFacturaDetalle;
                        var tipoImpuesto = await _tipoImpuestoService.ObtenXClave(impuesto.Attribute("Impuesto")?.Value);
                        facturaDetalleImpuesto.IdTipoImpuesto = tipoImpuesto.Id;
                        var tipoFactor = await _factorService.ObtenerXDescripcion(impuesto.Attribute("TipoFactor")?.Value);
                        facturaDetalleImpuesto.IdTipoFactor = tipoFactor.Id;
                        facturaDetalleImpuesto.IdClasificacionImpuesto = 2;
                        facturaDetalleImpuesto.IdCategoriaImpuesto = 1;
                        facturaDetalleImpuesto.Base = Convert.ToDecimal(impuesto.Attribute("Base")?.Value);
                        facturaDetalleImpuesto.Importe = Convert.ToDecimal(impuesto.Attribute("Importe")?.Value);
                        facturaDetalleImpuesto.TasaCuota = Convert.ToDecimal(impuesto.Attribute("TasaOCuota")?.Value);

                        await _facturaDetalleImpuestoService.Crear(facturaDetalleImpuesto);
                    }
                }
                var Retenciones = facturaDetalleImpuestos.Descendants(ns + "Retenciones").FirstOrDefault();
                if (Retenciones != null)
                {
                    var impuestos = Retenciones.Elements(ns + "Retencion");
                    foreach (var impuesto in impuestos)
                    {
                        var facturaDetalleImpuesto = new FacturaDetalleImpuestoDTO();
                        facturaDetalleImpuesto.IdFacturaDetalle = IdFacturaDetalle;
                        var tipoImpuesto = await _tipoImpuestoService.ObtenXClave(impuesto.Attribute("Impuesto")?.Value);
                        facturaDetalleImpuesto.IdTipoImpuesto = tipoImpuesto.Id;
                        var tipoFactor = await _factorService.ObtenerXDescripcion(impuesto.Attribute("TipoFactor")?.Value);
                        facturaDetalleImpuesto.IdTipoFactor = tipoFactor.Id;
                        facturaDetalleImpuesto.IdClasificacionImpuesto = 2;
                        facturaDetalleImpuesto.IdCategoriaImpuesto = 2;
                        facturaDetalleImpuesto.Base = Convert.ToDecimal(impuesto.Attribute("Base")?.Value);
                        facturaDetalleImpuesto.Importe = Convert.ToDecimal(impuesto.Attribute("Importe")?.Value);
                        facturaDetalleImpuesto.TasaCuota = Convert.ToDecimal(impuesto.Attribute("TasaOCuota")?.Value);

                        await _facturaDetalleImpuestoService.Crear(facturaDetalleImpuesto);
                    }
                }
            }
        }

        public async Task leerFacturaComplementoPago(XElement complemento, XNamespace pago20, int IdFactura)
        {
            pago20 = "http://www.sat.gob.mx/Pagos20";
            if (complemento != null)
            {
                var pagos = complemento.Element(pago20 + "Pagos");
                if (pagos != null)
                {
                    foreach (var pago in pagos.Elements(pago20 + "Pago"))
                    {
                        foreach (var doctoRelacionado in pago.Elements(pago20 + "DoctoRelacionado"))
                        {
                            var nuevaFacturaComplementoPago = new FacturaComplementoPagoDTO();
                            nuevaFacturaComplementoPago.IdFactura = IdFactura;
                            nuevaFacturaComplementoPago.Uuid = doctoRelacionado.Attribute("IdDocumento")?.Value;
                            nuevaFacturaComplementoPago.ImpuestoSaldoInsoluto = Convert.ToDecimal(doctoRelacionado.Attribute("ImpSaldoInsoluto")?.Value);
                            nuevaFacturaComplementoPago.ImpuestoSaldoAnterior = Convert.ToDecimal(doctoRelacionado.Attribute("ImpSaldoAnt")?.Value);
                            nuevaFacturaComplementoPago.ImpuestoPagado = Convert.ToDecimal(doctoRelacionado.Attribute("ImpPagado")?.Value);
                            nuevaFacturaComplementoPago.NumeroParcialidades = Convert.ToInt32(doctoRelacionado.Attribute("NumParcialidad")?.Value);

                            await _facturaComplementoPagoService.Crear(nuevaFacturaComplementoPago);
                        }
                    }
                }
            }
        }

        public async Task<bool> ObtenerProductos()
        {
            List<ConceptosExcelDTO> productos = new List<ConceptosExcelDTO>();
            string rutaExcel = @"C:\Users\dev_8\Downloads\ListadoCfdi.xlsx";

            var nombresDocumentos = LeerExcelConClosedXml(rutaExcel);

            foreach (var nombreDocumento in nombresDocumentos)
            {
                string path = @"C:\Users\dev_8\Downloads\facturas 2\" + nombreDocumento;
                IFormFile archivo = ConvertirRutaAIFormFileConExtensionXml(path);

                using (var memorystream = new MemoryStream())
                {
                    await archivo.CopyToAsync(memorystream);
                    byte[] xmlFile = memorystream.ToArray();

                    XDocument documento = XDocument.Load(new MemoryStream(xmlFile), System.Xml.Linq.LoadOptions.None);

                    var ns = documento.Root.GetNamespaceOfPrefix("cfdi");

                    var conceptos = documento.Descendants(ns + "Conceptos").FirstOrDefault();
                    if (conceptos != null)
                    {
                        foreach (var concepto in conceptos.Elements(ns + "Concepto"))
                        {
                            productos.Add(new ConceptosExcelDTO()
                            {
                                ClaveProducto = concepto.Attribute("ClaveProdServ")?.Value,
                                Descripcion = concepto.Attribute("Descripcion")?.Value,
                                Unidad = concepto.Attribute("Unidad")?.Value,
                                ClaveUnidadSat = concepto.Attribute("ClaveUnidad")?.Value,
                                CostoUnitario = Convert.ToDecimal(concepto.Attribute("ValorUnitario")?.Value)
                            });
                        }
                    }
                }
            }

            var conceptosOrdenados = productos.OrderBy(z => z.ClaveProducto).ToList();
            await GenerarExcelConceptos(conceptosOrdenados);

            return true;
        }

        private List<DatosFacturaDTO> LeerExcelConClosedXml(string rutaExcel)
        {
            var lista = new List<DatosFacturaDTO>();

            using (var workbook = new XLWorkbook(rutaExcel))
            {
                var hoja = workbook.Worksheet(1); // Primera hoja
                int fila = 2;

                while (!string.IsNullOrWhiteSpace(hoja.Cell(fila, 1).GetString()))
                {
                    string nombreDocumento = hoja.Cell(fila, 3).GetString(); // Columna 3
                    string estatus = hoja.Cell(fila, 1).GetString(); // Columna 1
                    lista.Add(new DatosFacturaDTO()
                    {
                        nombreDocumento = nombreDocumento,
                        EstatusFactura = estatus
                    });
                    fila++;
                }
            }

            return lista;
        }

        public static IFormFile ConvertirRutaAIFormFileConExtensionXml(string rutaArchivo)
        {
            // Crear el FileStream para leer el archivo
            var stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);

            // Cambiar la extensión a .xml
            string nuevoNombreArchivo = Path.GetFileNameWithoutExtension(rutaArchivo) + ".xml";

            return new FormFile(stream, 0, stream.Length, "archivo", nuevoNombreArchivo)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/xml" // o "text/xml" si prefieres
            };
        }

        public async Task GenerarExcelConceptos(List<ConceptosExcelDTO> conceptos)
        {
            var path = @"C:\Users\dev_8\Downloads\Conceptos\";
            string carpeta = Path.GetDirectoryName(path);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Conceptos");

                worksheet.Cell(1, 1).Value = "ClaveProductoServicio";
                worksheet.Cell(1, 2).Value = "Unidad";
                worksheet.Cell(1, 3).Value = "ClaveUnidadSat";
                worksheet.Cell(1, 4).Value = "Descripcion";
                worksheet.Cell(1, 5).Value = "PrecioUnitario";

                for (int i = 0; i < conceptos.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = conceptos[i].ClaveProducto;
                    worksheet.Cell(i + 2, 2).Value = conceptos[i].Unidad;
                    worksheet.Cell(i + 2, 3).Value = conceptos[i].ClaveUnidadSat;
                    worksheet.Cell(i + 2, 4).Value = conceptos[i].Descripcion;
                    worksheet.Cell(i + 2, 5).Value = conceptos[i].CostoUnitario;
                }
                path = path + "Conceptos.xlsx";

                workbook.SaveAs(path);
            }
            return;
        }

        public async Task<List<FacturaDetalleDTO>> ObtenFacturaDetalleXIdFactura(int IdFactura)
        {
            var facturaDetalles = new List<FacturaDetalleDTO>();
            var productosYServicios = await _productoYservicioService.ObtenerTodos();
            facturaDetalles = await _facturaDetalleService.ObtenXIdFactura(IdFactura);
            foreach (var facturaDetalle in facturaDetalles)
            {
                var productoYServicio = productosYServicios.Where(z => z.Id == facturaDetalle.IdProductoYservicio).FirstOrDefault();
                if (productoYServicio.IdUnidad == null || productoYServicio.IdUnidad == 0)
                {
                    facturaDetalle.UnidadSat = "";
                }
                else
                {
                    var unidad = await _unidadService.ObtenerXId((int)productoYServicio.IdUnidad);
                    facturaDetalle.UnidadSat = unidad.Descripcion;
                }
                facturaDetalle.Descripcion = productoYServicio.Descripcion;
                facturaDetalle.IdCategoriaProductoYServicio = productoYServicio.IdCategoriaProductoYServicio;
                facturaDetalle.IdSubategoriaProductoYServicio = productoYServicio.IdSubategoriaProductoYServicio;
            }
            return facturaDetalles;
        }

        public async Task<List<FacturaComplementoPagoDTO>> ObtenComplementoPagoXIdFactura(int IdFactura)
        {
            var complementoPagos = new List<FacturaComplementoPagoDTO>();
            complementoPagos = await _facturaComplementoPagoService.ObtenXIdFactura(IdFactura);
            return complementoPagos;
        }

        public async Task<OrdenCompraFacturasDTO> ObtenFacturaXOrdenCompra(int IdOrdenCompra)
        {
            var ordenCompraFacturas = new OrdenCompraFacturasDTO();
            ordenCompraFacturas.FacturasXOrdenCompra = new List<FacturaXOrdenCompraDTO>();
            decimal MontoTotalOC = 0;
            decimal MontoTotalFactura = 0;

            var ordenCompra = await _ordenCompraService.ObtenXId(IdOrdenCompra);

            var insumosXOrdenCompra = await _ordenCompraProceso.InsumosOrdenCompraXIdOrdenCompra(IdOrdenCompra);
            foreach (var ixoc in insumosXOrdenCompra)
            {
                MontoTotalOC += ixoc.ImporteConIva;
            }

            var facturasXOrdenCompra = await _facturaXOrdenCompraService.ObtenerXIdOrdenCompra(IdOrdenCompra);

            foreach (var fxoc in facturasXOrdenCompra)
            {
                var factura = await _facturaService.ObtenXId(fxoc.IdFactura);
                if (fxoc.Estatus != 1 && fxoc.Estatus != 5) {
                    MontoTotalFactura += factura.Total;
                }
                var detallesFactura = await _facturaDetalleService.ObtenXIdFactura(factura.Id);
                foreach (var det in detallesFactura)
                {
                    var producto = await _productoYservicioService.ObtenerXId(det.IdProductoYservicio);
                    det.Descripcion = producto.Descripcion;
                    var unidad = await _unidadSatService.ObtenerXId((int)producto.IdUnidadSat);
                    det.UnidadSat = unidad.Clave;
                }
                fxoc.FechaEmision = factura.FechaEmision;
                fxoc.Uuid = factura.Uuid;
                fxoc.Total = factura.Total;
                fxoc.DetalleFactura.AddRange(detallesFactura);
            }

            ordenCompraFacturas.MontoTotalFactura = MontoTotalFactura;
            ordenCompraFacturas.MontoTotalOrdenCompra = MontoTotalOC;
            ordenCompraFacturas.FacturasXOrdenCompra = facturasXOrdenCompra;
            ordenCompraFacturas.EstatusSaldado = ordenCompra.EstatusSaldado;

            return ordenCompraFacturas;
        }

        public async Task<RespuestaDTO> AutorizarFacturaXOrdenCompra(FacturaXOrdenCompraDTO facturaXOrdenCompra) {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se autorizo la factura";
            if (facturaXOrdenCompra.Estatus != 1) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "La factura debe ser capturada para poder autorizarla";
                return respuesta;
            }

            var InusmosXOC = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(facturaXOrdenCompra.IdOrdenCompra);
            var totalOc = InusmosXOC.Sum(z => z.ImporteConIva);
            var ordeCompra = await _ordenCompraService.ObtenXId(facturaXOrdenCompra.IdOrdenCompra);
            var facturasXOC = await _facturaXOrdenCompraService.ObtenerXIdOrdenCompra(facturaXOrdenCompra.IdOrdenCompra);
            var facturasPagadas = facturasXOC.Where(z => z.Estatus == 3 || z.Estatus == 4);

            var totalF = facturasPagadas.Sum(z => z.TotalSaldado);
            if (totalF >= totalOc) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ya se a facturado en su totalidad";
                return respuesta;
            }

            var OCxMB = await _ordenCompraXMovimientoBancarioService.ObtenXIdOrdenCompra(facturaXOrdenCompra.IdOrdenCompra);
            var OCxMBordenados = OCxMB.Where(z => z.Estatus == 1).OrderBy(z => z.Id);

            if (ordeCompra.TotalSaldado > totalF) {
                decimal diferencia = ordeCompra.TotalSaldado - (decimal)totalF;
                if (diferencia > 0) {
                    var factura = await _facturaService.ObtenXId(facturaXOrdenCompra.IdFactura);
                    decimal TotalFactura = 0;
                    if (diferencia >= factura.Total)
                    {
                        facturaXOrdenCompra.TotalSaldado = factura.Total;
                        facturaXOrdenCompra.Estatus = 4;
                        TotalFactura = factura.Total;
                    }
                    else if (diferencia < factura.Total)
                    {
                        facturaXOrdenCompra.TotalSaldado = factura.Total - diferencia;
                        facturaXOrdenCompra.Estatus = 3;
                        TotalFactura = factura.Total - diferencia;
                    }

                    var editaFacturaXOC = await _facturaXOrdenCompraService.Editar(facturaXOrdenCompra);
                    if (!editaFacturaXOC)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No se pudo autorizar la facura";
                        return respuesta;
                    }

                    var facturasXOC2 = await _facturaXOrdenCompraService.ObtenerXIdOrdenCompra(facturaXOrdenCompra.IdOrdenCompra);
                    var facturasPagadas2 = facturasXOC.Where(z => z.Estatus == 3 || z.Estatus == 4);

                    foreach (var OCMB in OCxMBordenados)
                    {
                        var facturaXMB = await _facturaXOrdenCompraXMovimientoBancarioService.ObtenXIdMovimientoBancario(OCMB.IdMovimientoBancario);
                        var facturaXMBsinCancelar = facturaXMB.Where(z => z.Estatus == 1);
                        decimal TotalFMB = 0;
                        foreach (var fmb in facturaXMBsinCancelar)
                        {
                            foreach (var fxoc in facturasPagadas2)
                            {
                                if (fmb.IdFacturaXOrdenCompra == fxoc.Id)
                                {
                                    TotalFMB += fmb.TotalSaldado;
                                }
                            }
                        }

                        if (TotalFMB < OCMB.TotalSaldado)
                        {
                            decimal diferecia = (decimal)OCMB.TotalSaldado - TotalFMB;
                            if (diferecia >= TotalFactura)
                            {
                                //crearFacturaMB
                                await _facturaXOrdenCompraXMovimientoBancarioService.Crear(new FacturaXOrdenCompraXMovimientoBancarioDTO()
                                {
                                    IdFacturaXOrdenCompra = facturaXOrdenCompra.Id,
                                    Estatus = 1,
                                    IdMovimientoBancario = OCMB.IdMovimientoBancario,
                                    TotalSaldado = TotalFactura,
                                });
                            }
                            else
                            {
                                //crearFactuaMB
                                await _facturaXOrdenCompraXMovimientoBancarioService.Crear(new FacturaXOrdenCompraXMovimientoBancarioDTO()
                                {
                                    IdFacturaXOrdenCompra = facturaXOrdenCompra.Id,
                                    Estatus = 1,
                                    IdMovimientoBancario = OCMB.IdMovimientoBancario,
                                    TotalSaldado = diferecia,
                                });
                                TotalFactura -= diferecia;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    facturaXOrdenCompra.Estatus = 2;
                    var editaFacturaXOC = await _facturaXOrdenCompraService.Editar(facturaXOrdenCompra);
                    if (!editaFacturaXOC)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No se pudo autorizar la facura";
                        return respuesta;
                    }
                }
            }
            else
            {
                facturaXOrdenCompra.Estatus = 2;
                var editaFacturaXOC = await _facturaXOrdenCompraService.Editar(facturaXOrdenCompra);
                if (!editaFacturaXOC)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo autorizar la facura";
                    return respuesta;
                }
            }

            
            return respuesta;
        }

        public async Task<RespuestaDTO> CancelarFacturaXOrdenCompra(FacturaXOrdenCompraDTO facturaXOrdenCompra) {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se ccanceó la factura";

            if (facturaXOrdenCompra.Estatus != 1)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "la factura ya ha sido autorizada";
                return respuesta;
            }
            facturaXOrdenCompra.Estatus = 5;
            var editaFacturaXOC = await _facturaXOrdenCompraService.Editar(facturaXOrdenCompra);
            var factura = await _facturaService.ObtenXId(facturaXOrdenCompra.IdFactura);
            factura.Estatus = 2;
            var editarFactura = await _facturaService.Cancelar(factura);

            return respuesta;
        }
    }
}
