using AutoMapper;
using FluentValidation;
using JobOpportunityMicroservice.Application.Models;
using JobOpportunityMicroservice.Application.Services.Interfaces;
using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Domain.Exceptions;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using JobOpportunityMicroservice.Infra.Data.Models;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JobOpportunityMicroservice.Application.Services;

public class JobAdvertisementServices : IJobAdvertisementServices
{
    #region Constants
    private const string GET_PAGINATE_TAG = "[JobAdvertisementServices:GetPaginateAsync]";
    private const string GET_BY_ID_TAG = "[JobAdvertisementServices:GetByIdAsync]";
    private const string SEMICOLON  = ";";
    #endregion

    private readonly IRepository<JobAdvertisement> _repositoryJobAdvertisement;
    private readonly IMapper _mapper;
    private readonly ILogger<JobAdvertisementServices> _logger;
    private readonly IValidator<PaginationParametersRequest> _paginationParametersRequest;

    public JobAdvertisementServices(
        IRepository<JobAdvertisement> repositoryJobAdvertisement,
        IMapper mapper,
        ILogger<JobAdvertisementServices> logger,
        IValidator<PaginationParametersRequest> paginationParametersRequest)
    {
        _repositoryJobAdvertisement = repositoryJobAdvertisement;
        _mapper = mapper;
        _logger = logger;
        _paginationParametersRequest = paginationParametersRequest;
    }

    /// <summary>
    /// Get paginatation job advertisement
    /// </summary>
    /// <param name="paginationParametersRequest">Page number and page size</param>
    /// <returns>Job advertisement pagination data</returns>
    public async Task<GenericHttpResponse<PagedList<JobAdvertisementResponse>>> GetPaginateAsync(PaginationParametersRequest paginationParametersRequest)
    {
        try
        {
            var result = await _paginationParametersRequest.ValidateAsync(paginationParametersRequest);

            if (!result.IsValid)
                throw new BusinessException(ErrorCodeResource.DATA_VALIDATION_FAILURE, result.Errors.Select(e => e.ErrorMessage));

            var jobAdvertisements = _repositoryJobAdvertisement.GetPaginate(
                new PaginationParameters(
                    paginationParametersRequest.PageNumber!.Value,
                    paginationParametersRequest.PageSize!.Value
                ));

            var jobAdvertisementsResponse = new PagedList<JobAdvertisementResponse>(
                jobAdvertisements.Select(ja => _mapper.Map<JobAdvertisement, JobAdvertisementResponse>(ja)),
                jobAdvertisements.CurrentPage,
                jobAdvertisements.PageSize,
                jobAdvertisements.TotalCount
            );

            return new GenericHttpResponse<PagedList<JobAdvertisementResponse>>
            {
                Data = jobAdvertisementsResponse,
                StatusCode = StatusCodes.Status200OK
            };
        }
        catch (BusinessException ex)
        {
            var response = new GenericHttpResponse<PagedList<JobAdvertisementResponse>>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = ex.Errors.Any() ? new List<string> { $"{ex.Message}: {string.Join($"{SEMICOLON} ", ex.Errors)}" } : new List<string> { $"{ex.Message}" }
            };

            _logger.LogError(ex, $"{GET_PAGINATE_TAG} {ex}");

            return response;
        }
        catch (Exception ex)
        {
            var response = new GenericHttpResponse<PagedList<JobAdvertisementResponse>>
            {
                Errors = new List<string> { ErrorCodeResource.UNEXPECTED_ERROR_OCURRED },
                StatusCode = StatusCodes.Status400BadRequest
            };
            
            _logger.LogError(ex, $"{GET_PAGINATE_TAG} {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {response.TraceId}");

            return response;
        }
    }
    
    /// <summary>
    /// Get by id job advertisement
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Job advertisement details</returns>
    public async Task<GenericHttpResponse<JobAdvertisementResponse>> GetByIdAsync(string strId)
    {
        try
        {
            if (!Guid.TryParse(strId, out var id))
                throw new BusinessException(string.Format(ErrorCodeResource.INVALID_PROPERTY, "Id"));
            
            var jobAdvertisement = await _repositoryJobAdvertisement.GetByIdAsync(id);

            if (jobAdvertisement is null)
                throw new BusinessException(string.Format(ErrorCodeResource.ENTITY_NOT_FOUND, "Job advertisement"));

            return new GenericHttpResponse<JobAdvertisementResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = _mapper.Map<JobAdvertisement, JobAdvertisementResponse>(jobAdvertisement)
            };
        }
        catch (BusinessException ex)
        {
            var response = new GenericHttpResponse<JobAdvertisementResponse>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = ex.Errors.Any() ? new List<string> { $"{ex.Message}: {string.Join($"{SEMICOLON} ", ex.Errors)}" } : new List<string> { $"{ex.Message}" }
            };

            _logger.LogError(ex, $"{GET_BY_ID_TAG} {ex}, Id: {strId}");

            return response;
        }
        catch (Exception ex)
        {
            var response = new GenericHttpResponse<JobAdvertisementResponse>
            {
                Errors = new List<string> { ErrorCodeResource.UNEXPECTED_ERROR_OCURRED },
                StatusCode = StatusCodes.Status400BadRequest
            };
            
            _logger.LogError(ex, $"{GET_BY_ID_TAG} {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {response.TraceId}, Id: {strId}");

            return response;
        }
    }
}