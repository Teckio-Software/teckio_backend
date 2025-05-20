using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ContratistaCuentasContablesProceso<T> where T : DbContext
    {
        private readonly IContratistaService<T> _contratistaService;
        private readonly ICuentaContableService<T> _cuentaContableService;
        public ContratistaCuentasContablesProceso(
            IContratistaService<T> contratistaService,
            ICuentaContableService<T> cuentaContableService
            )
        {
            _contratistaService = contratistaService;
            _cuentaContableService = cuentaContableService;
        }

        public async Task<List<CuentaContableDTO>> obtenerXContratista(int IdContratista) {
            var cuentas = new List<CuentaContableDTO>();

            var contratista = await _contratistaService.ObtenXId(IdContratista);
            if (contratista.Id <= 0) {
                return cuentas;
            }

            var cuentaContable = new CuentaContableDTO();

            if (contratista.IdCuentaContable != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdCuentaContable));
                cuentaContable.TipoCuentaContableDescripcion = "Cuenta Contable";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdIvaAcreditableContable != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdIvaAcreditableContable));
                cuentaContable.TipoCuentaContableDescripcion = "IVA Acreditable";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdIvaPorAcreditar != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdIvaPorAcreditar));
                cuentaContable.TipoCuentaContableDescripcion = "IVA Por Acreditar";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdCuentaAnticipos != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdCuentaAnticipos));
                cuentaContable.TipoCuentaContableDescripcion = "Anticipos";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdCuentaRetencionISR != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdCuentaRetencionISR));
                cuentaContable.TipoCuentaContableDescripcion = "Retención ISR";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdCuentaRetencionIVA != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdCuentaRetencionIVA));
                cuentaContable.TipoCuentaContableDescripcion = "Retencón IVA";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdEgresosIvaExento != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdEgresosIvaExento));
                cuentaContable.TipoCuentaContableDescripcion = "Egresos IVA Exento";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdEgresosIvaGravable != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdEgresosIvaGravable));
                cuentaContable.TipoCuentaContableDescripcion = "Egresos IVA Gravable";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }
            if (contratista.IdIvaAcreditableFiscal != null)
            {
                cuentaContable = await _cuentaContableService.ObtenXId(Convert.ToInt32(contratista.IdIvaAcreditableFiscal));
                cuentaContable.TipoCuentaContableDescripcion = "IVA Acreditable Fiscal";
                if (cuentaContable.Id != 0)
                {
                    cuentas.Add(cuentaContable);
                }
            }

            return cuentas;
        }
    }
}
