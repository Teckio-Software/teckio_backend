
namespace ERP_TECKIO.Modelos;

public partial class GeneradoresXEstimacion
{
    public int Id { get; set; }

    public int IdEstimacion { get; set; }
    public int? IdGenerador { get; set; }

    public string Codigo { get; set; } = null!;

    public string EjeX { get; set; } = null!;

    public string EjeY { get; set; } = null!;

    public string EjeZ { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public decimal X { get; set; }

    public decimal Y { get; set; }

    public decimal Z { get; set; }

    public virtual Estimaciones IdEstimacionesNavigation { get; set; } = null!;
    public virtual Generadores IdGeneradoresNavigation { get; set; } = null!;
}
