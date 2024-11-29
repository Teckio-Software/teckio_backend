
namespace ERP_TECKIO.Modelos;

public partial class TipoInsumo
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Insumo> Insumos { get; set; } = new List<Insumo>();
}
