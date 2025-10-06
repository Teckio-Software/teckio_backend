using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class ContratoDTO
    {
        public int Id { get; set; }

        public bool TipoContrato { get; set; }

        public int NumeroDestajo { get; set; }

        public int Estatus { get; set; }

        public int IdProyecto { get; set; }

        public decimal CostoDestajo { get; set; }

        public int IdContratista { get; set; }

        public int IdPeriodoEstimacion { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string NumeroDestajoDescripcion { get; set; }
        public decimal Anticipo { get; set; }
        public string AnticipoConFormato { get; set; }
        public decimal Iva { get; set; }
        public string IvaConFormato { get; set; }
        public decimal ImporteTotalConFormato { get; set; }
    }

    public class ParametrosParaBuscarContratos {
        public int IdProyecto { get; set; }
        public bool TipoContrato { get; set; }
        public int IdContratista { get; set; }
        public int IdContrato { get; set; }
        public string? FechaInicio {  get; set; } 
        public string? FechaFin {  get; set; } 
    }
}
