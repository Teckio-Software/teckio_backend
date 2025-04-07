
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class ProgramacionEstimadaGanttDTO
    {
        public string Id { get; set; }
        public int Numerador { get; set; }
        public string Name { get; set; }
        public int IdProyecto { get; set; }
        public bool EsSabado { get; set; }
        public bool EsDomingo { get; set; }
        public int IdPrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public int TipoPrecioUnitario { get; set; }
        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Importe { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Duracion { get; set; }
        public int Progress { get; set; }
        public int Comando { get; set; }
        public int DesfaseComando { get; set; }
        public string? Parent { get; set; }
        public string Type { get; set;  }
        public int Predecesor { get; set; }
        public List<DependenciaProgramacionEstimadaDTO> Dependencies { get; set; }
        public string CadenaDependencias { get; set; }
    }

    public class ProgramacionEstimadaGanttDeserealizadaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdProyecto { get; set; }
        public bool EsSabado { get; set; }
        public bool EsDomingo { get; set; }
        public int IdPrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public int TipoPrecioUnitario { get; set; }
        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int Duracion { get; set; }
        public int Progress { get; set; }
        public int Comando { get; set; }
        public int DesfaseComando { get; set; }
        public int? IdPadre { get; set; }
        public string Type { get; set;  }
    }

    public class ImporteSemanalDTO
    {
        public int NumeroSemana { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Anio { get; set; }
        public decimal Total { get; set; }
        public string TotalConFormato { get; set; }
    }

    public class ImporteSemanalPorTipoDTO: ImporteSemanalDTO
    {
        public int TipoDeInsumo { get; set; }
    }

    public class ImportesSemanalesPorTipoDTO
    {
        public List<ImporteSemanalDTO> semanas { get; set; } = new List<ImporteSemanalDTO>();
        public List<ImporteSemanalDTO> semanasMDO { get; set; } = new List<ImporteSemanalDTO>();
        public List<ImporteSemanalDTO> semanasMaterial { get; set; } = new List<ImporteSemanalDTO>();
        public List<ImporteSemanalDTO> semanasEquipo { get; set; } = new List<ImporteSemanalDTO>();
    }
}
