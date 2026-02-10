using Account.Application.Commands;
using Account.Application.DTOs;
using Account.Application.Interfaces;
using Account.Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Application.Extensions;
using Shared.ServiceDefaults;
using System.Security.Claims;

namespace Account.Application.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BankAccountService> _logger;

        public BankAccountService(IMapper mapper, IMediator mediator, ILogger<BankAccountService> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(CreateBankAccountRequest request, Guid requestId, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            //var command = _mapper.Map<CreateBankAccountCommand>(dto);
            //command.UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            ////command.UserId = principal.GetUserId();
            //return await _mediator.Send(command);

            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
            {
                var command = _mapper.Map<CreateBankAccountCommand>(request);
                command.UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
                //command.UserId = principal.GetUserId();

                var requestCreateOrder = new IdentifiedCommand<CreateBankAccountCommand, bool>(command, requestId);

                _logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateOrder.GetGenericTypeName(),
                    nameof(requestCreateOrder.Id),
                    requestCreateOrder.Id,
                    requestCreateOrder);

                var result = await _mediator.Send(requestCreateOrder);

                if (result)
                {
                    _logger.LogInformation("CreateOrderCommand succeeded - RequestId: {RequestId}", requestId);
                }
                else
                {
                    _logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", requestId);
                }

                return result;
            }
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = new DeleteBankAccountCommand { Id = id, UserId = principal.GetUserId() };
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<BankAccountResponseDto>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllBankAccountsQuery { Limit = limit, Offset = offset });
        }

        public async Task<BankAccountResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBankAccountByIdQuery { Id = id });

        }

        public async Task UpdateAsync(UpdateBankAccountRequest request, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateBankAccountCommand>(request);
            command.UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            //command.UserId = principal.GetUserId();
            await _mediator.Send(command);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBankAccountExistsQuery { Id = id });
        }

    }
}
