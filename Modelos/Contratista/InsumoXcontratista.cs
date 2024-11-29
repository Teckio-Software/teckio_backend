
namespace ERP_TECKIO.Modelos;

public partial class InsumoXcontratista
{
    public int Id { get; set; }

    public int IdInsumo { get; set; }

    public int IdContratista { get; set; }

    public decimal Costo { get; set; }

    public virtual Contratista IdContratistaNavigation { get; set; } = null!;

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;
}
