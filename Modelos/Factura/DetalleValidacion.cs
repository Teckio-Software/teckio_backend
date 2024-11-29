
namespace ERP_TECKIO.Modelos;

public partial class DetalleValidacion
{
    public int Id { get; set; }

    public int IdAcuseValidacion { get; set; }

    public string CodigoValidacion { get; set; }

    public virtual AcuseValidacion IdAcuseValidacionNavigation { get; set; } = null!;

    //public virtual CatalogoValidacion IdCatalogoValiacionNavigation { get; set; } = null!;
}
