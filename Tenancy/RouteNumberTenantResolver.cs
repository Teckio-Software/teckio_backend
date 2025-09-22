namespace ERP_TECKIO.Tenancy
{

    //Qué es: Implementación que lee {variante:int} desde la ruta tipo api/preciounitario/1.
    //Qué hace: Busca variante en RouteValues; si no, intenta último segmento numérico.
    //Regresa: El número como string (ej. "1").
    public sealed class RouteNumberTenantResolver : ITenantResolver
    {
        public string GetTenantId(HttpContext httpContext)
        {
            if (httpContext.Request.RouteValues.TryGetValue("empresa", out var v) && v is not null)
                return v.ToString()!;

            // Plan B: último segmento numérico por si algún controlador usa otra convención
            var last = httpContext.Request.Path.Value?
                .Split('/', StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault();

            if (int.TryParse(last, out var n))
                return n.ToString();

            throw new InvalidOperationException("No se pudo resolver la variante/tenant desde la URL.");
        }
    }
}
