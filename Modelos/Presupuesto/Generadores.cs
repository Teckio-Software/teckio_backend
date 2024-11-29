
namespace ERP_TECKIO.Modelos;

public partial class Generadores
{
    public int Id { get; set; }

    public int IdPrecioUnitario { get; set; }

    public string Codigo { get; set; } = null!;

    public string EjeX { get; set; } = null!;

    public string EjeY { get; set; } = null!;

    public string EjeZ { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public decimal X { get; set; }

    public decimal Y { get; set; }

    public decimal Z { get; set; }

    public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; } = null!;
}
