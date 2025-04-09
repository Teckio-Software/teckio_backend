using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using SpreadsheetLight;
using System.Xml;
using System.Xml.Linq;

namespace ERP_TECKIO.Procesos.Facturacion
{
    public class ObtenFacturaProceso<T> where T : DbContext
    {

        public ObtenFacturaProceso()
        {

        }

        public class ConceptosExcelDTO
        {
            public string ClaveProducto { get; set; }
            public string Descripcion { get; set; }
            public string Unidad { get; set; }
            public string ClaveUnidadSat { get; set; }
            public decimal CostoUnitario { get; set; }
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

        private List<string> LeerExcelConClosedXml(string rutaExcel)
        {
            var lista = new List<string>();

            using (var workbook = new XLWorkbook(rutaExcel))
            {
                var hoja = workbook.Worksheet(1); // Primera hoja
                int fila = 2;

                while (!string.IsNullOrWhiteSpace(hoja.Cell(fila, 1).GetString()))
                {
                    string nombreDocumento = hoja.Cell(fila, 3).GetString(); // Columna 3
                    lista.Add(nombreDocumento);
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
    }
}
