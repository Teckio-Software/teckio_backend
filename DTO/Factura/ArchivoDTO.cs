using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO.DTO.Factura
{
    public class ArchivoDTO
    {
        public long Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Ruta { get; set; } = null!;
    }
}
