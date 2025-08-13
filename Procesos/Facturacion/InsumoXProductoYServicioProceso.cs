using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos.Facturacion
{
    public class InsumoXProductoYServicioProceso<TContext> where TContext: DbContext
    {
        private readonly TContext _dbContext;

        public InsumoXProductoYServicioProceso(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InsumoXProductoYServicioCompuestoDTO>> ObtenerLista(int id)
        {
            try
            {
                var items = _dbContext.Database.SqlQueryRaw<InsumoXProductoYServicioCompuestoDTO>(""""
                select 
                ips.Id as Id,
                ips.IdProductoYservicio as IdProductoYservicio,
                ips.IdInsumo as IdInsumo,
                ips.Cantidad as Cantidad,
                i.Clave as Clave,
                i.Descripcion as Descripcion
                from Factura.InsumoxProductoYServicio ips 
                join dbo.Insumo i on ips.IdInsumo = i.Id 
                where ips.IdProductoYservicio =
                """"+id).ToList();

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
                    return new List<InsumoXProductoYServicioCompuestoDTO>();
                }
            }
            catch
            {
                return new List<InsumoXProductoYServicioCompuestoDTO>();

            }
        }
    }
}
