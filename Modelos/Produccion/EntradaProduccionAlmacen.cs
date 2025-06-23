
namespace ERP_TECKIO.Modelos;

public partial class EntradaProduccionAlmacen
{
    public int Id { get; set; }

    public int IdAlmacen { get; set; }

    public DateTime FechaEntrada { get; set; }

    public string Recibio { get; set; } = null!;

    public string? Observaciones { get; set; }

    public virtual Almacen IdAlmacenNavigation { get; set; } = null!;

    public virtual ICollection<ProductosXentradaProduccionAlmacen> ProductosXentradaProduccionAlmacens { get; set; } = new List<ProductosXentradaProduccionAlmacen>();
}
