namespace ERP_TECKIO.Modelos
{
    public class PrecioUnitarioXEmpleado : PrecioUnitarioXEmpleadoAbstract
    {
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; }
        public virtual Proyecto IdProyectoNavigation { get; set; }
    }

    public class PrecioUnitarioXEmpleadoAbstract
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public int IdProyceto { get; set; }
        public int IdPrecioUnitario { get; set; }
    }
}
