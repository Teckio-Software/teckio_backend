using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ERP_TECKIO.Tenancy
{
    //Parámetros: tenantId(por ej., "1").
    //Regresa: Cadena de conexión lista para EF.
    //Por qué: Permite cambiar la fuente (appsettings, base central, vault), sin tocar el resto.
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string tenantId);
    }
}
