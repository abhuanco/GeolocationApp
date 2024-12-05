using System.Linq.Expressions;
using GeolocationApp.Application.DTOs;
using GeolocationApp.Domain.DTOs;
using GeolocationApp.Domain.Entities;

namespace GeolocationApp.Application.Interfaces
{
    public interface IVisitService
    {
        Task<VisitResponseDto?> GetVisitByIdAsync(Guid id);

        Task<VisitResponseDto> CreateVisitAsync(VisitRequestDto visitRequestDto);
        
        Task<VisitResponseDto?> UpdateVisitAsync(Guid id, VisitRequestDto visit);
        
        Task<bool> DeleteVisitAsync(Guid id);
        
        Task<(IEnumerable<VisitResponseDto> data, int totalCount)> GetPagedVisitsAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Visit, bool>>? filter = null,
            Func<IQueryable<Visit>, IOrderedQueryable<Visit>>? orderBy = null
        );
        
        Task<(IEnumerable<VisitResponseDto> data, int totalCount)> GetVisitsOrderedByDateAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Visit, bool>>? filter = null
        );
    }
}