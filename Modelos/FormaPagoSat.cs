using ERP_TECKIO.Modelos;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class FormaPagoSat
{
    public int Id { get; set; }

    public string Clave { get; set; } = null!;

    public string Concepto { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
