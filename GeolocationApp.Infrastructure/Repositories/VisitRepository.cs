using System.Linq.Expressions;
using GeolocationApp.Domain.Entities;
using GeolocationApp.Domain.Repositories;
using GeolocationApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GeolocationApp.Infrastructure.Repositories
{
    public class VisitRepository(ApplicationDbContext context) : RepositoryBase<Visit>(context), IVisitRepository
    {
        public async Task<(IEnumerable<Visit> data, int totalCount)> GetPagedVisitsAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Visit, bool>>? filter = null,
            Func<IQueryable<Visit>, IOrderedQueryable<Visit>>? orderBy = null)
        {
            IQueryable<Visit> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            int totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<(IEnumerable<Visit> data, int totalCount)> GetVisitsOrderedByDateAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Visit, bool>>? filter = null)
        {
            return await GetPagedVisitsAsync(
                pageIndex,
                pageSize,
                filter,
                orderBy: query => query.OrderBy(v => v.VisitDate)
            );
        }

        public async Task<Visit?> GetVisitByIpAsync(string ip)
        {
            return await _dbSet.Where(c => c.Ip == ip).FirstOrDefaultAsync();
        }
    }
}