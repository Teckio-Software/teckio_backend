using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class ClienteProceso<T> where T : DbContext
    {
        private readonly IClientesService<T> _Service;

        private readonly ICuentaContableService<T> _CuentaContableService;
        public ClienteProceso(
            IClientesService<T> service
            , ICuentaContableService<T> cuentaContable) {
            _Service = service;
            _CuentaContableService = cuentaContable;
        }

        
    }
}
