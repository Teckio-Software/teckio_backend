using ERP_TECKIO.Modelos;
namespace ERP_TECKIO 
{
    public class FacturaBaseDTO : FacturaAbstract { }

    public class FacturaTeckioDTO : FacturaBaseDTO
    {
        public int EstatusValido { get; set; }
        public int EstatusCRP { get; set; }
    }
}
