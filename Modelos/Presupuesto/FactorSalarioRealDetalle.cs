namespace ERP_TECKIO.Modelos;

public partial class FactorSalarioRealDetalle
{
    public int Id { get; set; }

    public int IdFactorSalarioReal { get; set; }

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public decimal PorcentajeFsrdetalle { get; set; }

    public string? ArticulosLey { get; set; }

    public virtual FactorSalarioReal IdFactorSalarioRealNavigation { get; set; } = null!;
}
