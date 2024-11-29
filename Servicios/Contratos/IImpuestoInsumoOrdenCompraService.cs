using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;



namespace ERP_TECKIO
{
    public interface IImpuestoInsumoOrdenCompraService<T> where T : DbContext
    {
        Task<bool> CrearLista(List<ImpuestoInsumoOrdenCompraCreacionDTO> parametro);
        Task<List<ImpuestoInsumoOrdenCompraDTO>> ObtenerXIdInsumoXOrdenCompra(int IdInsumoXOrdenCompra);
    }
}
