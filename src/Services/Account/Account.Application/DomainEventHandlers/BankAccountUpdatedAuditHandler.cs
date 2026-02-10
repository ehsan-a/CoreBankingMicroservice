//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Account.Application.EventHandlers
//{
//    public class AccountUpdatedAuditHandler : INotificationHandler<AccountUpdatedEvent>
//    {
//        private readonly IAuditLogService _auditLogService;
//        private readonly IMapper _mapper;

//        public AccountUpdatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
//        {
//            _auditLogService = auditLogService;
//            _mapper = mapper;
//        }

//        public async Task Handle(AccountUpdatedEvent domainEvent, CancellationToken cancellationToken)
//        {
//            var auditLog = _mapper.Map<AuditLog>(domainEvent.Account);
//            auditLog.ActionType = AuditActionType.Update;
//            auditLog.PerformedBy = domainEvent.UserId.ToString();
//            auditLog.OldValue = domainEvent.OldValue;
//            await _auditLogService.LogAsync(auditLog);
//        }
//    }
//}
