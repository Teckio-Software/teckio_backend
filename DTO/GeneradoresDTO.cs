using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class GeneradoresDTO
    {
        public int Id { get; set; }

        public int IdPrecioUnitario { get; set; }

        public string Codigo { get; set; } = null!;

        public string EjeX { get; set; } = null!;

        public string EjeY { get; set; } = null!;

        public string EjeZ { get; set; } = null!;

        public decimal Cantidad { get; set; }

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }
        public decimal CantidadTotal { get; set; }
    }
}
