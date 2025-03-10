namespace ERP_TECKIO.Modelos;

public partial class FsixinsummoMdO
{
    public int Id { get; set; }

    public decimal DiasNoLaborales { get; set; }

    public decimal DiasPagados { get; set; }

    public decimal Fsi { get; set; }

    public int IdInsumo { get; set; }

    public int IdProyecto { get; set; }

    public virtual ICollection<FsixinsummoMdOdetalle> FsixinsummoMdOdetalles { get; set; } = new List<FsixinsummoMdOdetalle>();

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
