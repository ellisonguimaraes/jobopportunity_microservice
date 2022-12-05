using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Models;

namespace JobOpportunityMicroservice.Infra.Data.Repositories.Interface;

public interface IRepository<T> where T : BaseEntity
{
    PagedList<T> GetPaginate(PaginationParameters paginationParameters);

    Task<T> CreateAsync(T item);
    
    Task<T?> GetByIdAsync(Guid id);
    
    Task<T> DeleteAsync(Guid id);

    Task<T> UpdateAsync(T item);
}