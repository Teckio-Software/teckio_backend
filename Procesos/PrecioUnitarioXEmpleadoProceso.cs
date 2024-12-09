using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class PrecioUnitarioXEmpleadoProceso<T> where T : DbContext
    {
        private readonly IPrecioUnitarioXEmpleadoService<T> _precioUnitarioXEmpleadoService;
        private readonly IPrecioUnitarioService<T> _precioUnitarioService;
        private readonly PrecioUnitarioProceso<T> _precioUnitarioProceso;
        public PrecioUnitarioXEmpleadoProceso(
            IPrecioUnitarioXEmpleadoService<T> precioUnitarioXEmpleadoService,
            IPrecioUnitarioService<T> precioUnitarioService,
            PrecioUnitarioProceso<T> precioUnitarioProceso
            ) {
            _precioUnitarioXEmpleadoService = precioUnitarioXEmpleadoService;
            _precioUnitarioService = precioUnitarioService;
            _precioUnitarioProceso = precioUnitarioProceso;
        }

        public async Task<List<PrecioUnitarioXEmpleadoDTO>> ObtenerXIdEmpleado(int IdEmpleado)
        {
            var PUXEmpleado = await _precioUnitarioXEmpleadoService.ObtenerXIdEmpleado(IdEmpleado);
            var AgrupadosXProyecto = PUXEmpleado.GroupBy(z => z.IdProyceto);
            foreach (var item in AgrupadosXProyecto)
            {
                var PUXProyecto = await _precioUnitarioProceso.ObtenerSinEstructura(item.Key);

                foreach (var item1 in PUXEmpleado.Where(z => z.IdProyceto == item.Key))
                {
                    var PU = PUXProyecto.Where(z => z.Id == item1.IdPrecioUnitario).First();
                    item1.Codigo = PU.Codigo;
                    item1.Descripcion = PU.Descripcion;
                    item1.Unidad = PU.Unidad;
                    item1.Cantidad = PU.Cantidad;
                    item1.CantidadConFormato = String.Format("{0:#,##0.0000}", PU.Cantidad);
                }
            }
            return PUXEmpleado;
        }

        public async Task<List<PrecioUnitarioXEmpleadoDTO>> ObtenerParaAsignarPreciosUniatrios(int IdEmpleado, int IdProyceto)
        {
            var PreciosUnitarios = new List<PrecioUnitarioXEmpleadoDTO>();
            var PUXEmpleado = await _precioUnitarioXEmpleadoService.ObtenerXIdEmpleadoYIdProyceto(IdEmpleado, IdProyceto);

            var PUXProyecto = await _precioUnitarioProceso.ObtenerSinEstructura(IdProyceto);
            foreach (var item in PUXProyecto.Where(z => z.TipoPrecioUnitario == 1))
            {
                var PUXId = PUXEmpleado.Where(z => z.IdPrecioUnitario == item.Id).FirstOrDefault();
                if (PUXId == null) {
                    PreciosUnitarios.Add(new PrecioUnitarioXEmpleadoDTO()
                    {
                        IdEmpleado = IdEmpleado,
                        IdPrecioUnitario = item.Id,
                        IdProyceto = item.IdProyecto,
                        Codigo = item.Codigo,
                        Descripcion = item.Descripcion,
                        Unidad = item.Unidad,
                        Cantidad = item.Cantidad,
                        CantidadConFormato = String.Format("{0:#,##0.00}", item.Cantidad)
                    });
                }
            }

            return PreciosUnitarios;
        }
    }
}
