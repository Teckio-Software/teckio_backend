using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class SalidaProduccionAlmacenProceso<TContext> where TContext : DbContext
    {
        private readonly ISalidaProduccionAlmacenService<TContext> _salidaProduccionAlmacenService;
        private readonly IProductosXSalidaProduccionAlmacenService<TContext> _productosXSalidaProduccionAlmacenService;
        private readonly IExistenciaProductoAlmacenService<TContext> _existenciaProductoAlmacenService;
        private readonly IProductoYservicioService<TContext> _productoYservicioService;
        private readonly IEntradaProduccionAlmacenService<TContext> _entradaProduccionAlmacenService;

        public SalidaProduccionAlmacenProceso(
            ISalidaProduccionAlmacenService<TContext> salidaProduccionAlmacenService,
            IProductosXSalidaProduccionAlmacenService<TContext> productosXSalidaProduccionAlmacenService,
            IExistenciaProductoAlmacenService<TContext> existenciaProductoAlmacenService,
            IProductoYservicioService<TContext> productoYservicioService,
            IEntradaProduccionAlmacenService<TContext> entradaProduccionAlmacenService
            )
        {
            _salidaProduccionAlmacenService = salidaProduccionAlmacenService;
            _productosXSalidaProduccionAlmacenService = productosXSalidaProduccionAlmacenService;
            _existenciaProductoAlmacenService = existenciaProductoAlmacenService;
            _productoYservicioService = productoYservicioService;
            _entradaProduccionAlmacenService = entradaProduccionAlmacenService;
        }

        public async Task<RespuestaDTO> CrearSalida(SalidaProduccionAlmacenDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //Primero crea la salida de producción al almacén
                var objeto = await _salidaProduccionAlmacenService.CrearYObtener(parametro);
                if (objeto.Id > 0)
                {
                    //Luego recorre la lista de productos para ir verificando y descontando la existencia en el almacén
                    foreach (var producto in parametro.ProductosXsalidaProduccionAlmacens)
                    {
                        var productoEncontrado = await _productoYservicioService.ObtenerXId(producto.IdProductoYservicio);

                        //Si no encuentra el producto, elimina la salida de producción al almacén y regresa error
                        if (productoEncontrado.Id <= 0)
                        {
                            await _salidaProduccionAlmacenService.Eliminar(objeto.Id);

                            respuesta.Estatus = false;
                            respuesta.Descripcion = "No se encontro el producto con ID: " + producto.IdProductoYservicio;
                            return respuesta;
                        }

                        //Si el producto no es de tipo servicio, verifica la existencia en el almacén
                        if (productoEncontrado.IdUnidad != 2)
                        {
                            var existencia = await _existenciaProductoAlmacenService.ObtenerExistencia(producto.IdProductoYservicio, objeto.IdAlmacen);

                            //Si no hay existencia o no encuentra el producto en el almacén, elimina la salida de producción al almacén y regresa error
                            if (existencia.Id <= 0)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);
                                respuesta.Estatus = false;
                                respuesta.Descripcion = "No se encontro existencia del producto con ID: " + producto.IdProductoYservicio + " en el almacén con ID: " + objeto.IdAlmacen;
                                return respuesta;
                            }

                            //Si la cantidad a sacar es mayor a la existencia, elimina la salida de producción al almacén y regresa error
                            if (existencia.Cantidad < producto.Cantidad)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);
                                respuesta.Estatus = false;
                                respuesta.Descripcion = "No hay suficiente existencia del producto con ID: " + producto.IdProductoYservicio + " en el almacén con ID: " + objeto.IdAlmacen;
                                return respuesta;
                            }

                            //Descuenta la existencia y actualiza
                            existencia.Cantidad -= producto.Cantidad;
                            var existenciaActualizada = await _existenciaProductoAlmacenService.Editar(existencia);

                            //Si no se actualiza la existencia, elimina la salida de producción al almacén y regresa error
                            if (!existenciaActualizada.Estatus)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);
                                respuesta.Estatus = false;
                                respuesta.Descripcion = "Ocurrio un error al actualizar la existencia del producto con ID: " + producto.IdProductoYservicio + " en el almacén con ID: " + objeto.IdAlmacen;
                                return respuesta;
                            }

                            producto.IdSalidaProduccionAlmacen = objeto.Id;
                            var productoCreado = await _productosXSalidaProduccionAlmacenService.Crear(producto);
                            if (!productoCreado.Estatus)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);

                                respuesta.Estatus = false;
                                respuesta.Descripcion = "Ocurrio un error al crear los productos de la salida de producción al almacén";
                                return respuesta;
                            }
                        }

                    }
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Salida de producción al almacén creada correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al crear la salida de producción al almacén";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al crear la salida de producción al almacén";
                return respuesta;
            }
        }

        public async Task<SalidaProduccionAlmacenDTO> CrearYObtenerSalida(SalidaProduccionAlmacenDTO parametro)
        {
            try
            {
                //Primero crea la salida de producción al almacén
                var objeto = await _salidaProduccionAlmacenService.CrearYObtener(parametro);
                if (objeto.Id > 0)
                {
                    //Luego recorre la lista de productos para ir verificando y descontando la existencia en el almacén
                    foreach (var producto in parametro.ProductosXsalidaProduccionAlmacens)
                    {
                        var productoEncontrado = await _productoYservicioService.ObtenerXId(producto.IdProductoYservicio);

                        //Si no encuentra el producto, elimina la salida de producción al almacén y regresa error
                        if (productoEncontrado.Id <= 0)
                        {
                            await _salidaProduccionAlmacenService.Eliminar(objeto.Id);

                            return new SalidaProduccionAlmacenDTO();
                        }

                        //Si el producto no es de tipo servicio, verifica la existencia en el almacén
                        if (productoEncontrado.IdUnidad != 2)
                        {
                            var existencia = await _existenciaProductoAlmacenService.ObtenerExistencia(producto.IdProductoYservicio, objeto.IdAlmacen);

                            //Si no hay existencia o no encuentra el producto en el almacén, elimina la salida de producción al almacén y regresa error
                            if (existencia.Id <= 0)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);
                                return new SalidaProduccionAlmacenDTO();

                            }

                            //Si la cantidad a sacar es mayor a la existencia, elimina la salida de producción al almacén y regresa error
                            if (existencia.Cantidad < producto.Cantidad)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);
                                return new SalidaProduccionAlmacenDTO();

                            }

                            //Descuenta la existencia y actualiza
                            existencia.Cantidad -= producto.Cantidad;
                            var existenciaActualizada = await _existenciaProductoAlmacenService.Editar(existencia);

                            //Si no se actualiza la existencia, elimina la salida de producción al almacén y regresa error
                            if (!existenciaActualizada.Estatus)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);
                                return new SalidaProduccionAlmacenDTO();

                            }

                            producto.IdSalidaProduccionAlmacen = objeto.Id;
                            producto.TipoOrigen = 2;
                            var productoCreado = await _productosXSalidaProduccionAlmacenService.Crear(producto);
                            if (!productoCreado.Estatus)
                            {
                                await _salidaProduccionAlmacenService.Eliminar(objeto.Id);

                                return new SalidaProduccionAlmacenDTO();

                            }
                        }

                    }
                    return objeto;

                }
                else
                {
                    return new SalidaProduccionAlmacenDTO();

                }
            }
            catch
            {
                return new SalidaProduccionAlmacenDTO();

            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var salida = await _salidaProduccionAlmacenService.ObtenerXId(Id);
                if(salida.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro la salida de producción al almacén";
                    return respuesta;
                }
                var productos = await _productosXSalidaProduccionAlmacenService.ObtenerXSalida(Id);
                foreach (var producto in productos)
                {
                    var existencia = await _existenciaProductoAlmacenService.ObtenerExistencia(producto.IdProductoYservicio, salida.IdAlmacen);
                    if (existencia.Id > 0)
                    {
                        existencia.Cantidad += producto.Cantidad;
                        await _existenciaProductoAlmacenService.Editar(existencia);
                    }
                    await _productosXSalidaProduccionAlmacenService.Eliminar(producto.Id);
                }
                respuesta = await _salidaProduccionAlmacenService.Eliminar(Id);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Salida de producción al almacén eliminada correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al eliminar la salida de producción al almacén";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al eliminar la salida de producción al almacén";
                return respuesta;
            }
        }

    }
}
