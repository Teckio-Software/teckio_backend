namespace ERP_TECKIO.Modelos.Presupuesto
{
    public class CostoHorarioVariableXPrecioUnitarioDetalleAbstract
    {
        public int Id { get; set; }

        public int IdPrecioUnitarioDetalle { get; set; }

        public int TipoCostoVariable { get; set; }

        public decimal Rendimiento { get; set; }
    }

    public class CostoHorarioVariableXPrecioUnitarioDetalle: CostoHorarioVariableXPrecioUnitarioDetalleAbstract
    {
        public virtual Insumo IdInsumoNavigation { get; set; } = null!;

        public virtual PrecioUnitarioDetalle IdPrecioUnitarioDetalleNavigation { get; set; } = null!;
    }

}
