
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_TECKIO.Modelos;

public class CuentaContable : CuentaContableAbstract
{
    public int IdRubro { get; set; }
    public decimal SaldoInicial { get; set; }

    public decimal SaldoFinal { get; set; }

    public decimal Presupuesto { get; set; }

    public int IdCodigoAgrupadorSat { get; set; }
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdCuentaAnticiposNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdCuentaContableNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdIepsNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdIvaExentoNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdIvaGravableNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdIvaPorTrasladarNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdIvaRetenidoNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdIvaTrasladadoNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Clientes>? ClienteIdRetensionIsrNavigations { get; set; } = new List<Clientes>();
    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdCuentaAnticiposNavigations { get; set; } = new List<Contratista>();

    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdCuentaContableNavigations { get; set; } = new List<Contratista>();

    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdCuentaRetencionIsrNavigations { get; set; } = new List<Contratista>();

    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdCuentaRetencionIvaNavigations { get; set; } = new List<Contratista>();

    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdEgresosIvaExentoNavigations { get; set; } = new List<Contratista>();

    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdEgresosIvaGravableNavigations { get; set; } = new List<Contratista>();

    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdIvaAcreditableContableNavigations { get; set; } = new List<Contratista>();

    [NotMapped]
    public virtual ICollection<Contratista> ContratistaIdIvaAcreditableFiscalNavigations { get; set; } = new List<Contratista>();

    [NotMapped]

    public virtual ICollection<Contratista> ContratistaIdIvaPorAcreditarNavigations { get; set; } = new List<Contratista>();

    public virtual Rubro IdRubroNavigation { get; set; } = null!;
    [NotMapped]
    public virtual ICollection<PolizaDetalle> PolizaDetalles { get; set; } = new List<PolizaDetalle>();
    [NotMapped]
    public virtual ICollection<SaldosBalanzaComprobacion> SaldosBalanzaComprobacions { get; set; } = new List<SaldosBalanzaComprobacion>();

}

public abstract class CuentaContableAbstract
{
    public int Id { get; set; }

    public string Codigo { get; set; }

    public string Descripcion { get; set; }

    public int TipoMoneda { get; set; }

    public int IdPadre { get; set; }
    public int Nivel { get; set; }
    public bool ExisteMovimiento { get; set; }
    public bool ExistePoliza { get; set; }
}
