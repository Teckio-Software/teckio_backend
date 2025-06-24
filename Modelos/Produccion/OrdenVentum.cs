using ERP_TECKIO.Modelos;
using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class OrdenVentum : OrdenVentaAbstract
{
    public virtual ICollection<DetalleOrdenVentum> DetalleOrdenVenta { get; set; } = new List<DetalleOrdenVentum>();

    public virtual Clientes IdClienteNavigation { get; set; } = null!;
}

public abstract class OrdenVentaAbstract
{
    public int Id { get; set; }

    public string NumeroOrdenVenta { get; set; } = null!;

    public string Autorizo { get; set; } = null!;

    public int IdCliente { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int Estatus { get; set; }

    public decimal ImporteTotal { get; set; }

    public decimal Subtotal { get; set; }

    public int EstatusSaldado { get; set; }

    public decimal TotalSaldado { get; set; }

    public string? Observaciones { get; set; }
}
