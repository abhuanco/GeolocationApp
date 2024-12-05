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
        public async Task<VisitResponseDto?> GetVisitByIdAsync(Guid id)
        {
            var visit = await visitRepository.GetByIdAsync(id);
            return mapper.Map<VisitResponseDto>(visit);
        }

        public async Task<VisitResponseDto> CreateVisitAsync(VisitRequestDto visitRequestDto)
        {
            // Check if exist Ip
            var existVisit = await visitRepository.GetVisitByIpAsync(visitRequestDto.Ip);
            if (existVisit != null) return mapper.Map<VisitResponseDto>(existVisit);
            
            var visit = mapper.Map<Visit>(visitRequestDto);
            var response = await visitRepository.AddAsync(visit);
            return mapper.Map<VisitResponseDto>(response);
        }

        public async Task<VisitResponseDto?> UpdateVisitAsync(Guid id, VisitRequestDto visitRequest)
        {
            var visitEntity = await visitRepository.GetByIdAsync(id);

            mapper.Map(visitRequest, visitEntity);
            var updatedVisit = await visitRepository.UpdateAsync(visitEntity);
            return mapper.Map<VisitResponseDto>(updatedVisit);
        }

        public async Task<bool> DeleteVisitAsync(Guid id)
        {
            return await visitRepository.DeleteAsync(id);
        }

        public async Task<(IEnumerable<VisitResponseDto> data, int totalCount)> GetPagedVisitsAsync(
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

            var visitResponses = mapper.Map<IEnumerable<VisitResponseDto>>(visits);

            return (visitResponses, totalCount);
        }

        public async Task<(IEnumerable<VisitResponseDto> data, int totalCount)> GetVisitsOrderedByDateAsync(int pageIndex, int pageSize,
            Expression<Func<Visit, bool>>? filter = null)
        {
            var (visits, totalCount) = await visitRepository.GetVisitsOrderedByDateAsync(
                pageIndex,
                pageSize,
                null
            );

            var visitResponses = mapper.Map<IEnumerable<VisitResponseDto>>(visits);

            return (visitResponses, totalCount);
        }
    }
}