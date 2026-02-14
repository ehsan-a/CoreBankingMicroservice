using AutoMapper;
using Compliance.Application.DTOs;
using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;

namespace Compliance.Application.Commands
{
    public class CreateComplianceCommandHandler : ICommandHandler<CreateComplianceCommand, RegisteredComplianceResponseDto>
    {
        private readonly IComplianceRepository _complianceRepository;
        private readonly IMapper _mapper;

        public CreateComplianceCommandHandler(IComplianceRepository complianceRepository, IMapper mapper)
        {
            _complianceRepository = complianceRepository;
            _mapper = mapper;
        }

        public async Task<RegisteredComplianceResponseDto> Handle(CreateComplianceCommand request, CancellationToken cancellationToken)
        {
            var complianceExists = await _complianceRepository.ExistsByNationalCodeAsync(request.NationalCode, cancellationToken);

            if (complianceExists)
            {
                throw new ConflictException("Authentication already exists.");
            }
            var compliance = BankCompliance.Create(
                request.NationalCode,
                request.CentralBankCreditCheckPassed,
                request.CivilRegistryVerified,
                request.PoliceClearancePassed,
                request.UserId);

            await _complianceRepository.AddAsync(compliance, cancellationToken);

            await _complianceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return _mapper.Map<RegisteredComplianceResponseDto>(compliance);
        }
    }
}
