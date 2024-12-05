using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class ConjuntoIndirectosDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public int TipoCalculo { get; set; }
        public decimal Porcentaje { get; set; }
        public string PorcentajeConFormato { get; set; }
    }
}
