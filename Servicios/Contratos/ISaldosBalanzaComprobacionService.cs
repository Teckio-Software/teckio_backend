using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface ISaldosBalanzaComprobacionService<T> where T : DbContext
    {
        Task<List<SaldosBalanzaComprobacionDTO>> ObtenTodosXRangoFecha(SaldosBalanzaComproblacionXRangoFechaDTO rangoFechas);
        Task<List<SaldosBalanzaComprobacionDTO>> ObtenTodosXPeriodo(SaldosBalanzaComproblacionXPeriodoDTO periodo);
        Task<List<VistaBalanzaComprobacionDTO>> CrearVistaXPeriodo(List<CuentaContableDTO> cuentas, List<SaldosBalanzaComprobacionDTO> saldos, SaldosBalanzaComproblacionXPeriodoDTO periodo); //metodo para obtener la vista a mostrar
        Task<List<VistaBalanzaComprobacionDTO>> CrearVistaXRangoFecha(List<CuentaContableDTO> cuentas, List<SaldosBalanzaComprobacionDTO> saldos, SaldosBalanzaComproblacionXRangoFechaDTO rangoFechas); //metodo para obtener la vista a mostrar
        Task<List<VistaBalanzaComprobacionDTO>> EstructurarRegistros(List<VistaBalanzaComprobacionDTO> balanzaDeComprobacionSinOrdenar); //metodo para obtener la vista a mostrar
        Task<List<SaldosBalanzaComprobacionDTO>> ObtenTodos();
        Task<SaldosBalanzaComprobacionDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(SaldosBalanzaComprobacionDTO modelo);
        Task<RespuestaDTO> Editar(SaldosBalanzaComprobacionDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
