using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturaion;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class InsumoXproduccion : InsumoXproduccionAbstract
{
    

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Produccion IdProduccionNavigation { get; set; } = null!;

}

public abstract class InsumoXproduccionAbstract
{
    public int Id { get; set; }

    public int IdProduccion { get; set; }

    public int IdInsumo { get; set; }

    public decimal Cantidad { get; set; }
}
