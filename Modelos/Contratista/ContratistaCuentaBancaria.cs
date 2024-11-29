
namespace ERP_TECKIO.Modelos;

public partial class ContratistaCuentaBancaria
{
    public int Id { get; set; }

    public int? IdBanco { get; set; }

    public string? NumeroSucursal { get; set; }

    public string? NumeroCuenta { get; set; }

    public string? Clabe { get; set; }

    public bool? TopeMinimo { get; set; }

    public DateTime? FechaApertura { get; set; }

    public string? TipoMoneda { get; set; }

    public int? IdContratista { get; set; }

    public virtual Banco? IdBancoNavigation { get; set; }

    public virtual Contratista? IdContratistaNavigation { get; set; }

    public virtual ICollection<MovimientoBancario> MovimientoBancarios { get; set; } = new List<MovimientoBancario>();
}
