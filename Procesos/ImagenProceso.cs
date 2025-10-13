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
        private readonly LogProcess _logProcess;

        public ImagenProceso(
            IConfiguration configuration,
            IImagenService<TContext> imagenService,
            LogProcess logProcess
            )
        {
            _Configuration = configuration;
            _ImagenService = imagenService;
            _logProcess = logProcess;
        }

        //public async Task<RespuestaDTO> SeleccionaImagen(int id, List<System.Security.Claims.Claim> claims)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    var IdUsStr = claims.Where(z => z.Type == "idUsuario").ToList();
        //    string metodo = "ObtenerSeleccionada";
        //    if (IdUsStr[0].Value == null)
        //    {
        //        respuesta.Descripcion = "La información del usuario es inconsistente";
        //        respuesta.Estatus = false;
        //        return respuesta;
        //    }
        //    int IdUsuario = int.Parse(IdUsStr[0].Value);
        //    try
        //    {
        //        var imagen = await _ImagenService.Obtener(id);
        //        var imagenSeleccionada = await _ImagenService.ObtenerTodos();
        //        imagenSeleccionada = imagenSeleccionada.Where(i => i.Seleccionado).ToList();
        //        foreach (var img in imagenSeleccionada)
        //        {
        //            img.Seleccionado = false;
        //            await _ImagenService.Editar(img);
        //        }
        //        imagen.Seleccionado = true;
        //        respuesta = await _ImagenService.Editar(imagen);
        //        if (respuesta.Estatus)
        //        {
        //            await _logProcess.RegistrarLog(NivelesLog.Critical, metodo, "Imagen seleccionada exitosamente", "", IdUsuario, 1);
        //            respuesta.Descripcion = "Imagen seleccionada exitosamente";
        //        }
        //        else
        //        {
        //            await _logProcess.RegistrarLog(NivelesLog.Error, metodo, "Ocurrió un error al intentar seleccionar la imagen", "", IdUsuario, 1);
        //            respuesta.Descripcion = "Ocurrió un error al intentar seleccionar la imagen";
        //        }

        //        return respuesta;
        //    }
        //    catch
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Ocurrió un error al intentar seleccionar la imagen";
        //        await _logProcess.RegistrarLog(NivelesLog.Critical, metodo, "Ocurrió un error al intentar seleccionar la imagen", "", IdUsuario, 1);
        //        return respuesta;
        //    }
        //}

        public async Task<RespuestaDTO> GuardarImagen(IFormFile archivo, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var IdUsStr = claims.Where(z => z.Type == "idUsuario").ToList();
            string metodo = "ObtenerSeleccionada";
            if (IdUsStr[0].Value == null)
            {
                respuesta.Descripcion = "Los datos del usuario están incompletos";
                respuesta.Estatus = false;
                return respuesta;
            }
            int IdUsuario = int.Parse(IdUsStr[0].Value);
            try
            {
                //Obtiene la ruta base para guardar las imagenes
                var ruta = _Configuration["Rutas:Imagenes"];
                if (ruta == null || archivo == null)
                {
                    await _logProcess.RegistrarLog(NivelesLog.Error, metodo, "No se encontró la ruta de destino para las imágenes", "", IdUsuario, 1);
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró la ruta de destino para las imágenes";
                    return respuesta;
                }
                //Comprueba que la extensión del archivo sea correcta
                if (!(System.IO.Path.GetExtension(archivo.FileName).ToLower().Contains("png")|| System.IO.Path.GetExtension(archivo.FileName).ToLower().Contains("jpg") || System.IO.Path.GetExtension(archivo.FileName).ToLower().Contains("jpeg")))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El tipo de archivo es incorrecto, debe ser jpg, jpeg, webp o png";
                    await _logProcess.RegistrarLog(NivelesLog.Warn, metodo, "El tipo de archivo es incorrecto, debe ser jpg, jpeg o png", "", IdUsuario, 1);
                    return respuesta;
                }
                //if (System.IO.Path.GetExtension(archivo.FileName).ToLower().Contains("webp"))
                //{

                //}
                var fecha = DateTime.Now;
                var mes = fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
                //Obtiene el nombre del archivo
                var nombreArchivo = DateTime.Now.Millisecond + archivo.FileName;
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
                        await _logProcess.RegistrarLog(NivelesLog.Error, metodo, "Ocurrió un error al intentar escribir el archivo", "", IdUsuario, 1);
                        respuesta.Descripcion = "Ocurrió un error al intentar escribir el archivo";
                        respuesta.Estatus = false;
                        return respuesta;
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
                        var contenido = memoryStream.ToArray();
                        pesoBytes = contenido.Length;
                        await File.WriteAllBytesAsync(rutaFinal, contenido);
                    }
                    catch
                    {
                        await _logProcess.RegistrarLog(NivelesLog.Error, metodo, "Ocurrió un error al intentar escribir el archivo", "", IdUsuario, 1);
                        respuesta.Descripcion = "Ocurrió un error al intentar escribir el archivo";
                        respuesta.Estatus = false;
                        return respuesta;
                    }
                }
                ImagenDTO imagen = new ImagenDTO();
                imagen.Tipo = System.IO.Path.GetExtension(archivo.FileName);
                imagen.Ruta = rutaFinal;
                try
                {
                    imagen.Seleccionado = true;
                    var resultado = await _ImagenService.CrearYObtener(imagen);
                    await _logProcess.RegistrarLog(NivelesLog.Info, metodo, "Imagen creada exitosamente", "", IdUsuario, 1);
                    var imagenSeleccionada = await _ImagenService.ObtenerTodos();
                    imagenSeleccionada = imagenSeleccionada.Where(i => i.Seleccionado && i.Id!=resultado.Id).ToList();
                    foreach (var img in imagenSeleccionada)
                    {
                        img.Seleccionado = false;
                        await _ImagenService.Editar(img);
                    }
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "data:image/"+(resultado.Tipo.Replace(".",""))+";base64,"+ Convert.ToBase64String(File.ReadAllBytes(imagen.Ruta));
                    return respuesta;
                }
                catch
                {
                    await _logProcess.RegistrarLog(NivelesLog.Error, metodo, "Ocurrió un error al intentar crear un registro para la imagen", "", IdUsuario, 1);
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar crear un registro para la imagen";
                    return respuesta;
                }
            }
            catch
            {
                await _logProcess.RegistrarLog(NivelesLog.Critical, metodo, "Ocurrió un error al intentar subir la imagen", "", IdUsuario, 1);
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar subir la imagen";
                return respuesta;
            }
        }

        //public async Task<int> EditarImagen(IFormFile archivo, int idImagen)
        //{
        //    try
        //    {
        //        var imagen = await _ImagenService.Obtener(idImagen);
        //        if (imagen.Id <= 0)
        //        {
        //            return 0;
        //        }
        //        //Obtiene la ruta base para guardar las imagenes
        //        var ruta = _Configuration["Rutas:Imagenes"];
        //        if (ruta == null || archivo == null)
        //        {
        //            return 0;
        //        }
        //        if (File.Exists(imagen.Ruta))
        //        {
        //            File.Delete(imagen.Ruta);
        //        }
        //        var fecha = DateTime.Now;
        //        var mes = fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
        //        //Obtiene el nombre del archivo
        //        var nombreArchivo = archivo.FileName;
        //        //Genera la ruta compuesta
        //        var rutaCompuesta = Path.Combine(ruta, fecha.Year.ToString(), mes, fecha.Day.ToString());
        //        //Comprueba si la ruta existe, si no existe la crea
        //        if (!Directory.Exists(rutaCompuesta))
        //        {
        //            try
        //            {
        //                Directory.CreateDirectory(rutaCompuesta);
        //            }
        //            catch
        //            {
        //                return 0;
        //            }
        //        }
        //        //Crea la ruta final con el nombre del archivo
        //        var rutaFinal = Path.Combine(rutaCompuesta, nombreArchivo);
        //        var pesoBytes = 0;
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            try
        //            {
        //                //Lee el archivo y lo guarda en la ruta final
        //                await archivo.CopyToAsync(memoryStream);
        //                var contenIdo = memoryStream.ToArray();
        //                pesoBytes = contenIdo.Length;
        //                await File.WriteAllBytesAsync(ruta, contenIdo);
        //            }
        //            catch
        //            {
        //                return 0;
        //            }
        //        }

        //        imagen.Ruta = rutaFinal;
        //        try
        //        {
        //            var resultado = await _ImagenService.Editar(imagen);
        //            return imagen.Id;
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        //public async Task<RespuestaDTO> ElimnarImagen(int id)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var imagen = await _ImagenService.Obtener(id);
        //        if (imagen.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se encontró la imagen que se quiere eliminar";
        //            return respuesta;
        //        }
        //        respuesta = await _ImagenService.Eliminar(id);
        //        //Si sale bien elimina la imagen de su sitio.
        //        if (respuesta.Estatus)
        //        {
        //            if (File.Exists(imagen.Ruta))
        //            {
        //                File.Delete(imagen.Ruta);
        //            }
        //        }
        //        return respuesta;
        //    }
        //    catch
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Ocurrio un error al intentar eliminar la imagen";
        //        return respuesta;
        //    }

        //}

        public async Task<ImagenDTO> ObtenerSeleccionada(List<System.Security.Claims.Claim> claims)
        {
            var IdUsStr = claims.Where(z => z.Type == "idUsuario").ToList();
            string metodo = "ObtenerSeleccionada";
            if (IdUsStr[0].Value == null)
            {
                return new ImagenDTO();
            }
            int IdUsuario = int.Parse(IdUsStr[0].Value);
            try
            {
                var imagenes = await _ImagenService.ObtenerTodos();
                var imagen = imagenes.Where(i => i.Seleccionado).FirstOrDefault();
                if (imagen == null)
                {
                    return new ImagenDTO();
                }
                byte[] bytesImagen = File.ReadAllBytes(imagen.Ruta);
                if (bytesImagen.Length <= 0)
                {
                    return new ImagenDTO();
                }
                imagen.Base64 = Convert.ToBase64String(bytesImagen);
                if (imagen.Id > 0)
                {
                    await _logProcess.RegistrarLog(NivelesLog.Info, metodo, "Imagen " + imagen.Id + " consultada exitosamente", "", IdUsuario, 1);
                }
                return imagen;
            }
            catch
            {
                await _logProcess.RegistrarLog(NivelesLog.Critical, metodo, "Ocurrió un error al intentar consultar la imagen", "", IdUsuario, 1);
                return new ImagenDTO();
            }
        }

        public async Task<List<ImagenDTO>> ObtenerImagenes (List<System.Security.Claims.Claim> claims)
        {
            var IdUsStr = claims.Where(z => z.Type == "idUsuario").ToList();
            string metodo = "ObtenerSeleccionada";
            if (IdUsStr[0].Value == null)
            {
                return new List<ImagenDTO>();
            }
            int IdUsuario = int.Parse(IdUsStr[0].Value);
            var imagenes = await _ImagenService.ObtenerTodos();
            if (imagenes.Count > 0)
            {
                await _logProcess.RegistrarLog(NivelesLog.Info, metodo, "Imágenes obtenidas exitosamente", "", IdUsuario, 1);
                return imagenes;
            }
            else
            {
                await _logProcess.RegistrarLog(NivelesLog.Warn, metodo, "No se encontró ninguna imagen", "", IdUsuario, 1);
                return new List<ImagenDTO>();
            }
        }

    }
}
