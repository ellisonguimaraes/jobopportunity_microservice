using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Domain.Exceptions;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
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
    public virtual async Task<T> CreateAsync(T item)
    {
        item.CreatedAt = DateTime.UtcNow;
        item.UpdateAt = DateTime.UtcNow;
        
        DbSet.Add(item);
        await ApplicationDbContext.SaveChangesAsync();
        
        return item;
    }

    /// <summary>
    /// Delete in database
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Deleted entity</returns>
    public virtual async Task<T> DeleteAsync(Guid id)
    {
        var getItem = await GetByIdAsync(id);
        
        if (getItem is null)
            throw new BusinessException(string.Format(ErrorCodeResource.ENTITY_NOT_FOUND, typeof(T).Name));

        DbSet.Remove(getItem);
        await ApplicationDbContext.SaveChangesAsync();

        return getItem;
    }
    
    /// <summary>
    /// Update entity in database
    /// </summary>
    /// <param name="item">Entity item</param>
    /// <returns>Updated entity</returns>
    public virtual async Task<T> UpdateAsync(T item)
    {
        var getItem = await GetByIdAsync(item.Id);
        
        if (getItem is null)
            throw new BusinessException(string.Format(ErrorCodeResource.ENTITY_NOT_FOUND, typeof(T).Name));
        
        item.UpdateAt = DateTime.UtcNow;
        
        ApplicationDbContext.Entry(getItem).CurrentValues.SetValues(item);
        ApplicationDbContext.Entry(getItem).Property(x => x.CreatedAt).IsModified = false;
        
        await ApplicationDbContext.SaveChangesAsync();
        
        return getItem;
    }
}