using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO
{
    /// <summary>
    /// Clase usada para mostrar en los "headers" la Cantidad de registros total en cada consulta que se haga a la base de datos
    /// Usado para el paginado
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Método para insertar el total de registros en la parte de los headers (usado para el paginado)
        /// </summary>
        /// <typeparam name="T">Clase genérica</typeparam>
        /// <param name="httpContext">Objeto del tipo <see cref="HttpContext"/> pertenciente a cada controller</param>
        /// <param name="queryable">Objeto del tipo <see cref="IQueryable"/> de cualquier clase</param>
        /// <returns>El total de registros en cada consulta a la BD</returns>
        /// <exception cref="ArgumentNullException">En caso de que no se implemente el objeto <see cref="HttpContext"/> en el controlador</exception>
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            await Task.Run(() =>
            {
                if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

                double Cantidad = queryable.Count();
                httpContext.Response.Headers.Add("CantidadTotalRegistros", Cantidad.ToString());
            });
        }
        public async static Task InsertarParametrosCreacionEdicionEnCabecera<T>(this HttpContext httpContext,
            bool estatus, string mensaje)
        {
            await Task.Run(() =>
            {
                if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }
                string estatusString = estatus ? "1" : "0";

                httpContext.Response.Headers.Add("estatus", estatusString);
                httpContext.Response.Headers.Add("mensaje", mensaje);
            });
        }
    }
}
