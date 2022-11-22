using JobOpportunityMicroservice.Domain;

namespace JobOpportunityMicroservice.Infra.Data.Repositories.Interface;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<Category?> GetByNameAsync(string name);
}