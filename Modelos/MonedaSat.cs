using ERP_TECKIO.Modelos;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class MonedaSat
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Moneda { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
