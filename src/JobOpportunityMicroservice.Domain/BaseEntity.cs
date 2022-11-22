﻿namespace JobOpportunityMicroservice.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdateAt { get; set; }
}