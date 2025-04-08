using ERP_TECKIO.Modelos;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class ProductoYservicio
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int IdUnidad { get; set; }

    public int IdProductoYservicioSat { get; set; }

    public int IdUnidadSat { get; set; }

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual ProductoYservicioSat IdProductoYservicioSatNavigation { get; set; } = null!;

    public virtual Unidad IdUnidadNavigation { get; set; } = null!;

    public virtual UnidadSat IdUnidadSatNavigation { get; set; } = null!;
}
