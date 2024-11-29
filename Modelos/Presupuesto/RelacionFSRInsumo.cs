namespace ERP_TECKIO.Modelos;


public partial class RelacionFSRInsumo
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public decimal SueldoBase { get; set; }

    public decimal Prestaciones { get; set; }

    public int IdInsumo { get; set; }

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
