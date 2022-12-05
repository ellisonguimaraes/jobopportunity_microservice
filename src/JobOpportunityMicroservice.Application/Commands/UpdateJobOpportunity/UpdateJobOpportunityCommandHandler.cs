using AutoMapper;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using MediatR;

namespace JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;

public class UpdateJobOpportunityCommandHandler : IRequestHandler<UpdateJobOpportunityCommand, JobOpportunityCommandResponse>
{
    private readonly IRepository<JobAdvertisement> _jobAdvertisementRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateJobOpportunityCommandHandler(IRepository<JobAdvertisement> jobAdvertisementRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _jobAdvertisementRepository = jobAdvertisementRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<JobOpportunityCommandResponse> Handle(UpdateJobOpportunityCommand request, CancellationToken cancellationToken)
    {
        var jobAdvertisement = _mapper.Map<JobAdvertisement>(request);

        jobAdvertisement.JobCategories = await BuildJobCategoryListAsync(request.Categories, jobAdvertisement.Id);

        var result = await _jobAdvertisementRepository.UpdateAsync(jobAdvertisement);

        return _mapper.Map<JobOpportunityCommandResponse>(result);
    }
    
    /// <summary>
    /// Build job category to save entity
    /// </summary>
    /// <param name="categories">Category string list</param>
    /// <param name="jobAdvertisementId">Job advertisement entity ID</param>
    /// <returns>Category entity list</returns>
    private async Task<List<JobCategory>> BuildJobCategoryListAsync(IEnumerable<string> categories, Guid jobAdvertisementId)
    {
        var jobCategories = new List<JobCategory>();
    
        foreach (var c in categories)
        {
            var category = await _categoryRepository.GetByNameAsync(c);

            if (category is not null)
            {
                var jobCategory = new JobCategory
                {
                    CategoryId = category.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    JobAdvertisementId = jobAdvertisementId
                };
                
                jobCategories.Add(jobCategory);
            }
        }

        return jobCategories;
    }
}