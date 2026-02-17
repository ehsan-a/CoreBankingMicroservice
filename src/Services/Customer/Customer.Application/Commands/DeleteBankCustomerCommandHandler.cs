using Customer.Application.Specifications;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;

namespace Customer.Application.Commands
{
    public class DeleteBankCustomerCommandHandler : ICommandHandler<DeleteBankCustomerCommand>
    {
        private readonly IBankCustomerRepository _bankCustomerRepository;

        public DeleteBankCustomerCommandHandler(IBankCustomerRepository bankCustomerRepository)
        {
            _bankCustomerRepository = bankCustomerRepository;
        }

        public async Task Handle(DeleteBankCustomerCommand request, CancellationToken cancellationToken)
        {
            var spec = new BankCustomerGetAllSpec();
            var item = await _bankCustomerRepository.GetByIdAsync(request.Id, spec, cancellationToken)
            ?? throw new NotFoundException("Customer Not Found");

            item.Delete(request.UserId);

            await _bankCustomerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
