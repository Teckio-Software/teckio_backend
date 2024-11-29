
namespace ERP_TECKIO.Modelos;

public partial class Especialidad
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public virtual ICollection<Concepto> Conceptos { get; set; } = new List<Concepto>();
}