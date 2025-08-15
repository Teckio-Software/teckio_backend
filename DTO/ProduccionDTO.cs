using ERP_TECKIO.Modelos;

namespace ERP_TECKIO.DTO
{
    public class ProduccionDTO : ProduccionAbstract
    {
    }

    public class ProduccionConAlmacenDTO: ProduccionAbstract
    {
        public int IdAlmacen { get; set; }
    }
}
