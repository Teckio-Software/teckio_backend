using System;
using System.Collections.Generic;

namespace ERP_TECKIO.Modelos.Factura;

public partial class Unidad
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ProductoYservicio> ProductoYservicios { get; set; } = new List<ProductoYservicio>();
}
