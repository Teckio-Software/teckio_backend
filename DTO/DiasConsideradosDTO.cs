using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class DiasConsideradosDTO
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public decimal Valor { get; set; }

        public string ArticulosLey { get; set; }

        public bool EsLaborableOPagado { get; set; }

        public int IdFactorSalarioIntegrado { get; set; }
    }
}
