using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Contexts;
using JobOpportunityMicroservice.Infra.Data.Models;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobOpportunityMicroservice.Infra.Data.Repositories;

/// <summary>
/// Category Repository
/// </summary>
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    /// <summary>
    /// Get pagination Category
    /// </summary>
    /// <param name="paginationParameters">Page number and page size</param>
    /// <returns>Paged list category</returns>
    public override PagedList<Category> GetPaginate(PaginationParameters paginationParameters)
        => new PagedList<Category>(
            DbSet
                .Include(c => c.JobCategories)
                    .ThenInclude(jc => jc.JobAdvertisement)
                    .ThenInclude(ja => ja.Address)
                .OrderBy(c => c.Id),
            paginationParameters.PageNumber,
            paginationParameters.PageSize
        );
    
    /// <summary>
    /// Get category entity by id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Entity</returns>
    public override async Task<Category?> GetByIdAsync(Guid id)
        => await DbSet
            .Include(c => c.JobCategories)
                .ThenInclude(jc => jc.JobAdvertisement)
                .ThenInclude(ja => ja.Address)
            .SingleOrDefaultAsync(x => x.Id.Equals(id));
    
    /// <summary>
    /// Get category entity by name
    /// </summary>
    /// <param name="name">Category name</param>
    /// <returns>Entity</returns>
    public async Task<Category?> GetByNameAsync(string name)
        => await DbSet
            .Include(c => c.JobCategories)
                .ThenInclude(jc => jc.JobAdvertisement)
                .ThenInclude(ja => ja.Address)
            .SingleOrDefaultAsync(x => x.Name.ToUpper().Equals(name.ToUpper()));
}