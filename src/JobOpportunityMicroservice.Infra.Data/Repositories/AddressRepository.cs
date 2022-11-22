using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Contexts;
using JobOpportunityMicroservice.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobOpportunityMicroservice.Infra.Data.Repositories;

/// <summary>
/// Address Repository
/// </summary>
public class AddressRepository : Repository<Address>
{
    public AddressRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    
    /// <summary>
    /// Get pagination address
    /// </summary>
    /// <param name="paginationParameters">Page number and page size</param>
    /// <returns>Paged list address</returns>
    public override PagedList<Address> GetPaginate(PaginationParameters paginationParameters)
        => new PagedList<Address>(
            DbSet
                .Include(a => a.JobAdvertisement)
                .OrderBy(a => a.Id),
            paginationParameters.PageNumber,
            paginationParameters.PageSize
        );
    
    /// <summary>
    /// Get address entity by id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Entity</returns>
    public override async Task<Address?> GetByIdAsync(Guid id)
        => await DbSet
            .Include(a => a.JobAdvertisement)
            .SingleOrDefaultAsync(x => x.Id.Equals(id));
}