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
        public string? CantidadDecimal { get; set; }


        public decimal X { get; set; }
        public string? XDecimal { get; set; }

        public decimal Y { get; set; }
        public string? YDecimal { get; set; }

        public decimal Z { get; set; }
        public string? ZDecimal { get; set; }
        public decimal CantidadTotal { get; set; }
        public string? TotalDecimal { get; set; }
        public string CantidadOperacion { get; set; }
    }
}
