using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class UnidadSat
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ProductoYservicio> ProductoYservicios { get; set; } = new List<ProductoYservicio>();
}
