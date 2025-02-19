using System.Linq.Expressions;
using AutoMapper;
using GeolocationApp.Application.Interfaces;
using GeolocationApp.Domain.DTOs;
using GeolocationApp.Domain.Entities;
using GeolocationApp.Domain.Repositories;

namespace GeolocationApp.Application.Services
{
    public class VisitService(IVisitRepository visitRepository, IMapper mapper) : IVisitService
    {
        public async Task<ResponseVisit?> GetVisitByIdAsync(Guid id)
        {
            var visit = await visitRepository.GetByIdAsync(id);
            return mapper.Map<ResponseVisit>(visit);
        }

        public async Task<ResponseVisit> CreateVisitAsync(UpdateVisit updateVisit)
        {
            // Check if exist Ip
            var existVisit = await visitRepository.GetVisitByIpAsync(updateVisit.Ip);
            if (existVisit != null) return mapper.Map<ResponseVisit>(existVisit);
            
            var visit = mapper.Map<Visit>(updateVisit);
            var response = await visitRepository.AddAsync(visit);
            return mapper.Map<ResponseVisit>(response);
        }

        public async Task<ResponseVisit?> UpdateVisitAsync(Guid id, UpdateVisit updateVisitUpdate)
        {
            var visitEntity = await visitRepository.GetByIdAsync(id);

            mapper.Map(updateVisitUpdate, visitEntity);
            var updatedVisit = await visitRepository.UpdateAsync(visitEntity);
            return mapper.Map<ResponseVisit>(updatedVisit);
        }

        public async Task<bool> DeleteVisitAsync(Guid id)
        {
            return await visitRepository.DeleteAsync(id);
        }

        public async Task<(IEnumerable<ResponseVisit> data, int totalCount)> GetPagedVisitsAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Visit, bool>>? filter = null,
            Func<IQueryable<Visit>, IOrderedQueryable<Visit>>? orderBy = null)
        {
            var (visits, totalCount) = await visitRepository.GetPagedVisitsAsync(
                pageIndex,
                pageSize,
                filter,
                orderBy
            );

            var visitResponses = mapper.Map<IEnumerable<ResponseVisit>>(visits);

            return (visitResponses, totalCount);
        }

        public async Task<(IEnumerable<ResponseVisit> data, int totalCount)> GetVisitsOrderedByDateAsync(int pageIndex, int pageSize,
            Expression<Func<Visit, bool>>? filter = null)
        {
            var (visits, totalCount) = await visitRepository.GetVisitsOrderedByDateAsync(
                pageIndex,
                pageSize,
                null
            );

            var visitResponses = mapper.Map<IEnumerable<ResponseVisit>>(visits);

            return (visitResponses, totalCount);
        }
    }
}