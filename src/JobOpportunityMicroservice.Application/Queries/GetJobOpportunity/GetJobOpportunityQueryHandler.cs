using AutoMapper;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Domain.Exceptions;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using MediatR;

namespace JobOpportunityMicroservice.Application.Queries.GetJobOpportunity;

public class GetJobOpportunityQueryHandler : IRequestHandler<GetJobOpportunityQuery, JobOpportunityCommandResponse>
{
    private readonly IRepository<JobAdvertisement> _jobAdvertisementRepository;
    private readonly IMapper _mapper;

    public GetJobOpportunityQueryHandler(IRepository<JobAdvertisement> jobAdvertisementRepository, IMapper mapper)
    {
        _jobAdvertisementRepository = jobAdvertisementRepository;
        _mapper = mapper;
    }
    
    public async Task<JobOpportunityCommandResponse> Handle(GetJobOpportunityQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var id))
            throw new BusinessException(string.Format(ErrorCodeResource.INVALID_PROPERTY, nameof(JobAdvertisement.Id)));
        
        var result = await _jobAdvertisementRepository.GetByIdAsync(id);

        if (result is null)
            throw new BusinessException(string.Format(ErrorCodeResource.ENTITY_NOT_FOUND, nameof(JobAdvertisement)));

        return _mapper.Map<JobOpportunityCommandResponse>(result);
    }
}