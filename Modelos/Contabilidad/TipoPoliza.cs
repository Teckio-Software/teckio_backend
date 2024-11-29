
namespace ERP_TECKIO.Modelos;

public abstract class TipoPolizaAbstract
{
    public int Id { get; set; }
    public string? Descripcion { get; set; }
}
public partial class TipoPoliza : TipoPolizaAbstract
{
    

    public string? Codigo { get; set; }

   
    public int Naturaleza { get; set; }
    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
public class TipoPolizaProveedores : TipoPolizaAbstract
{
    public virtual ICollection<PolizaProveedores> Polizas { get; set; } = new List<PolizaProveedores>();

}
