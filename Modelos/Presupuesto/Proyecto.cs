
using ERP_TECKIO.Modelos;


namespace ERP_TECKIO.Modelos;

public partial class Proyecto
{
    public int Id { get; set; }

    public string? CodigoProyecto { get; set; }

    public string? Nombre { get; set; }

    public int? NoSerie { get; set; }

    public string? Moneda { get; set; }

    public decimal? PresupuestoSinIva { get; set; }

    public decimal? TipoCambio { get; set; }

    public decimal? PresupuestoSinIvaMonedaNacional { get; set; }

    public decimal? PorcentajeIva { get; set; }

    public decimal? PresupuestoConIvaMonedaNacional { get; set; }

    public decimal? Anticipo { get; set; }

    public int? CodigoPostal { get; set; }

    public string Domicilio { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinal { get; set; }

    public int? TipoProgramaActividad { get; set; }

    public int? InicioSemana { get; set; }

    public bool? EsSabado { get; set; }

    public bool? EsDomingo { get; set; }

    public int? IdPadre { get; set; }

    public int? Nivel { get; set; }

    public virtual ICollection<AlmacenEntradaInsumo> AlmacenEntradaInsumos { get; set; } = new List<AlmacenEntradaInsumo>();
    public virtual ICollection<AlmacenSalidaInsumo> AlmacenSalidaInsumos { get; set; } = new List<AlmacenSalidaInsumo>();
    public virtual ICollection<Almacen> Almacens { get; set; } = new List<Almacen>();
    public virtual ICollection<CompraDirecta> CompraDirecta { get; set; } = new List<CompraDirecta>();
    public virtual ICollection<Concepto> Conceptos { get; set; } = new List<Concepto>();
    public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();
    public virtual ICollection<Estimaciones> Estimaciones { get; set; } = new List<Estimaciones>();
    public virtual ICollection<FactorSalarioIntegrado> FactorSalarioIntegrados { get; set; } = new List<FactorSalarioIntegrado>();
    public virtual ICollection<FactorSalarioReal> FactorSalarioReals { get; set; } = new List<FactorSalarioReal>();
    public virtual ICollection<InsumoExistencia> InsumoExistencia { get; set; } = new List<InsumoExistencia>();
    public virtual ICollection<Insumo> Insumos { get; set; } = new List<Insumo>();
    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
    public virtual ICollection<PeriodoEstimaciones> PeriodoEstimaciones { get; set; } = new List<PeriodoEstimaciones>();
    public virtual ICollection<PrecioUnitario> PrecioUnitarios { get; set; } = new List<PrecioUnitario>();
    public virtual ICollection<ProgramacionEstimada> ProgramacionEstimada { get; set; } = new List<ProgramacionEstimada>();
    public virtual ICollection<RelacionFSRInsumo> RelacionFsrinsumos { get; set; } = new List<RelacionFSRInsumo>();
    public virtual ICollection<Requisicion> Requisicions { get; set; } = new List<Requisicion>();
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    public virtual ICollection<ConjuntoIndirectos> ConjuntoIndirectos { get; set; } = new List<ConjuntoIndirectos>();
    public virtual ICollection<ProgramacionEstimadaGantt> ProgramacionEstimadaGantts { get; set; } = new List<ProgramacionEstimadaGantt>();
    public virtual ICollection<PrecioUnitarioXEmpleado> PrecioUnitarioXEmpleados { get; set; } = new List<PrecioUnitarioXEmpleado>();
    public virtual ICollection<DependenciaProgramacionEstimada> DependenciaProgramacionEstimadas { get; set; } = new List<DependenciaProgramacionEstimada>();
}
