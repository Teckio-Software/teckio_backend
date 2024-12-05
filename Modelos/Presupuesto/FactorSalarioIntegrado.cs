namespace ERP_TECKIO.Modelos;

public partial class FactorSalarioIntegrado
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public decimal Fsi { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual ICollection<DiasConsiderados> DiasConsiderados { get; set; } = new List<DiasConsiderados>();
}
