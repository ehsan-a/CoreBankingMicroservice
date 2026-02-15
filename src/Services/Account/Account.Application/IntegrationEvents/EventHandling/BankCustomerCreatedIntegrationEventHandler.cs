using Account.Application.IntegrationEvents.Events;
using Account.Domain.Replicas;
using Shared.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.IntegrationEvents.EventHandling
{
    public class BankCustomerCreatedIntegrationEventHandler : IIntegrationEventHandler<BankCustomerCreatedIntegrationEvent>
    {
        private readonly ICustomerReplicaRepository _customerReplicaRepository;

        public BankCustomerCreatedIntegrationEventHandler(ICustomerReplicaRepository customerReplicaRepository)
        {
            _customerReplicaRepository = customerReplicaRepository;
        }

        public async Task Handle(BankCustomerCreatedIntegrationEvent @event)
        {
            var customer = new CustomerReplica
            {
                Id = @event.BankCustomerResponseDto.Id,
                NationalCode = @event.BankCustomerResponseDto.NationalCode,
                Status = CustomerStatus.Active
            };

            await _customerReplicaRepository.AddAsync(customer);
            await _customerReplicaRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
