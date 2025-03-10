namespace ERP_TECKIO.Modelos;

public partial class FsrxinsummoMdO
{
    public int Id { get; set; }

    public decimal CostoDirecto { get; set; }

    public decimal CostoFinal { get; set; }

    public decimal? Fsr { get; set; }

    public int IdInsumo { get; set; }

    public int IdProyecto { get; set; }

    public virtual ICollection<FsrxinsummoMdOdetalle> FsrxinsummoMdOdetalles { get; set; } = new List<FsrxinsummoMdOdetalle>();

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
