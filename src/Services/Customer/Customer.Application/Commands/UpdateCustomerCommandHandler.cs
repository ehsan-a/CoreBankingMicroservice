using AutoMapper;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;
using System.Text.Json;

namespace Customer.Application.Commands
{
    public class UpdateBankCustomerCommandHandler : ICommandHandler<UpdateBankCustomerCommand>
    {
        private readonly IBankCustomerRepository _bankCustomerRepository;
        private readonly IMapper _mapper;

        public UpdateBankCustomerCommandHandler(IBankCustomerRepository bankCustomerRepository, IMapper mapper)
        {
            _bankCustomerRepository = bankCustomerRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBankCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _bankCustomerRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
            if (customer == null) throw new NotFoundException("");

            _mapper.Map(request, customer);

            var oldAccount = await _bankCustomerRepository
              .GetByIdAsNoTrackingAsync(customer.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _bankCustomerRepository.Update(customer);

            BankCustomer.Update(customer, request.UserId, oldValue);

            await _bankCustomerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
