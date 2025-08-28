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

        //public async Task<RespuestaDTO> Crear(string modelo, IFormFile archivo)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        ParametrosImpresionPuDTO parametros = JsonSerializer.Deserialize<ParametrosImpresionPuDTO>(modelo);
        //        if(parametros == null)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "Ocurrió un error al intentar guardar los parámetros de impresión";
        //            return respuesta;
        //        }
        //        if (archivo != null)
        //        {
        //            var resultadoImagen = await _ImagenProceso.GuardarImagen(archivo);
        //            if (resultadoImagen <= 0)
        //            {
        //                respuesta.Estatus = false;
        //                respuesta.Descripcion = "Ocurrió un error al intentar guardar la imagen de los parámetros de impresión";
        //                return respuesta;
        //            }
        //            parametros.IdImagen = resultadoImagen;
        //            respuesta = await _ParametrosImpresionPuService.Crear(parametros);
        //            return respuesta;
        //        }
        //        else
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se recibió ninguna imagen para los parámetros de impresión";
        //            return respuesta;
        //        }
        //    }
        //    catch
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Ocurrió un error al intentar guardar los parámetros de impresión";
        //        return respuesta;
        //    }
        //}

        public async Task<RespuestaDTO> Crear(ParametrosImpresionPuDTO parametros)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //Validaciones
                if (string.IsNullOrWhiteSpace(parametros.Nombre))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El nombre de los parámetros de impresión es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.EncabezadoCentro))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El encabezado central es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.EncabezadoDerecho))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El encabezado derecho es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.EncabezadoIzquierdo))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El encabezado izquierdo es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.PieCentro))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El pie central es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.PieDerecho))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El pie derecho es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.PieIzquierdo))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El pie izquierdo es obligatorio";
                    return respuesta;
                }
                if (parametros.MargenSuperior == null || parametros.MargenSuperior < 0)
                {
                    parametros.MargenSuperior = 30;
                }
                if (parametros.MargenInferior == null || parametros.MargenInferior < 0)
                {
                    parametros.MargenInferior = 30;
                }
                if (parametros.MargenDerecho == null || parametros.MargenDerecho < 0)
                {
                    parametros.MargenDerecho = 30;
                }
                if (parametros.MargenIzquierdo == null || parametros.MargenIzquierdo < 0)
                {
                    parametros.MargenIzquierdo = 30;
                }
                respuesta = await _ParametrosImpresionPuService.Crear(parametros);
                return respuesta;
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

        //public async Task<List<ParametrosImpresionPuDTO>> ObtenerXIdCliente(int idCliente)
        //{
        //    try
        //    {
        //        var resultado = await _ParametrosImpresionPuService.ObtenerPorCliente(idCliente);
        //        return resultado;
        //    }
        //    catch
        //    {
        //        return new List<ParametrosImpresionPuDTO>();
        //    }
        //}

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

        public async Task<RespuestaDTO> EditarParametros(ParametrosImpresionPuDTO parametros)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //Validaciones
                if (string.IsNullOrWhiteSpace(parametros.Nombre))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El nombre de los parámetros de impresión es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.EncabezadoCentro))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El encabezado central es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.EncabezadoDerecho))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El encabezado derecho es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.EncabezadoIzquierdo))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El encabezado izquierdo es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.PieCentro))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El pie central es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.PieDerecho))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El pie derecho es obligatorio";
                    return respuesta;
                }
                if (string.IsNullOrWhiteSpace(parametros.PieIzquierdo))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El pie izquierdo es obligatorio";
                    return respuesta;
                }
                if (parametros.MargenSuperior == null || parametros.MargenSuperior < 0)
                {
                    parametros.MargenSuperior = 30;
                }
                if (parametros.MargenInferior == null || parametros.MargenInferior < 0)
                {
                    parametros.MargenInferior = 30;
                }
                if (parametros.MargenDerecho == null || parametros.MargenDerecho < 0)
                {
                    parametros.MargenDerecho = 30;
                }
                if (parametros.MargenIzquierdo == null || parametros.MargenIzquierdo < 0)
                {
                    parametros.MargenIzquierdo = 30;
                }
                respuesta = await _ParametrosImpresionPuService.Editar(parametros);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar los parámetros";
                return respuesta;
            }
        }

        //public async Task<RespuestaDTO> EditarParametrosConImagen(string modelo, IFormFile archivo)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        ParametrosImpresionPuDTO parametros = JsonSerializer.Deserialize<ParametrosImpresionPuDTO>(modelo);
        //        if (parametros == null)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "Ocurrió un error al intentar editar los parámetros de impresión";
        //            return respuesta;
        //        }
        //        if (archivo != null)
        //        {
        //            var resultadoImagen = await _ImagenProceso.EditarImagen(archivo, (int)parametros.IdImagen);
        //            if (resultadoImagen <= 0)
        //            {
        //                respuesta.Estatus = false;
        //                respuesta.Descripcion = "Ocurrió un error al intentar editar la imagen de los parámetros de impresión";
        //                return respuesta;
        //            }
        //            parametros.IdImagen = resultadoImagen;
        //            respuesta = await _ParametrosImpresionPuService.Editar(parametros);
        //            return respuesta;
        //        }
        //        else
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se recibió ninguna imagen para los parámetros de impresión";
        //            return respuesta;
        //        }
        //    }
        //    catch
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Ocurrio un error al intentar editar los parámetros";
        //        return respuesta;
        //    }
        //}

        public async Task<RespuestaDTO> EliminarParametro(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var parametros = await _ParametrosImpresionPuService.Obtener(id);
                if (parametros.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontrarón los parámetros";
                    return respuesta;
                }
                var resultPar = await _ParametrosImpresionPuService.Eliminar(id);
                return resultPar;
                //El fragmento de código comentado eliminaba la imagen en automático al eliminar el parámetro.
                //if (resultPar.Estatus)
                //{
                //    var resultImg = await _ImagenProceso.ElimnarImagen((int)parametros.IdImagen);
                //    if (resultImg.Estatus)
                //    {
                //        respuesta.Estatus = true;
                //        respuesta.Descripcion = "Parámetros eliminados exitosamente";
                //        return respuesta;
                //    }
                //    else
                //    {
                //        return resultImg;
                //    }
                //}
                //else
                //{
                //    respuesta.Estatus = false;
                //    respuesta.Descripcion = resultPar.Descripcion;
                //    return respuesta;
                //}
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar los parametros";
                return respuesta;
            }
        }
    }
}
