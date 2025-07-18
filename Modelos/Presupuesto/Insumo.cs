
using ERP_TECKIO.Modelos.Presupuesto;

namespace ERP_TECKIO.Modelos;

public partial class Insumo
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Unidad { get; set; }

    public int? IdTipoInsumo { get; set; }

    public int? IdFamiliaInsumo { get; set; }

    public decimal? CostoUnitario { get; set; }
    public decimal CostoBase { get; set; }
    public bool EsFsrGlobal { get; set; }
    public int? IdProyecto { get; set; }

    public virtual ICollection<AlmacenEntradaInsumo> AlmacenEntradaInsumos { get; set; } = new List<AlmacenEntradaInsumo>();

    public virtual ICollection<AlmacenSalidaInsumo> AlmacenSalidaInsumos { get; set; } = new List<AlmacenSalidaInsumo>();

    public virtual FamiliaInsumo? IdFamiliaInsumoNavigation { get; set; }
    public virtual ICollection<FsixinsummoMdO> FsixinsummoMdOs { get; set; } = new List<FsixinsummoMdO>();

    public virtual ICollection<FsrxinsummoMdO> FsrxinsummoMdOs { get; set; } = new List<FsrxinsummoMdO>();

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual TipoInsumo? IdTipoInsumoNavigation { get; set; }

    public virtual ICollection<InsumoExistencia> InsumoExistencia { get; set; } = new List<InsumoExistencia>();

    public virtual ICollection<InsumoXCompraDirecta> InsumoXcompraDirecta { get; set; } = new List<InsumoXCompraDirecta>();

    public virtual ICollection<InsumoXCotizacion> InsumoXcotizacions { get; set; } = new List<InsumoXCotizacion>();

    public virtual ICollection<InsumoXOrdenCompra> InsumoXordenCompras { get; set; } = new List<InsumoXOrdenCompra>();

    public virtual ICollection<InsumoXRequisicion> InsumoXrequisicions { get; set; } = new List<InsumoXRequisicion>();

    public virtual ICollection<PrecioUnitarioDetalle> PrecioUnitarioDetalles { get; set; } = new List<PrecioUnitarioDetalle>();

    public virtual ICollection<RelacionFSRInsumo> RelacionFsrinsumos { get; set; } = new List<RelacionFSRInsumo>();
    public virtual ICollection<InsumoXproduccion> InsumoxProduccion { get; set; } = new List<InsumoXproduccion>(); 
    public virtual ICollection<CostoHorarioVariableXPrecioUnitarioDetalle> CostoHorarioVariableXprecioUnitarioDetalles { get; set; } = new List<CostoHorarioVariableXPrecioUnitarioDetalle>();
}
