using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Domain.Exceptions;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using MediatR;

namespace JobOpportunityMicroservice.Application.Commands.DeleteJobOpportunity;

public class DeleteJobOpportunityCommandHandler : IRequestHandler<DeleteJobOpportunityCommand, Guid>
{
    private readonly IRepository<JobAdvertisement> _jobAdvertisementRepository;

    public DeleteJobOpportunityCommandHandler(IRepository<JobAdvertisement> jobAdvertisementRepository)
    {
        _jobAdvertisementRepository = jobAdvertisementRepository;
    }
    
    public async Task<Guid> Handle(DeleteJobOpportunityCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParseExact(request.Id, "D", out var id))
            throw new BusinessException(string.Format(ErrorCodeResource.INVALID_PROPERTY, nameof(JobAdvertisement.Id)));

        await _jobAdvertisementRepository.DeleteAsync(id);

        return id;
    }
}