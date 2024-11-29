namespace ERP_TECKIO.Modelos;

public partial class DiasConsiderados
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Valor { get; set; }

    public string ArticulosLey { get; set; } = null!;

    public bool EsLaborableOpagado { get; set; }

    public int IdFactorSalarioIntegrado { get; set; }

    public virtual FactorSalarioIntegrado IdFactorSalarioIntegradoNavigation { get; set; } = null!;
}
