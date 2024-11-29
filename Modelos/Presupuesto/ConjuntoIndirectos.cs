
namespace ERP_TECKIO.Modelos;
    public partial class ConjuntoIndirectos
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public int TipoCalculo { get; set; }
        public decimal Porcentaje { get; set; }
    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
    public virtual ICollection<Indirectos> Indirectos { get; set; } = new List<Indirectos>();

}
