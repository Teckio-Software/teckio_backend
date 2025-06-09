using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class CuentaContableProceso<T> where T : DbContext
    {
        private readonly ICuentaContableService<T> _Service;
        private readonly ICuentaBancariaEmpresaService<T> _cuentaBancariaEmpresaService;
        private readonly IContratistaService<T> _contratistaService;
        private readonly IClientesService<T> _clientesService;

        public CuentaContableProceso(
            ICuentaContableService<T> Service,
            ICuentaBancariaEmpresaService<T> cuentaBancariaEmpresaService,
            IContratistaService<T> contratistaService,
            IClientesService<T> clientesService
            ) { 
            _Service = Service;
            _cuentaBancariaEmpresaService = cuentaBancariaEmpresaService;
            _contratistaService = contratistaService;
            _clientesService = clientesService;
        }

        public async Task<List<CuentaContableDTO>> ObtenerAsignables() {
            var lista = await _Service.ObtenTodos();
            var asignables = lista.Where(z => z.ExisteMovimiento == false).OrderBy(z => z.Codigo).ToList();

            var cuentasEmpresa = await _cuentaBancariaEmpresaService.ObtenTodos();
            var proveedores = await _contratistaService.ObtenTodos();
            var clientes = await _clientesService.ObtenTodos();
            var nuevosAsignables = new List<CuentaContableDTO>();
            foreach (var cuenta in asignables) {
                var existeCuenta = cuentasEmpresa.Where(z => z.IdCuentaContable == cuenta.Id).ToList();
                if (existeCuenta.Count() > 0) {
                    continue;
                }

                var existeProveedorCuentacontable = proveedores.Where(z => z.IdCuentaContable == cuenta.Id).ToList();
                if (existeCuenta.Count() > 0)
                {
                    continue;
                }
                var existeProveedorIvaAcreditableContable = proveedores.Where(z => z.IdIvaAcreditableContable == cuenta.Id).ToList();
                if (existeProveedorIvaAcreditableContable.Count() > 0)
                {
                    continue;
                }
                var existeProveedorIvaPorAcreditar = proveedores.Where(z => z.IdIvaPorAcreditar == cuenta.Id).ToList();
                if (existeProveedorIvaPorAcreditar.Count() > 0)
                {
                    continue;
                }
                var existeProveedorCuentaAnticipos = proveedores.Where(z => z.IdCuentaAnticipos == cuenta.Id).ToList();
                if (existeProveedorCuentaAnticipos.Count() > 0)
                {
                    continue;
                }
                var existeProveedorCuentaRetencionISR = proveedores.Where(z => z.IdCuentaRetencionISR == cuenta.Id).ToList();
                if (existeProveedorCuentaRetencionISR.Count() > 0)
                {
                    continue;
                }
                var existeProveedorCuentaRetencionIVA = proveedores.Where(z => z.IdCuentaRetencionIVA == cuenta.Id).ToList();
                if (existeProveedorCuentaRetencionIVA.Count() > 0)
                {
                    continue;
                }
                var existeProveedorEgresoIVAExento = proveedores.Where(z => z.IdEgresosIvaExento == cuenta.Id).ToList();
                if (existeProveedorEgresoIVAExento.Count() > 0)
                {
                    continue;
                }
                var existeProveedorEgresoIVAGravable = proveedores.Where(z => z.IdEgresosIvaGravable == cuenta.Id).ToList();
                if (existeProveedorEgresoIVAGravable.Count() > 0)
                {
                    continue;
                }
                var existeProveedorIvaAcreditableFiscal = proveedores.Where(z => z.IdIvaAcreditableFiscal == cuenta.Id).ToList();
                if (existeProveedorIvaAcreditableFiscal.Count() > 0)
                {
                    continue;
                }

                var existeClienteIdCuentaContable = clientes.Where(z => z.IdCuentaContable == cuenta.Id).ToList();
                if (existeClienteIdCuentaContable.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdIvaTrasladado = clientes.Where(z => z.IdIvaTrasladado == cuenta.Id).ToList();
                if (existeClienteIdIvaTrasladado.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdIvaPorTrasladar = clientes.Where(z => z.IdIvaPorTrasladar == cuenta.Id).ToList();
                if (existeClienteIdIvaPorTrasladar.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdCuentaAnticipos = clientes.Where(z => z.IdCuentaAnticipos == cuenta.Id).ToList();
                if (existeClienteIdCuentaAnticipos.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdIvaExento = clientes.Where(z => z.IdIvaExento == cuenta.Id).ToList();
                if (existeClienteIdIvaExento.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdIvaGravable = clientes.Where(z => z.IdIvaGravable == cuenta.Id).ToList();
                if (existeClienteIdIvaGravable.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdRetensionIsr = clientes.Where(z => z.IdRetensionIsr == cuenta.Id).ToList();
                if (existeClienteIdRetensionIsr.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdIeps = clientes.Where(z => z.IdIeps == cuenta.Id).ToList();
                if (existeClienteIdIeps.Count() > 0)
                {
                    continue;
                }
                var existeClienteIdIvaRetenido = clientes.Where(z => z.IdIvaRetenido == cuenta.Id).ToList();
                if (existeClienteIdIvaRetenido.Count() > 0)
                {
                    continue;
                }


                nuevosAsignables.Add(cuenta);
            }

            return nuevosAsignables;

        }
    }
}
