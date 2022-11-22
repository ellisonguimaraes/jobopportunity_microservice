using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace JobOpportunityMicroservice.Application.Models;

public class PaginationParametersRequest
{
    [BindProperty(Name = "page_number")]
    public int? PageNumber  { get; set; }
    
    [BindProperty(Name = "page_size")]
    public int? PageSize { get; set; }
}