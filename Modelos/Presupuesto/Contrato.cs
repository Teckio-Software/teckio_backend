
namespace ERP_TECKIO.Modelos
{
    public partial class Contrato
    {
        public int Id { get; set; }

        public bool TipoContrato { get; set; }

        public int NumeroDestajo { get; set; }

        public int Estatus { get; set; }

        public int IdProyecto { get; set; }

        public decimal CostoDestajo { get; set; }

        public int IdContratista { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public decimal Anticipo { get; set; }
        public decimal Iva { get; set; }

        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        public virtual Contratista IdContratistaNavigation { get; set; } = null!;
        public virtual ICollection<DetalleXContrato> DetalleXcontratos { get; set; } = new List<DetalleXContrato>();
    }
}
