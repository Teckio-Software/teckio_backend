using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ERP_TECKIO.Procesos
{
    public class ParametrosImpresionPuProceso<TContext> where TContext : DbContext
    {
        private readonly ImagenProceso<TContext> _ImagenProceso;
        private readonly IParametrosImpresionPuService<TContext> _ParametrosImpresionPuService;

        public ParametrosImpresionPuProceso(
            ImagenProceso<TContext> imagenProceso,
            IParametrosImpresionPuService<TContext> parametrosImpresionPuService
            )
        {
            _ImagenProceso = imagenProceso;
            _ParametrosImpresionPuService = parametrosImpresionPuService;
        }

        public async Task<RespuestaDTO> Crear(string modelo, IFormFile archivo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                ParametrosImpresionPuDTO parametros = JsonSerializer.Deserialize<ParametrosImpresionPuDTO>(modelo);
                if(parametros == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar guardar los parámetros de impresión";
                    return respuesta;
                }
                if (archivo != null)
                {
                    var resultadoImagen = await _ImagenProceso.GuardarImagen(archivo);
                    if (resultadoImagen <= 0)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "Ocurrió un error al intentar guardar la imagen de los parámetros de impresión";
                        return respuesta;
                    }
                    parametros.IdImagen = resultadoImagen;
                    respuesta = await _ParametrosImpresionPuService.Crear(parametros);
                    return respuesta;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se recibió ninguna imagen para los parámetros de impresión";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar guardar los parámetros de impresión";
                return respuesta;
            }
        }

        public async Task<ParametrosImpresionPuDTO> ObtenerXId(int id)
        {
            try
            {
                var resultado = await _ParametrosImpresionPuService.Obtener(id);
                return resultado;
            }
            catch
            {
                return new ParametrosImpresionPuDTO();
            }
        }

        public async Task<List<ParametrosImpresionPuDTO>> ObtenerXIdCliente(int idCliente)
        {
            try
            {
                var resultado = await _ParametrosImpresionPuService.ObtenerPorCliente(idCliente);
                return resultado;
            }
            catch
            {
                return new List<ParametrosImpresionPuDTO>();
            }
        }

        public async Task<List<ParametrosImpresionPuDTO>> ObtenerTodos()
        {
            try
            {
                var resultado = await _ParametrosImpresionPuService.ObtenerTodos();
                return resultado;
            }
            catch
            {
                return new List<ParametrosImpresionPuDTO>();
            }
        }
    }
}
