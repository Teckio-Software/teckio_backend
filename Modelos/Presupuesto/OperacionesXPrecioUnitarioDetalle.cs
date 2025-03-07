namespace ERP_TECKIO.Modelos.Presupuesto
{
    public class OperacionesXPrecioUnitarioDetalleAbstract
    {
        public int Id { get; set; }
        public int IdPrecioUnitarioDetalle { get; set; }
        public string Operacion { get; set; }
        public decimal Resultado { get; set; }
        public string Descripcion { get; set; }

    }

    public class OperacionesXPrecioUnitarioDetalle: OperacionesXPrecioUnitarioDetalleAbstract
    {
        public virtual PrecioUnitarioDetalle IdPrecioUnitarioDetalleNavigation { get; set; }
    }
}
