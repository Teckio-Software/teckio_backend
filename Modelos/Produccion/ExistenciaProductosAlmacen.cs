using ERP_TECKIO.Modelos.Facturacion;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class ExistenciaProductosAlmacen : ExistenciaProductosAlmacenAbstract
{
    public virtual ProductoYservicio IdProductoYservicioNavigation { get; set; } = null!;
}

public abstract class ExistenciaProductosAlmacenAbstract
{
    public int Id { get; set; }

    public int IdProductoYservicio { get; set; }

    public decimal Cantidad { get; set; }
}
