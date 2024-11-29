
namespace ERP_TECKIO.Modelos;

public partial class NaturalezaRubro
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Rubro> Rubros { get; set; } = new List<Rubro>();
}
