using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Application.Commands.DeleteJobOpportunity;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;
using JobOpportunityMicroservice.Application.Models;
using JobOpportunityMicroservice.Application.Queries.GetAllJobOpportunity;
using JobOpportunityMicroservice.Application.Queries.GetJobOpportunity;
using JobOpportunityMicroservice.Infra.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OpportunityController : ControllerBase
{
    private readonly IMediator _mediator;

    public OpportunityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get paginatation job advertisement
    /// </summary>
    /// <param name="getAllJobOpportunityQuery">Page number and page size</param>
    /// <returns>Job advertisement pagination data</returns>
    [HttpGet]
    public async Task<IActionResult> GetPaginationAsync([FromQuery] GetAllJobOpportunityQuery getAllJobOpportunityQuery)
    {
        var result = await _mediator.Send(getAllJobOpportunityQuery);
        
        var metadata = new {
            result.TotalCount,
            result.PageSize,
            result.CurrentPage,
            result.HasNext,
            result.HasPrevious,
            result.TotalPages
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        
        return Ok(new GenericHttpResponse<PagedList<JobOpportunityCommandResponse>> { Data = result });
    }

    /// <summary>
    /// Get by id job advertisement
    /// </summary>
    /// <param name="query">Id</param>
    /// <returns>Job advertisement details</returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] GetJobOpportunityQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(new GenericHttpResponse<JobOpportunityCommandResponse> { Data = result });
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
    
    /// <summary>
    /// Delete job opportunity
    /// </summary>
    /// <param name="command">Delete job command</param>
    /// <returns>No content status code</returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteJobOpportunityAsync([FromRoute] DeleteJobOpportunityCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}