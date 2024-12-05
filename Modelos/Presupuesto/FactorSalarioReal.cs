namespace ERP_TECKIO.Modelos;

public partial class FactorSalarioReal
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public decimal PorcentajeFsr { get; set; }

    public virtual ICollection<FactorSalarioRealDetalle> FactorSalarioRealDetalles { get; set; } = new List<FactorSalarioRealDetalle>();

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
