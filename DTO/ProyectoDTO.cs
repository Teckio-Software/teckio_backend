using System.Data;

namespace ERP_TECKIO
{
    /// <summary>
    /// Clase base en la que se relacionan los 
    /// </summary>
    public class ProyectoDTO
    {
        public int Id { get; set; }

        public string? CodigoProyecto { get; set; }

        public string? Nombre { get; set; }

        public int NoSerie { get; set; }

        public string? Moneda { get; set; }

        public decimal PresupuestoSinIva { get; set; }

        public decimal TipoCambio { get; set; }

        public decimal PresupuestoSinIvaMonedaNacional { get; set; }

        public decimal PorcentajeIva { get; set; }

        public decimal PresupuestoConIvaMonedaNacional { get; set; }

        public decimal Anticipo { get; set; }

        public int CodigoPostal { get; set; }

        public string Domicilio { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public int TipoProgramaActividad { get; set; }

        public int InicioSemana { get; set; }

        public bool EsSabado { get; set; }

        public bool EsDomingo { get; set; }

        public int IdPadre { get; set; }

        public int Nivel { get; set; }
        public bool Expandido { get; set; }
        public List<ProyectoDTO> Hijos { get; set; } = new List<ProyectoDTO>();
    }
}