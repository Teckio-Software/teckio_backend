namespace ERP_TECKIO.Tenancy
{
    //Qué es: Azúcar sintáctico para leer el TenantContext.
    //Uso: HttpContext.GetTenant().TenantId.
    //Nota: El ! asume que el middleware ya corrió (por eso el orden importa).
    public static class HttpContextTenantExtensions
    {
        public static TenantContext GetTenant(this HttpContext ctx)
            => (TenantContext)ctx.Items[nameof(TenantContext)]!;
    }
}
