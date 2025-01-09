using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ClienteCuentasContablesProceso<T> where T : DbContext
    {
        private readonly IClientesService<T> _clientesService;
        private readonly ICuentaContableService<T> _cuentaContableService;

        public ClienteCuentasContablesProceso(
                IClientesService<T> clientesService,
                ICuentaContableService<T> cuentaContableService
            )
        {
            _clientesService = clientesService;
            _cuentaContableService = cuentaContableService;
        }

        public async Task<List<CuentaContableDTO>> obtenerXCliente (int IdCliente)
        {
            var cuentas = new List<CuentaContableDTO>();

            var cliente = await _clientesService.ObtenXId(IdCliente);
            if (cliente.Id <= 0)
            {
                return cuentas;
            }

            var cuentaContable = new CuentaContableDTO();


            if (cliente.IdCuentaContable != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdCuentaContable));
                cuentaContable.TipoCuentaContable = "Cuenta Contable";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdIvaTrasladado != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdIvaTrasladado));
                cuentaContable.TipoCuentaContable = "IVA Trasladado";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdIvaPorTrasladar != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdIvaPorTrasladar));
                cuentaContable.TipoCuentaContable = "IVA Por Trasladar";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdCuentaAnticipos != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdCuentaAnticipos));
                cuentaContable.TipoCuentaContable = "Anticipos";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdIvaExento != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdIvaExento));
                cuentaContable.TipoCuentaContable = "IVA Exento";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdIvaGravable != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdIvaGravable));
                cuentaContable.TipoCuentaContable = "IVA Gravable";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdRetensionIsr != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdRetensionIsr));
                cuentaContable.TipoCuentaContable = "Retencón ISR";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdIeps != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdIeps));
                cuentaContable.TipoCuentaContable = "IEPS";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (cliente.IdIvaRetenido != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(cliente.IdIvaRetenido));
                cuentaContable.TipoCuentaContable = "IVA Retenido";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }

            return cuentas;
        }
    }
}
