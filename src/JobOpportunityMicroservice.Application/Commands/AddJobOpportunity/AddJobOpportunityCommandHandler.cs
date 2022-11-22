using AutoMapper;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using MediatR;

namespace JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;

public class AddJobOpportunityCommandHandler : IRequestHandler<AddJobOpportunityCommand, JobOpportunityCommandResponse>
{
    private readonly IRepository<JobAdvertisement> _jobAdvertisementRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public AddJobOpportunityCommandHandler(IRepository<JobAdvertisement> jobAdvertisementRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _jobAdvertisementRepository = jobAdvertisementRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<JobOpportunityCommandResponse> Handle(AddJobOpportunityCommand request, CancellationToken cancellationToken)
    {
        var jobAdvertisement = _mapper.Map<JobAdvertisement>(request);

        jobAdvertisement.JobCategories = await BuildJobCategoryListAsync(request.Categories);
        jobAdvertisement.UserId = Guid.NewGuid().ToString();

        var result = await _jobAdvertisementRepository.CreateAsync(jobAdvertisement);

        return _mapper.Map<JobOpportunityCommandResponse>(result);
    }
    
    /// <summary>
    /// Build job category to save entity
    /// </summary>
    /// <param name="categories">Category string list</param>
    /// <returns>Category entity list</returns>
    private async Task<List<JobCategory>> BuildJobCategoryListAsync(IEnumerable<string> categories)
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
                    UpdateAt = DateTime.UtcNow
                };
                
                jobCategories.Add(jobCategory);
            }
        }

        return jobCategories;
    }
}