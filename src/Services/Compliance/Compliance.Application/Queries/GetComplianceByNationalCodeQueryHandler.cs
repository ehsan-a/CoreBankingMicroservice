using AutoMapper;
using Compliance.Application.DTOs;
using Compliance.Application.Specifications;
using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Shared.Application.Interfaces;

namespace Compliance.Application.Queries
{
    public class GetComplianceByNationalCodeQueryHandler : IQueryHandler<GetComplianceByNationalCodeQuery, ComplianceResponseDto>
    {
        private readonly IComplianceRepository _complianceRepository;
        private readonly IMapper _mapper;

        public GetComplianceByNationalCodeQueryHandler(IComplianceRepository complianceRepository, IMapper mapper)
        {
            _complianceRepository = complianceRepository;
            _mapper = mapper;
        }

        public async Task<ComplianceResponseDto> Handle(GetComplianceByNationalCodeQuery request, CancellationToken cancellationToken)
        {
            var spec = new ComplianceGetAllSpec();
            var compliance = await _complianceRepository.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
            return _mapper.Map<ComplianceResponseDto>(compliance);
        }
    }
}
