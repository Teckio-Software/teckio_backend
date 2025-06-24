using ERP_TECKIO.Modelos;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class ImpuestoDetalleOrdenVentum : ImpuestoDetalleOrdenVentaAbstract
{
    public virtual CategoriaImpuesto IdCategoriaImpuestoNavigation { get; set; } = null!;

    public virtual ClasificacionImpuesto IdClasificacionImpuestoNavigation { get; set; } = null!;

    public virtual DetalleOrdenVentum IdDetalleOrdenVentaNavigation { get; set; } = null!;

    public virtual TipoFactor IdTipoFactorNavigation { get; set; } = null!;

    public virtual TipoImpuesto IdTipoImpuestoNavigation { get; set; } = null!;
}

public abstract class ImpuestoDetalleOrdenVentaAbstract {
    public int Id { get; set; }

    public int IdDetalleOrdenVenta { get; set; }

    public int IdTipoImpuesto { get; set; }

    public int IdTipoFactor { get; set; }

    public int IdCategoriaImpuesto { get; set; }

    public int IdClasificacionImpuesto { get; set; }

    public decimal TasaCuota { get; set; }

    public decimal ImporteTotal { get; set; }
}
