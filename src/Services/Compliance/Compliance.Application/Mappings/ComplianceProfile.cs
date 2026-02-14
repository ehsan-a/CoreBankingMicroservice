using AutoMapper;
using Compliance.Application.Commands;
using Compliance.Application.DTOs;
using Compliance.Domain.Aggregates.BankComplinceAggregate;

namespace Compliance.Application.Mappings
{
    public class ComplianceProfile : Profile
    {
        public ComplianceProfile()
        {
            CreateMap<BankCompliance, RegisteredComplianceResponseDto>();
            CreateMap<CreateComplianceRequest, BankCompliance>();

            CreateMap<CreateComplianceCommand, BankCompliance>();
            CreateMap<CreateComplianceRequest, CreateComplianceCommand>();
        }
    }
}
