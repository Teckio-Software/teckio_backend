using ExcelDataReader.Log;
using System.Text;

namespace ERP_TECKIO.Tenancy
{
    // Regresa: string con el id del tenant (tu número de la URL).
    // Errores: Si no logra resolver, debe lanzar excepción(la implementación decide).
    // Por qué: Aísla la lógica de “de dónde saco el tenant” (ruta, header, etc.).
    public interface ITenantResolver
    {
        string GetTenantId(HttpContext httpContext);
    }
}
