using JobOpportunityMicroservice.Application.Models;
using JobOpportunityMicroservice.Infra.Data.Models;

namespace JobOpportunityMicroservice.Application.Services.Interfaces;

public interface IJobAdvertisementServices
{
    Task<GenericHttpResponse<PagedList<JobAdvertisementResponse>>> GetPaginateAsync(
        PaginationParametersRequest paginationParametersRequest);

    Task<GenericHttpResponse<JobAdvertisementResponse>> GetByIdAsync(string strId);
}