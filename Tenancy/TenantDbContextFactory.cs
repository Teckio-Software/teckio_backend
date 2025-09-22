using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Tenancy
{
    //Qué es: Implementación que construye DbContextOptions en runtime con la cadena adecuada.
    //Pide la connection string al provider.
    //Construye DbContextOptions con esa cadena.
    //Crea el AppDbContext.
    public sealed class TenantDbContextFactory : ITenantDbContextFactory
    {
        private readonly IConnectionStringProvider _csProvider;
        private readonly DbContextOptions<AppDbContext> _baseOptions;

        public TenantDbContextFactory(
            IConnectionStringProvider csProvider,
            DbContextOptions<AppDbContext> baseOptions // vacío (sin connection string)
        )
        {
            _csProvider = csProvider;
            _baseOptions = baseOptions;
        }

        public AppDbContext CreateDbContext(string tenantId)
        {
            var cs = _csProvider.GetConnectionString(tenantId);
            var builder = new DbContextOptionsBuilder<AppDbContext>(_baseOptions);
            builder.UseSqlServer(cs); // Cambia a Npgsql/MySql si aplica
            return new AppDbContext(builder.Options);
        }
    }
}
