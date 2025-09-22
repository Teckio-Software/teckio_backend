namespace ERP_TECKIO.Tenancy
{

    //Qué hace: Busca ConnectionStrings:Tenants:{id}.
    //Errores: KeyNotFoundException si no existe.
    public sealed class ConfigConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration _cfg;
        public ConfigConnectionStringProvider(IConfiguration cfg) => _cfg = cfg;

        public string GetConnectionString(string tenantId)
        {
            var cs = _cfg[$"ConnectionStrings:Tenants:{tenantId}"];
            if (string.IsNullOrWhiteSpace(cs))
                throw new KeyNotFoundException($"No hay cadena de conexión para la variante '{tenantId}'.");
            return cs;
        }
    }
}
