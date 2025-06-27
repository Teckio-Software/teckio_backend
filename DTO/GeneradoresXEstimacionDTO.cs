using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class GeneradoresXEstimacionDTO
    {
        public int Id { get; set; }

        public int IdEstimacion { get; set; }
        public int? IdGenerador { get; set; }

        public string Codigo { get; set; } = null!;

        public string EjeX { get; set; } = null!;

        public string EjeY { get; set; } = null!;

        public string EjeZ { get; set; } = null!;

        public decimal Cantidad { get; set; }
        public decimal CantidadReferencia { get; set; }

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }
        public decimal CantidadTotal { get; set; }
        public string CantidadOperacion { get; set; } = "";
    }
}
