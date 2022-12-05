using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Domain.Exceptions;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using JobOpportunityMicroservice.Infra.Data.Contexts;
using JobOpportunityMicroservice.Infra.Data.Models;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobOpportunityMicroservice.Infra.Data.Repositories;

/// <summary>
/// Job Advertisement Repository
/// </summary>
public class JobAdvertisementRepository : Repository<JobAdvertisement>
{
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<JobCategory> _jobCategoryRepository;

    public JobAdvertisementRepository(ApplicationDbContext applicationDbContext, IRepository<Address> addressRepository, IRepository<JobCategory> jobCategoryRepository) : base(applicationDbContext)
    {
        _addressRepository = addressRepository;
        _jobCategoryRepository = jobCategoryRepository;
    }

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
    
    /// <summary>
    /// Update job advertisement in database
    /// </summary>
    /// <param name="item">Job advertisement</param>
    /// <returns>Updated job advertisement</returns>
    public override async Task<JobAdvertisement> UpdateAsync(JobAdvertisement item)
    {
        var getItem = await GetByIdAsync(item.Id);
        
        if (getItem is null)
            throw new BusinessException(string.Format(ErrorCodeResource.ENTITY_NOT_FOUND, nameof(JobAdvertisement)));
        
        item.UpdateAt = DateTime.UtcNow;

        ApplicationDbContext.Entry(getItem).CurrentValues.SetValues(item);
        ApplicationDbContext.Entry(getItem).Property(x => x.CreatedAt).IsModified = false;
        ApplicationDbContext.Entry(getItem).Property(x => x.UserId).IsModified = false;
        
        await ApplicationDbContext.SaveChangesAsync();
        
        getItem.Address = await _addressRepository.UpdateAsync(item.Address!);

        await UpdateJobCategoriesAsync(getItem.JobCategories, item.JobCategories);
        
        return getItem;
    }

    /// <summary>
    /// Update job categories entities
    /// </summary>
    /// <param name="currentJobCategories">Current JobCategory</param>
    /// <param name="newJobCategories">New JobCategory</param>
    private async Task UpdateJobCategoriesAsync(List<JobCategory> currentJobCategories, List<JobCategory> newJobCategories)
    {
        var jobCategoryToRemove = currentJobCategories.Where(x => !newJobCategories.Any(jc => jc.Id.Equals(x.Id))).ToList();

        for (var i = jobCategoryToRemove.Count - 1; i >= 0; i--)
        {
            await _jobCategoryRepository.DeleteAsync(jobCategoryToRemove[i].Id);
            currentJobCategories.RemoveAll(current => current.Id.Equals(jobCategoryToRemove[i].Id));
        }

        var jobCategoryToAdd = newJobCategories.Where(x => !currentJobCategories.Any(jc => jc.Id.Equals(x.Id)));

        foreach (var jc in jobCategoryToAdd)
            await _jobCategoryRepository.CreateAsync(jc);
    }
}