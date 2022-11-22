using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;
using JobOpportunityMicroservice.Application.Models;
using JobOpportunityMicroservice.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobOpportunityMicroservice.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OpportunityController : ControllerBase
{
    #region Constants

    private const string TRACE_ID_NAME = "TraceId";

    #endregion

    private readonly IJobAdvertisementServices _jobAdvertisementServices;
    private readonly IMediator _mediator;
    private readonly ILogger<OpportunityController> _logger;

    public OpportunityController(IJobAdvertisementServices jobAdvertisementServices, IMediator mediator,
        ILogger<OpportunityController> logger)
    {
        _jobAdvertisementServices = jobAdvertisementServices;
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get paginatation job advertisement
    /// </summary>
    /// <param name="paginationParametersRequest">Page number and page size</param>
    /// <returns>Job advertisement pagination data</returns>
    [HttpGet]
    public async Task<IActionResult> GetPaginationAsync(
        [FromQuery] PaginationParametersRequest paginationParametersRequest)
    {
        var result = await _jobAdvertisementServices.GetPaginateAsync(paginationParametersRequest);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Get by id job advertisement
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Job advertisement details</returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var result = await _jobAdvertisementServices.GetByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Add new job opportunity
    /// </summary>
    /// <param name="command">Add job command</param>
    /// <returns>Command response</returns>
    [HttpPost]
    public async Task<IActionResult> AddJobOpportunityAsync([FromBody] AddJobOpportunityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new GenericHttpResponse<JobOpportunityCommandResponse> { Data = result });
    }

    /// <summary>
    /// Update new job opportunity
    /// </summary>
    /// <param name="command">Update job command</param>
    /// <returns>Command response</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateJobOpportunityAsync([FromBody] UpdateJobOpportunityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new GenericHttpResponse<JobOpportunityCommandResponse> { Data = result });
    }
}