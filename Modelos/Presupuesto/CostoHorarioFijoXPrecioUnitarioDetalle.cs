namespace ERP_TECKIO.Modelos.Presupuesto
{
    public partial class CostoHorarioFijoXPrecioUnitarioDetalleAbstract
    {
        public int Id { get; set; }

        public int IdPrecioUnitarioDetalle { get; set; }

        public decimal Inversion { get; set; }

        public decimal InteresAnual { get; set; }

        public decimal HorasUso { get; set; }

        public decimal VidaUtil { get; set; }

        public decimal PorcentajeReparacion { get; set; }

        public decimal PorcentajeSeguroAnual { get; set; }

        public decimal GastoAnual { get; set; }

        public decimal MesTrabajoReal { get; set; }
        public decimal InteresSobreCapital { get; set; }
        public decimal Depreciacion{ get; set; }
        public decimal Reparaciones { get; set; }
        public decimal Seguro { get; set; }
        public decimal GastosAnuales { get; set; }
        public decimal Suma { get; set; }
        public decimal SubtotalGastosFijos { get; set; }
    }

    public partial class CostoHorarioFijoXPrecioUnitarioDetalle: CostoHorarioFijoXPrecioUnitarioDetalleAbstract
    {
        public virtual PrecioUnitarioDetalle IdPrecioUnitarioDetalleNavigation { get; set; } = null!;
    }

}
