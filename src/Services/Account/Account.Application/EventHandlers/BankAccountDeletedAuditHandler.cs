//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Account.Application.EventHandlers
//{
//    public class AccountDeletedAuditHandler : INotificationHandler<AccountDeletedEvent>
//    {
//        private readonly IAuditLogService _auditLogService;
//        private readonly IMapper _mapper;

//        public AccountDeletedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
//        {
//            _auditLogService = auditLogService;
//            _mapper = mapper;
//        }

//        public async Task Handle(AccountDeletedEvent domainEvent, CancellationToken cancellationToken)
//        {
//            var auditLog = _mapper.Map<AuditLog>(domainEvent.Account);
//            auditLog.ActionType = AuditActionType.Delete;
//            auditLog.PerformedBy = domainEvent.UserId.ToString();
//            await _auditLogService.LogAsync(auditLog);
//        }
//    }
//}
