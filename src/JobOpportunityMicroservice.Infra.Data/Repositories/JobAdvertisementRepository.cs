using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Contexts;
using JobOpportunityMicroservice.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobOpportunityMicroservice.Infra.Data.Repositories;

/// <summary>
/// Job Advertisement Repository
/// </summary>
public class JobAdvertisementRepository : Repository<JobAdvertisement>
{
    public JobAdvertisementRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    /// <summary>
    /// Get pagination Job Advertisement
    /// </summary>
    /// <param name="paginationParameters">Page number and page size</param>
    /// <returns>Paged list job advertisement</returns>
    public override PagedList<JobAdvertisement> GetPaginate(PaginationParameters paginationParameters)
        => new PagedList<JobAdvertisement>(
            DbSet
                .Include(ja => ja.JobCategories).ThenInclude(jc => jc.Category)
                .Include(ja => ja.Address)
                .OrderBy(ja => ja.Id),
            paginationParameters.PageNumber,
            paginationParameters.PageSize
        );
    
    /// <summary>
    /// Get job advertisement entity by id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Entity</returns>
    public override async Task<JobAdvertisement?> GetByIdAsync(Guid id)
        => await DbSet
            .Include(ja => ja.JobCategories).ThenInclude(jc => jc.Category)
            .Include(ja => ja.Address)
            .SingleOrDefaultAsync(x => x.Id.Equals(id));
}