using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturacion;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class ProductosXentradaProduccionAlmacen
{
    public int Id { get; set; }

    public int IdEntradaProduccionAlmacen { get; set; }

    public int IdProductoYservicio { get; set; }

    public decimal Cantidad { get; set; }

    public int TipoOrigen { get; set; }

    public virtual EntradaProduccionAlmacen IdEntradaProduccionAlmacenNavigation { get; set; } = null!;

    public virtual ProductoYservicio IdProductoYservicioNavigation { get; set; } = null!;
}
