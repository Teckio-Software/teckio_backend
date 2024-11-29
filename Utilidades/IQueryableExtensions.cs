

namespace ERP_TECKIO
{
    /// <summary>
    /// Clase que extiende un objeto de tipo <see cref="IQueryable"/>
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Método utilizado para paginar los registros en cada consulta de la BD
        /// </summary>
        /// <typeparam name="T">Para cualquier clase</typeparam>
        /// <param name="queryable">Objeto del tipo <see cref="IQueryable"/> que se usa para obtener determinados registros</param>
        /// <param name="paginacionDTO">Objeto del tipo <see cref="PaginacionDTO"/> para obtener el número de página en el que esta y el numero de registros por página en cada página</param>
        /// <returns>Un determinado número de registros de cualquier clase que implemente un objeto <see cref="IQueryable"/></returns>
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {
            return queryable
                .Skip((paginacionDTO.Pagina - 1) * paginacionDTO.RecordsPorPagina)
                .Take(paginacionDTO.RecordsPorPagina);
        }
    }
}
