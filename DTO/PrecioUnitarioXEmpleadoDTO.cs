using ERP_TECKIO.Modelos;

namespace ERP_TECKIO.DTO
{
    public class PrecioUnitarioXEmpleadoDTO : PrecioUnitarioXEmpleadoAbstract
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadConFormato { get; set; }
        public bool Seleccionado { get; set; }
    }
}
