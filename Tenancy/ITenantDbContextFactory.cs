using System;

namespace ERP_TECKIO.Tenancy
{
    //Qué es: Contrato que crea un AppDbContext con la conexión del tenant.
    //Regresa: AppDbContext listo para usar.
    public interface ITenantDbContextFactory
    {
        AppDbContext CreateDbContext(string tenantId);
    }
}
