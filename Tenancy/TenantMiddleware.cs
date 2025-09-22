using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Http;

namespace ERP_TECKIO.Tenancy
{
    //Qué es: Middleware que corre antes de los controladores: resuelve el tenant y lo guarda en HttpContext.Items.
    //Errores: Propaga lo que lance el resolver.
    //Orden: Debe estar antes de MapControllers().
    public sealed class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, ITenantResolver resolver)
        {
            var tenantId = resolver.GetTenantId(context);                 // 1) Lee la variante
            context.Items[nameof(TenantContext)] = new TenantContext(tenantId); // 2) La guarda
            await _next(context);                                          // 3) Sigue el pipeline
        }
    }
}
