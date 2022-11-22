using System.Text.Json.Serialization;

namespace JobOpportunityMicroservice.Application.Models;

public class CategoryResponse : BaseResponse
{
    [JsonPropertyName("category_name")]
    public string Name { get; set; }
}