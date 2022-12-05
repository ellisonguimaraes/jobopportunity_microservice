using AutoMapper;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Models;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using MediatR;

namespace JobOpportunityMicroservice.Application.Queries.GetAllJobOpportunity;

public class GetAllJobOpportunityQueryHandler : IRequestHandler<GetAllJobOpportunityQuery, PagedList<JobOpportunityCommandResponse>>
{
    private readonly IRepository<JobAdvertisement> _jobAdvertisementRepository;
    private readonly IMapper _mapper;

    public GetAllJobOpportunityQueryHandler(IRepository<JobAdvertisement> jobAdvertisementRepository, IMapper mapper)
    {
        _jobAdvertisementRepository = jobAdvertisementRepository;
        _mapper = mapper;
    }
    
    public Task<PagedList<JobOpportunityCommandResponse>> Handle(GetAllJobOpportunityQuery request, CancellationToken cancellationToken)
    {
        var jobAdvertisements = _jobAdvertisementRepository.GetPaginate(new PaginationParameters(request.PageNumber!.Value, request.PageSize!.Value));
        
        var jobAdvertisementsCommandResponse = new PagedList<JobOpportunityCommandResponse>(
            jobAdvertisements.Select(ja => _mapper.Map<JobAdvertisement, JobOpportunityCommandResponse>(ja)),
            jobAdvertisements.CurrentPage,
            jobAdvertisements.PageSize,
            jobAdvertisements.TotalCount
        );

        return Task.FromResult(jobAdvertisementsCommandResponse);
    }
}