
namespace ERP_TECKIO.Modelos;

public abstract class ArchivoAbstract
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ruta { get; set; } = null!;
}
/// <summary>
/// Esta es para la BD de datos operativa
/// </summary>
public partial class Archivo : ArchivoAbstract
{
    public virtual ICollection<Factura> FacturaIdArchivoNavigations { get; set; } = new List<Factura>();
    public virtual ICollection<Factura> FacturaIdArchivoPdfNavigations { get; set; } = new List<Factura>();
}

