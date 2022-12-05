using System.Globalization;
using AutoMapper;
using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;
using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Domain.Enums;
using Address = JobOpportunityMicroservice.Domain.Address;

namespace JobOpportunityMicroservice.Application.Mapper;

public class JobAdvertisementProfile : Profile
{
    private const string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
    
    public JobAdvertisementProfile()
    {
        CreateMap<AddJobOpportunityCommand, JobAdvertisement>()
            .ForMember(
                dest => dest.Benefit, 
                opt => opt.MapFrom(src => src.Benefit))
            .ForMember(
                dest => dest.Company, 
                opt => opt.MapFrom(src => src.Company))
            .ForMember(
                dest => dest.Description, 
                opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.Email, 
                opt => opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.Modality, 
                opt => opt.MapFrom(src => Enum.Parse<Modality>(src.Modality)))
            .ForMember(
                dest => dest.Requerements, 
                opt => opt.MapFrom(src => src.Requerements))
            .ForMember(
                dest => dest.Title, 
                opt => opt.MapFrom(src => src.Title))
            .ForMember(
                dest => dest.DateLimit, 
                opt => opt.MapFrom(src => DateTime.ParseExact(src.DateLimit, DATETIME_FORMAT, CultureInfo.InvariantCulture)))
            .ForMember(
                dest => dest.MonthlyHours, 
                opt => opt.MapFrom(src => src.MonthlyHours))
            .ForMember(
                dest => dest.PhoneNumber, 
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.MaxPayRange, 
                opt => opt.MapFrom(src => src.MaxPayRange))
            .ForMember(
                dest => dest.MinPayRange, 
                opt => opt.MapFrom(src => src.MinPayRange))
            .ForMember(
                dest => dest.Address, 
                opt => opt.MapFrom(src => new Address
                {
                    Id = default,
                    City = src.Address.City,
                    Country = src.Address.Country,
                    District = src.Address.District,
                    State = src.Address.State,
                    Street = src.Address.Street,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    JobAdvertisementId = default
                }))
            .ForMember(
                dest => dest.Link, 
                opt => opt.MapFrom(src => src.Link))
            .ForMember(
                dest => dest.CreatedAt, 
                opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(
                dest => dest.UpdateAt, 
                opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(
                dest => dest.IsActive, 
                opt => opt.MapFrom(src => true))
            .ForMember(
                dest => dest.UserId, 
                opt => opt.MapFrom(src => default(Guid)))
            .ForMember(
                dest => dest.Id, 
                opt => opt.MapFrom(src => default(Guid)));

        CreateMap<JobAdvertisement, JobOpportunityCommandResponse>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Address,
                opt => opt.MapFrom(src => new Commands.JobOpportunity.AddressResponse()
                {
                    Id = src.Address.Id,
                    City = src.Address.City,
                    Country = src.Address.Country,
                    District = src.Address.District,
                    State = src.Address.State,
                    Street = src.Address.Street,
                    CreatedAt = src.Address.CreatedAt!.Value.ToString(DATETIME_FORMAT),
                    UpdatedAt = src.Address.UpdateAt!.Value.ToString(DATETIME_FORMAT)
                }))
            .ForMember(
                dest => dest.Benefit,
                opt => opt.MapFrom(src => src.Benefit))
            .ForMember(
                dest => dest.Categories,
                opt => opt.MapFrom(src => src.JobCategories.Select(jc => jc.Category.Name)))
            .ForMember(
                dest => dest.Company,
                opt => opt.MapFrom(src => src.Company))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.Link,
                opt => opt.MapFrom(src => src.Link))
            .ForMember(
                dest => dest.Modality,
                opt => opt.MapFrom(src => src.Modality.ToString()))
            .ForMember(
                dest => dest.Requerements,
                opt => opt.MapFrom(src => src.Requerements))
            .ForMember(
                dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
            .ForMember(
                dest => dest.DateLimit,
                opt => opt.MapFrom(src => src.DateLimit!.Value.ToString(DATETIME_FORMAT)))
            .ForMember(
                dest => dest.IsActive,
                opt => opt.MapFrom(src => src.IsActive))
            .ForMember(
                dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.MonthlyHours,
                opt => opt.MapFrom(src => src.MonthlyHours))
            .ForMember(
                dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(
                dest => dest.MaxPayRange,
                opt => opt.MapFrom(src => src.MaxPayRange))
            .ForMember(
                dest => dest.MinPayRange,
                opt => opt.MapFrom(src => src.MinPayRange))
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt!.Value.ToString(DATETIME_FORMAT)))
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => src.UpdateAt!.Value.ToString(DATETIME_FORMAT)));
        
        CreateMap<UpdateJobOpportunityCommand, JobAdvertisement>()
            .ForMember(
                dest => dest.Benefit, 
                opt => opt.MapFrom(src => src.Benefit))
            .ForMember(
                dest => dest.Company, 
                opt => opt.MapFrom(src => src.Company))
            .ForMember(
                dest => dest.Description, 
                opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.Email, 
                opt => opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.Modality, 
                opt => opt.MapFrom(src => Enum.Parse<Modality>(src.Modality)))
            .ForMember(
                dest => dest.Requerements, 
                opt => opt.MapFrom(src => src.Requerements))
            .ForMember(
                dest => dest.Title, 
                opt => opt.MapFrom(src => src.Title))
            .ForMember(
                dest => dest.DateLimit, 
                opt => opt.MapFrom(src => DateTime.ParseExact(src.DateLimit, DATETIME_FORMAT, CultureInfo.InvariantCulture)))
            .ForMember(
                dest => dest.MonthlyHours, 
                opt => opt.MapFrom(src => src.MonthlyHours))
            .ForMember(
                dest => dest.PhoneNumber, 
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.MaxPayRange, 
                opt => opt.MapFrom(src => src.MaxPayRange))
            .ForMember(
                dest => dest.MinPayRange, 
                opt => opt.MapFrom(src => src.MinPayRange))
            .ForMember(
                dest => dest.Address, 
                opt => opt.MapFrom(src => new Address
                {
                    Id = Guid.Parse(src.Address.Id),
                    City = src.Address.City,
                    Country = src.Address.Country,
                    District = src.Address.District,
                    State = src.Address.State,
                    Street = src.Address.Street,
                    UpdateAt = null,
                    JobAdvertisementId = Guid.Parse(src.Id)
                }))
            .ForMember(
                dest => dest.Link, 
                opt => opt.MapFrom(src => src.Link))
            .ForMember(
                dest => dest.UpdateAt, 
                opt => opt.MapFrom(src => (DateTime?) null))
            .ForMember(
                dest => dest.CreatedAt, 
                opt => opt.MapFrom(src => (DateTime?) null))
            .ForMember(
                dest => dest.IsActive, 
                opt => opt.MapFrom(src => src.IsActive))
            .ForMember(
                dest => dest.Id, 
                opt => opt.MapFrom(src => Guid.Parse(src.Id)));
    }
}