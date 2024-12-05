using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class FactorSalarioRealDetalleDTO
    {
        public int Id { get; set; }

        public int IdFactorSalarioReal { get; set; }

        public string? Codigo { get; set; }

        public string? Descripcion { get; set; }

        public decimal PorcentajeFsrdetalle { get; set; }

        public string? ArticulosLey { get; set; }
    }
}
