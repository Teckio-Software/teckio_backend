using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos;


public partial class Produccion : ProduccionAbstract
{
    public virtual ProductoYservicio IdProductoYservicioNavigation { get; set; } = null!;

    public virtual ICollection<InsumoXproduccion> InsumoxProduccion { get; set; } = new List<InsumoXproduccion>();

}

public abstract class ProduccionAbstract
{
    public int Id { get; set; }

    public int IdProductoYservicio { get; set; }
        
    public DateTime FechaProduccion { get; set; }

    public string Produjo { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public string? Observaciones { get; set; }

    public int Estatus { get; set; }

    public string? Autorizo { get; set; }
}
