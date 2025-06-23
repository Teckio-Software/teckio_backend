

namespace ERP_TECKIO.Modelos;

public partial class Almacen : almacenAbsatract
{
    

    public virtual ICollection<AlmacenEntrada> AlmacenEntrada { get; set; } = new List<AlmacenEntrada>();

    public virtual ICollection<AlmacenSalida> AlmacenSalida { get; set; } = new List<AlmacenSalida>();

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<InsumoExistencia> InsumoExistencia { get; set; } = new List<InsumoExistencia>();
    public virtual ICollection<EntradaProduccionAlmacen> EntradaProduccionAlmacens { get; set; } = new List<EntradaProduccionAlmacen>();


}

public abstract class almacenAbsatract
{
    public int Id { get; set; }

    public string? Codigo { get; set; }

    public string? AlmacenNombre { get; set; }

    public bool? Central { get; set; }

    public string? Responsable { get; set; }

    public string? Domicilio { get; set; }

    public string? Colonia { get; set; }

    public string? Ciudad { get; set; }

    public string? Telefono { get; set; }

    public int? IdProyecto { get; set; }
}
