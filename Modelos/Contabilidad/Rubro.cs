
namespace ERP_TECKIO.Modelos;

public partial class Rubro
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int NaturalezaRubro { get; set; }

    public int TipoReporte { get; set; }

    public int Posicion { get; set; }

    public virtual ICollection<CuentaContable> CuentaContables { get; set; } = new List<CuentaContable>();
}
