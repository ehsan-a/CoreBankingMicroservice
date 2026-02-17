using Account.Application.IntegrationEvents.Events;
using Account.Application.Specifications;
using Account.Domain.Replicas;
using Shared.Application.Exceptions;
using Shared.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.IntegrationEvents.EventHandling
{
    public class BankCustomerUpdatedIntegrationEventHandler : IIntegrationEventHandler<BankCustomerUpdatedIntegrationEvent>
    {
        private readonly ICustomerReplicaRepository _customerReplicaRepository;

        public BankCustomerUpdatedIntegrationEventHandler(ICustomerReplicaRepository customerReplicaRepository)
        {
            _customerReplicaRepository = customerReplicaRepository;
        }

        public async Task Handle(BankCustomerUpdatedIntegrationEvent @event)
        {
            var spec = new CustomerReplicaGetAllSpec();
            var item = await _customerReplicaRepository.GetByIdAsync(@event.BankCustomerResponseDto.Id, spec)
                ?? throw new NotFoundException("Customer Not Found!");

            item.Status=
        }
    }
}
