using ERP_TECKIO.Modelos;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO.Modelos.Facturacion;

public partial class UsoCfdiSat
{
    public int Id { get; set; }

    public string Clave { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int TipoPersona { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
