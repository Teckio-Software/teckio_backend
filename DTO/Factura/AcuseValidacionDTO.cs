
namespace ERP_TECKIO
{
    public class AcuseValidacionDTO
    {
        public int Id { get; set; }

        public int IdFactura { get; set; }

        public int IdUsuario { get; set; }

        public string Folio { get; set; } = null!;

        public bool Estatus { get; set; }

        public DateTime Fecha { get; set; }
    }
}
