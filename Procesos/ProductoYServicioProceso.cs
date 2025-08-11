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

        public async Task<List<ProductoYServicioConjuntoDTO>> ObtenerProductosYServicios()
        {
            //[pendiente]
            try
            {
                var items = _dbContext.Database.SqlQueryRaw<ProductoYServicioConjuntoDTO>(""""
                select 
                ps.Id as Id,
                ps.Codigo as CodigoPs,
                ps.Descripcion as DescripcionPS,
                u.Id as IdUnidad,
                u.Descripcion as DescripcionUnidad,
                pss.Id as IdPSS,
                pss.Clave as ClavePSS,
                pss.Descripcion as DescripcionPSS,
                us.Id as IdUnidSSat,
                us.Tipo as TipoUS,
                us.Clave as ClaveUS,
                us.Nombre as NombreUs,
                cps.Id as IdCPS,
                cps.Descripcion as DescripcionCPS,
                sps.Id as IdSPS,
                sps.Descripcion DescripcionSPS
                from Factura.ProductoYServicio ps 
                join Factura.ProductoYServicioSat pss on ps.IdProductoYServicioSat = pss.Id 
                join Factura.Unidad u on u.Id = ps.IdUnidad 
                join Factura.UnidadSat us on us.Id = ps.IdUnidadSat 
                join Factura.CategoriaProductoYServicio cps on cps.Id = ps.IdCategoriaProductoYServicio 
                join Factura.SubcategoriaProductoYServicio sps on sps.Id = ps.IdSubategoriaProductoYServicio
                """").ToList();

                //if (items.Count > 0)
                //{
                //    string json = string.Join("", items);
                //    var datos = JsonSerializer.Deserialize<List<ProductoYServicioConjuntoDTO>>(json);
                //    return datos;
                //}
                if (items.Count > 0)
                {
                    return items;
                }
                else
                {
                    return new List<ProductoYServicioConjuntoDTO>();
                }
            }
            catch
            {
                return new List<ProductoYServicioConjuntoDTO>();
            }
        }

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
