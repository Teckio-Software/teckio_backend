
namespace ERP_TECKIO.Modelos;

public partial class CodigoAgrupadorSat
{
    public int Id { get; set; }

    public int? Nivel { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
}
