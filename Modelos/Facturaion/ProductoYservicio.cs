using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturaion;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO.Modelos.Facturacion;

public partial class ProductoYservicioAbstract
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int? IdUnidad { get; set; }

    public int IdProductoYservicioSat { get; set; }

    public int? IdUnidadSat { get; set; }
    public int IdCategoriaProductoYServicio { get; set; }
}
public partial class ProductoYservicio: ProductoYservicioAbstract
{
    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual ProductoYservicioSat IdProductoYservicioSatNavigation { get; set; } = null!;

    public virtual Unidad IdUnidadNavigation { get; set; } = null!;

    public virtual UnidadSat IdUnidadSatNavigation { get; set; } = null!;
    public virtual CategoriaProductoYServicio IdCategoriaProductoYServicioNavigation { get; set; } = null!;
    public virtual SubcategoriaProductoYServicio IdSubcategoriaProductoYServicioNavigation { get; set; } = null!;
}
