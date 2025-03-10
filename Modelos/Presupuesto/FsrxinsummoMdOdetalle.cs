namespace ERP_TECKIO.Modelos;

public partial class FsrxinsummoMdOdetalle
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string ArticulosLey { get; set; } = null!;

    public decimal PorcentajeFsr { get; set; }

    public int IdFsrxinsummoMdO { get; set; }

    public virtual FsrxinsummoMdO IdFsrxinsummoMdONavigation { get; set; } = null!;
}
