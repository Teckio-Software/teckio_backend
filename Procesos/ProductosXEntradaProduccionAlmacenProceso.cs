using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ProductosXEntradaProduccionAlmacenProceso<TContext> where TContext: DbContext
    {
        private readonly IProductoXEntradaProduccionAlmacenService<TContext> _service;

        public ProductosXEntradaProduccionAlmacenProceso(IProductoXEntradaProduccionAlmacenService<TContext> service)
        {
            _service = service;
        }

        public async Task<RespuestaDTO> Crear(ProductosXEntradaProduccionAlmacenDTO productos)
        {
            try
            {
                var resultado = await _service.Crear(productos);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar crear el detalle de entrada",
                    Estatus = false

                };
            }
        }

        public async Task<ProductosXEntradaProduccionAlmacenDTO> CrearYObtener(ProductosXEntradaProduccionAlmacenDTO productos)
        {
            try
            {
                var resultado = await _service.CrearYObtener(productos);
                return resultado;
            }
            catch
            {
                return new ProductosXEntradaProduccionAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProductosXEntradaProduccionAlmacenDTO productos)
        {
            try
            {
                var resultado = await _service.Editar(productos);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar editar el detalle de entrada",
                    Estatus = false

                };
            }
        }

        public async Task<RespuestaDTO> Eliminar(int id)
        {
            try
            {
                var productos = new ProductosXEntradaProduccionAlmacenDTO
                {
                    Id = id
                };
                var resultado = await _service.Eliminar(productos);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar eliminar el detalle de entrada",
                    Estatus = false

                };
            }
        }
    }
}
