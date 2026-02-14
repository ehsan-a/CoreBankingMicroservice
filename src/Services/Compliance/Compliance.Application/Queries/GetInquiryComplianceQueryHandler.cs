using AutoMapper;
using Compliance.Application.DTOs;
using Compliance.Application.Interfaces;
using Compliance.Application.Specifications;
using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;

namespace Compliance.Application.Queries
{
    public class GetInquiryComplianceQueryHandler : IQueryHandler<GetInquiryComplianceQuery, ComplianceResponseDto>
    {
        private readonly IComplianceRepository _complianceRepository;
        private readonly IMapper _mapper;
        private readonly ICivilRegistryService _civilRegistryService;
        private readonly IPoliceClearanceService _policeClearanceService;
        private readonly ICentralBankCreditCheckService _centralBankCreditCheckService;

        public GetInquiryComplianceQueryHandler(IComplianceRepository complianceRepository, IMapper mapper, ICivilRegistryService civilRegistryService, IPoliceClearanceService policeClearanceService, ICentralBankCreditCheckService centralBankCreditCheckService)
        {
            _complianceRepository = complianceRepository;
            _mapper = mapper;
            _civilRegistryService = civilRegistryService;
            _policeClearanceService = policeClearanceService;
            _centralBankCreditCheckService = centralBankCreditCheckService;
        }

        public async Task<ComplianceResponseDto> Handle(GetInquiryComplianceQuery request, CancellationToken cancellationToken)
        {
            var person = await GetCivilRegistryAsync(request.NationalCode);
            var spec = new ComplianceGetAllSpec();
            var compliance = await _complianceRepository.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
            if (person == null) throw new NotFoundException("Person", request.NationalCode);
            return new ComplianceResponseDto
            {
                CivilRegistry = await GetCivilRegistryAsync(request.NationalCode),
                CentralBankCreditCheck = await GetCentralBankCreditCheckAsync(request.NationalCode),
                PoliceClearance = await GetPoliceClearanceAsync(request.NationalCode),
                RegisteredCompliance = _mapper.Map<RegisteredComplianceResponseDto>(compliance)
            };
        }

        public async Task<CivilRegistryResponseDto?> GetCivilRegistryAsync(string nationalCode)
        => await _civilRegistryService.GetPersonInfoAsync(nationalCode);
        public async Task<PoliceClearanceResponseDto?> GetPoliceClearanceAsync(string nationalCode)
        => await _policeClearanceService.GetResultInfoAsync(nationalCode);
        public async Task<CentralBankCreditCheckResponseDto?> GetCentralBankCreditCheckAsync(string nationalCode)
        => await _centralBankCreditCheckService.GetResultInfoAsync(nationalCode);
    }
}
