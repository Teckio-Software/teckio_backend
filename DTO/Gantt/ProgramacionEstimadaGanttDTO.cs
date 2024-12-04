
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class ProgramacionEstimadaGanttDTO
    {
        public string Id { get; set; }

        public int IdProyecto { get; set; }

        public int IdPrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public bool TipoPrecioUnitario { get; set; }

        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Duracion { get; set; }

        public int Progress { get; set; }

        public int Comando { get; set; }

        public int DesfaseComando { get; set; }

        public string? Parent { get; set; }
    }
}
