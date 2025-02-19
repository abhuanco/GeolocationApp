using System.Linq.Expressions;
using GeolocationApp.Application.DTOs;
using GeolocationApp.Domain.DTOs;
using GeolocationApp.Domain.Entities;

namespace GeolocationApp.Application.Interfaces
{
    public interface IVisitService
    {
        Task<ResponseVisit?> GetVisitByIdAsync(Guid id);

        Task<ResponseVisit> CreateVisitAsync(UpdateVisit updateVisit);
        
        Task<ResponseVisit?> UpdateVisitAsync(Guid id, UpdateVisit updateVisit);
        
        Task<bool> DeleteVisitAsync(Guid id);
        
        Task<(IEnumerable<ResponseVisit> data, int totalCount)> GetPagedVisitsAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Visit, bool>>? filter = null,
            Func<IQueryable<Visit>, IOrderedQueryable<Visit>>? orderBy = null
        );
        
        Task<(IEnumerable<ResponseVisit> data, int totalCount)> GetVisitsOrderedByDateAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Visit, bool>>? filter = null
        );
    }
}