//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Account.Application.EventHandlers
//{
//    public class AccountCreatedAuditHandler : INotificationHandler<AccountCreatedEvent>
//    {
//        private readonly IAuditLogService _auditLogService;
//        private readonly IMapper _mapper;

//        public AccountCreatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
//        {
//            _auditLogService = auditLogService;
//            _mapper = mapper;
//        }

//        public async Task Handle(AccountCreatedEvent domainEvent, CancellationToken cancellationToken)
//        {
//            var auditLog = _mapper.Map<AuditLog>(domainEvent.Account);
//            auditLog.ActionType = AuditActionType.Create;
//            auditLog.PerformedBy = domainEvent.UserId.ToString();
//            await _auditLogService.LogAsync(auditLog);
//        }
//    }
//}
