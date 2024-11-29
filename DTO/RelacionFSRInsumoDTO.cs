using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class RelacionFSRInsumoDTO
    {
        public int Id { get; set; }

        public int IdProyecto { get; set; }

        public decimal SueldoBase { get; set; }
        public decimal Prestaciones { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public int IdTipoInsumo { get; set; }
        public int IdInsumo { get; set; }
    }
}