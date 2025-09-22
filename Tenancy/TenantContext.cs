using Azure.Core;

namespace ERP_TECKIO.Tenancy
{
    //Qué es: Guarda el TenantId resuelto para el request actual.
    //Uso: Lo mete el middleware en HttpContext.Items para que esté disponible más adelante.
    public sealed class TenantContext
    {
        public string TenantId { get; }
        public TenantContext(string tenantId) => TenantId = tenantId;
    }
}
