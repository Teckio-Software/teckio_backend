using System;
using System.Collections.Generic;

namespace ERP_TECKIO.Modelos.Facturacion;

public partial class ProductoYservicioSat
{
    public int Id { get; set; }

    public string Clave { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ProductoYservicio> ProductoYservicios { get; set; } = new List<ProductoYservicio>();
}
