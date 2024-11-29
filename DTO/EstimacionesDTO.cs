using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class EstimacionesDTO
    {
        public int Id { get; set; }
        public int IdPrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadConFormato { get; set; }
        public int IdPadre { get; set; }
        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public string CostoUnitarioConFormato { get; set; }
        public decimal Importe { get; set; }
        public string ImporteConFormato { get; set; }
        public int IdProyecto { get; set; }
        public decimal ImporteDeAvance { get; set; }
        public string ImporteDeAvanceConFormato { get; set; }
        public decimal PorcentajeAvance { get; set; }
        public string PorcentajeAvanceConFormato { get; set; }
        public bool PorcentajeAvanceEditando { get; set; }
        public decimal CantidadAvance { get; set; }
        public string CantidadAvanceConFormato { get; set; }
        public bool CantidadAvanceEditando { get; set; }
        public decimal ImporteDeAvanceAcumulado { get; set; }
        public string ImporteDeAvanceAcumuladoConFormato { get; set; }
        public decimal PorcentajeAvanceAcumulado { get; set; }
        public string PorcentajeAvanceAcumuladoConFormato { get; set; }
        public decimal CantidadAvanceAcumulado { get; set; }
        public string CantidadAvanceAcumuladoConFormato { get; set; }
        public decimal PorcentajeTotal { get; set; }
        public string PorcentajeTotalConFormato { get; set; }
        public decimal ImporteTotal { get; set; }
        public string ImporteTotalConFormato { get; set; }
        public decimal CantidadAvanceTotal { get; set; }
        public string CantidadAvanceTotalConFormato { get; set; }
        public int IdPeriodo { get; set; }
        public int TipoPrecioUnitario { get; set; }
        public bool Expandido { get; set; }
        public List<EstimacionesDTO> Hijos { get; set; }
    }
}
