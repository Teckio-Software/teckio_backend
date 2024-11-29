
namespace ERP_TECKIO
{
    public class SaldosBalanzaComprobacionDTO
    {
        public int Id { get; set; }

        public int IdCuentaContable { get; set; }
        public string? CuentaContableCodigo { get; set; }
        public string? CuentaContableDescripcion { get; set; }

        public int Mes { get; set; }

        public int Anio { get; set; }
        public DateOnly FechaFiltrado { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal SaldoFinal { get; set; }

        public decimal Debe { get; set; }

        public decimal Haber { get; set; }

        public bool EsUltimo { get; set; }
        public bool Expandido { get; set; }
        public SaldosBalanzaComprobacionDTO hijos { get; set; }
    }

    /// <summary>
    /// DTO para mostrar una tabla que se compone de 2 o más tablas
    /// y de esta forma generar la vista para el usuario
    /// </summary>
    public class VistaBalanzaComprobacionDTO
    {
        public int IdCuentaContable { get; set; }
        public string? CuentaContableCodigo { get; set; }
        public string? CuentaContableDescripcion { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public int Nivel { get; set; }
        public int IdPadre { get; set; }
        public bool Expandido { get; set; }
        public List<VistaBalanzaComprobacionDTO> hijos { get; set; }
    }

    public class SaldosBalanzaComproblacionXRangoFechaDTO
    {
        public int MesInicio { get; set; }
        public int AnioInicio { get; set; }
        public int MesTermino { get; set; }
        public int AnioTermino { get; set; }
    }

    public class SaldosBalanzaComproblacionXPeriodoDTO
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
    }
}
