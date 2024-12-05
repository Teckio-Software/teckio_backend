

namespace ERP_TECKIO.Modelos
{
    public partial class Indirectos
    {
        public int Id { get; set; }
        public int IdConjuntoIndirectos { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int TipoIndirecto { get; set; }
        public decimal Porcentaje { get; set; }
        public int IdIndirectoBase { get; set; }
        public virtual ConjuntoIndirectos IdConjuntoIndirectosNavigation { get; set; }

    }

    public partial class IndirectosXConcepto
    {
        public int Id { get; set; }
        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int TipoIndirecto { get; set; }
        public decimal Porcentaje { get; set; }
        public int IdIndirectoBase { get; set; }
        public virtual Concepto IdConceptoNavigation { get; set; }

    }
}
