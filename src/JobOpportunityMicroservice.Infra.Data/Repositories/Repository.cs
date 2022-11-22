using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Contexts;
using JobOpportunityMicroservice.Infra.Data.Models;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobOpportunityMicroservice.Infra.Data.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext ApplicationDbContext;
    
    protected readonly DbSet<T> DbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
        DbSet = ApplicationDbContext.Set<T>();
    }
    
    /// <summary>
    /// Get paginate entity
    /// </summary>
    /// <param name="paginationParameters">Page number and page size</param>
    /// <returns>Paged list entity</returns>
    public virtual PagedList<T> GetPaginate(PaginationParameters paginationParameters)
        => new PagedList<T>(
            DbSet.OrderBy(i => i.Id),
            paginationParameters.PageNumber,
            paginationParameters.PageSize
        );
    
    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Entity</returns>
    public virtual async Task<T?> GetByIdAsync(Guid id)
        => await DbSet.SingleOrDefaultAsync(i => i.Id.Equals(id));

    /// <summary>
    /// Create in database
    /// </summary>
    /// <param name="item">Type T item</param>
    /// <returns>Item with id</returns>
    public async Task<T> CreateAsync(T item)
    {
        item.CreatedAt = DateTime.UtcNow;
        item.UpdateAt = DateTime.UtcNow;
        
        DbSet.Add(item);
        await ApplicationDbContext.SaveChangesAsync();
        
        return item;
    }

    /// <summary>
    /// Delete address in database
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Deleted address</returns>
    public async Task<T?> DeleteAsync(Guid id)
    {
        var getItem = await DbSet.SingleOrDefaultAsync(i => i.Id.Equals(id));

        if (getItem is null)
            return default(T);

        DbSet.Remove(getItem);
        await ApplicationDbContext.SaveChangesAsync();

        return getItem;
    }
    
    /// <summary>
    /// Update address in database
    /// </summary>
    /// <param name="item">Address</param>
    /// <returns>Updated Address</returns>
    public async Task<T?> UpdateAsync(T item)
    {
        item.UpdateAt = DateTime.UtcNow;
        
        ApplicationDbContext.Entry(item).Property(x => x.CreatedAt).IsModified = false;
        
        await ApplicationDbContext.SaveChangesAsync();

        return await GetByIdAsync(item.Id);
    }
}