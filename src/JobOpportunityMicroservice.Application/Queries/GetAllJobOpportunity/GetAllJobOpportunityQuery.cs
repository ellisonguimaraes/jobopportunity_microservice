using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Infra.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Queries.GetAllJobOpportunity;

public record GetAllJobOpportunityQuery : IRequest<PagedList<JobOpportunityCommandResponse>>
{
    [BindProperty(Name = "page_number")]
    public int? PageNumber  { get; set; }
    
    [BindProperty(Name = "page_size")]
    public int? PageSize { get; set; }
}