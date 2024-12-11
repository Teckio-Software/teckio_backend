namespace ERP_TECKIO.Modelos
{
    public class Empleado : EmpleadoAbstract
    {
        public virtual ICollection<PrecioUnitarioXEmpleado> PrecioUnitarioXEmpleados { get; set; } = new List<PrecioUnitarioXEmpleado>();
    }

    public class EmpleadoAbstract
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Curp { get; set; }
        public string? Rfc { get; set; }
        public string SeguroSocial { get; set; }
        public DateTime? FechaRelacionLaboral { get; set; }
        public DateTime? FechaTerminoRelacionLaboral { get; set; }
        public decimal SalarioDiario { get; set; }
        public bool Estatus { get; set; }
    }
}
