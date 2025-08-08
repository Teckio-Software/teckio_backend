using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ERP_TECKIO.Procesos
{
    public class ProductoYServicioProceso<TContext> where TContext : DbContext
    {
        private readonly IProductoYservicioService<TContext> _productoYServicioService;

        private readonly TContext _dbContext;

        public ProductoYServicioProceso(IProductoYservicioService<TContext> productoYServicioService, TContext db)
        {
            _productoYServicioService = productoYServicioService;
            this._dbContext = db;
        }

        //public async Task<List<ProductoYServicioConjuntoDTO>> ObtenerProductosYServicios()
        //{
        //    //[pendiente]
        //    try
        //    {
        //        var items = _dbContext.Database.SqlQueryRaw<string>("""""
        //        select

        //        """"").ToList();

        //        if (items.Count > 0)
        //        {
        //            string json = string.Join("", items);
        //            var datos = JsonSerializer.Deserialize<List<ProductoYServicioConjuntoDTO>>(json);
        //            return datos;
        //        }
        //        else
        //        {
        //            return new List<ProductoYServicioConjuntoDTO>();
        //        }
        //    }
        //    catch
        //    {
        //        return new List<ProductoYServicioConjuntoDTO>();
        //    }
        //}

        public async Task<List<ProductoYservicioDTO>> ObtenerTodos()
        {
            try
            {
                var lista = await _productoYServicioService.ObtenerTodos();
                if (lista.Count > 0)
                {
                    return lista;
                }
                else
                {
                    return new List<ProductoYservicioDTO>();
                }
            }
            catch
            {
                return new List<ProductoYservicioDTO>();
            }
        }
    }
}
