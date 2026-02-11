using AutoMapper;
using Customer.Application.DTOs;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;

namespace Customer.Application.Commands
{
    public class CreateBankCustomerCommandHandler : ICommandHandler<CreateBankCustomerCommand, BankCustomerResponseDto>
    {
        private readonly IBankCustomerRepository _bankCustomerRepository;
        //private readonly IComplianceRepository _complianceRepository;
        private readonly IMapper _mapper;

        public CreateBankCustomerCommandHandler(
            IBankCustomerRepository bankCustomerRepository,
            //IComplianceRepository complianceRepository,
            IMapper mapper)
        {
            _bankCustomerRepository = bankCustomerRepository;
            //_complianceRepository = complianceRepository;
            _mapper = mapper;
        }

        public async Task<BankCustomerResponseDto> Handle(CreateBankCustomerCommand request, CancellationToken cancellationToken)
        {
            var bankCustomer = BankCustomer.Create(
                request.NationalCode,
                request.FirstName,
                request.LastName,
                request.UserId);

            //var spec = new AuthenticationGetAllSpec();
            //var authenticationResult = await _authenticationRepository.GetByNationalCodeAsync(customer.NationalCode, spec, cancellationToken);

            if (await _bankCustomerRepository.ExistsByNationalCodeAsync(bankCustomer.NationalCode, cancellationToken))
                throw new ConflictException("Customer already exists.");
            //if (authenticationResult == null)
            //    throw new UnauthorizedAccessException("Customer Authentication Not Found.");
            //if (!authenticationResult.CentralBankCreditCheckPassed || !authenticationResult.CivilRegistryVerified || !authenticationResult.PoliceClearancePassed)
            //    throw new UnauthorizedAccessException("Customer authentication result rejected.");

            await _bankCustomerRepository.AddAsync(bankCustomer, cancellationToken);

            await _bankCustomerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return _mapper.Map<BankCustomerResponseDto>(bankCustomer);
        }
    }
}
