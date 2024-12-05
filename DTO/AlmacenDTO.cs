
using ERP_TECKIO.Modelos;
namespace ERP_TECKIO
{
    
    public class AlmacenDTO : almacenAbsatract
    {
       
    }
    public class AlmacenCreacionDTO
    {
        public string? Codigo { get; set; }

        public string? AlmacenNombre { get; set; }

        public bool? Central { get; set; }

        public string? Responsable { get; set; }

        public string? Domicilio { get; set; }

        public string? Colonia { get; set; }

        public string? Ciudad { get; set; }

        public string? Telefono { get; set; }

        public int? IdProyecto { get; set; }
    }
}
