using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class IndirectosAbstractDTO
    {
        public int Id { get; set; }
        public int IdConjuntoIndirectos { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int TipoIndirecto { get; set; }
        public decimal Porcentaje { get; set; }
        public string PorcentajeConFormato { get; set; }
        public int IdIndirectoBase { get; set; }
        public bool Expandido { get; set; } = false;
    }

    public class IndirectosDTO : IndirectosAbstractDTO
    {
        public List<IndirectosDTO>? Hijos {  get; set; }
    }


    public class IndirectosXConceptoAbstractDTO
    {
        public int Id { get; set; }
        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int TipoIndirecto { get; set; }
        public decimal Porcentaje { get; set; }
        public string PorcentajeConFormato { get; set; }
        public int IdIndirectoBase { get; set; }
        public bool Expandido { get; set; } = false;
    }

    public class IndirectosXConceptoDTO : IndirectosXConceptoAbstractDTO
    {
        public List<IndirectosXConceptoDTO>? Hijos { get; set; }
    }
}
