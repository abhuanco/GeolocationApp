using System.Linq.Expressions;
using GeolocationApp.Domain.Entities;

namespace GeolocationApp.Domain.Repositories
{
    public interface IVisitRepository : IRepositoryBase<Visit>
    {
        /// <summary>
        /// Obtiene visitas paginadas con un filtro opcional.
        /// </summary>
        /// <param name="pageIndex">Índice de la página actual (1-based).</param>
        /// <param name="pageSize">Cantidad de elementos por página.</param>
        /// <param name="filter">Filtro opcional para las visitas.</param>
        /// <param name="orderBy">Criterio opcional para ordenar los resultados.</param>
        /// <returns>Un tupla con la lista de visitas y el total de elementos.</returns>
        Task<(IEnumerable<Visit> data, int totalCount)> GetPagedVisitsAsync(int pageIndex, int pageSize, Expression<Func<Visit, bool>>? filter = null, Func<IQueryable<Visit>, IOrderedQueryable<Visit>>? orderBy = null);

        /// <summary>
        /// Obtiene visitas paginadas ordenadas por fecha de visita.
        /// </summary>
        /// <param name="pageIndex">Índice de la página actual (1-based).</param>
        /// <param name="pageSize">Cantidad de elementos por página.</param>
        /// <param name="filter">Filtro opcional para las visitas.</param>
        /// <returns>Un tupla con la lista de visitas y el total de elementos.</returns>
        Task<(IEnumerable<Visit> data, int totalCount)> GetVisitsOrderedByDateAsync(int pageIndex, int pageSize, Expression<Func<Visit, bool>>? filter = null);
        
        Task<Visit?> GetVisitByIpAsync(string ip);
    }
}