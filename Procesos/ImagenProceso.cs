using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using ExcelDataReader.Log;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ImagenProceso<TContext> where TContext : DbContext
    {
        private readonly IConfiguration _Configuration;
        private readonly IImagenService<TContext> _ImagenService;

        public ImagenProceso(
            IConfiguration configuration,
            IImagenService<TContext> imagenService
            )
        {
            _Configuration = configuration;
            _ImagenService = imagenService;
        }

        public async Task<int> GuardarImagen(IFormFile archivo)
        {
            try
            {
                //Obtiene la ruta base para guardar las imagenes
                var ruta = _Configuration["Rutas:Imagenes"];
                if (ruta == null || archivo == null)
                {
                    return 0;
                }
                var fecha = DateTime.Now;
                var mes = fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
                //Obtiene el nombre del archivo
                var nombreArchivo = archivo.FileName;
                //Genera la ruta compuesta
                var rutaCompuesta = Path.Combine(ruta, fecha.Year.ToString(), mes, fecha.Day.ToString());
                //Comprueba si la ruta existe, si no existe la crea
                if (!Directory.Exists(rutaCompuesta))
                {
                    try
                    {
                        Directory.CreateDirectory(rutaCompuesta);
                    }
                    catch
                    {
                       return 0;
                    }
                }
                //Crea la ruta final con el nombre del archivo
                var rutaFinal = Path.Combine(rutaCompuesta, nombreArchivo);
                var pesoBytes = 0;
                using (var memoryStream = new MemoryStream())
                {
                    try
                    {
                        //Lee el archivo y lo guarda en la ruta final
                        await archivo.CopyToAsync(memoryStream);
                        var contenIdo = memoryStream.ToArray();
                        pesoBytes = contenIdo.Length;
                        await File.WriteAllBytesAsync(ruta, contenIdo);
                    }
                    catch
                    {
                        return 0;
                    }
                }
                ImagenDTO imagen = new ImagenDTO();

                imagen.Ruta = rutaFinal;
                try
                {
                    var resultado = await _ImagenService.CrearYObtener(imagen);
                    return imagen.Id;
                }
                catch
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> EditarImagen(IFormFile archivo, int idImagen)
        {
            try
            {
                var imagen = await _ImagenService.Obtener(idImagen);
                if (imagen.Id <= 0)
                {
                    return 0;
                }
                //Obtiene la ruta base para guardar las imagenes
                var ruta = _Configuration["Rutas:Imagenes"];
                if (ruta == null || archivo == null)
                {
                    return 0;
                }
                if (File.Exists(imagen.Ruta))
                {
                    File.Delete(imagen.Ruta);
                }
                var fecha = DateTime.Now;
                var mes = fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
                //Obtiene el nombre del archivo
                var nombreArchivo = archivo.FileName;
                //Genera la ruta compuesta
                var rutaCompuesta = Path.Combine(ruta, fecha.Year.ToString(), mes, fecha.Day.ToString());
                //Comprueba si la ruta existe, si no existe la crea
                if (!Directory.Exists(rutaCompuesta))
                {
                    try
                    {
                        Directory.CreateDirectory(rutaCompuesta);
                    }
                    catch
                    {
                        return 0;
                    }
                }
                //Crea la ruta final con el nombre del archivo
                var rutaFinal = Path.Combine(rutaCompuesta, nombreArchivo);
                var pesoBytes = 0;
                using (var memoryStream = new MemoryStream())
                {
                    try
                    {
                        //Lee el archivo y lo guarda en la ruta final
                        await archivo.CopyToAsync(memoryStream);
                        var contenIdo = memoryStream.ToArray();
                        pesoBytes = contenIdo.Length;
                        await File.WriteAllBytesAsync(ruta, contenIdo);
                    }
                    catch
                    {
                        return 0;
                    }
                }

                imagen.Ruta = rutaFinal;
                try
                {
                    var resultado = await _ImagenService.Editar(imagen);
                    return imagen.Id;
                }
                catch
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public async Task<RespuestaDTO> ElimnarImagen(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var imagen = await _ImagenService.Obtener(id);
                if (imagen.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró la imagen que se quiere eliminar";
                    return respuesta;
                }
                respuesta = await _ImagenService.Eliminar(id);
                //Si sale bien elimina la imagen de su sitio.
                if (respuesta.Estatus)
                {
                    if (File.Exists(imagen.Ruta))
                    {
                        File.Delete(imagen.Ruta);
                    }
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar la imagen";
                return respuesta;
            }

        }

        public async Task<ImagenDTO> ObtenerXId(int id)
        {
            try
            {
                var imagen = await _ImagenService.Obtener(id);
                byte[] bytesImagen = File.ReadAllBytes(imagen.Ruta);
                if (bytesImagen.Length <= 0)
                {
                    return new ImagenDTO();
                }
                imagen.Base64 = Convert.ToBase64String(bytesImagen);
                return imagen;
            }
            catch
            {
                return new ImagenDTO();
            }
        }
    }
}
