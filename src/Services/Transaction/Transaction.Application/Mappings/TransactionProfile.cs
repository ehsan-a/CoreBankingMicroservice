using AutoMapper;
using Transaction.Application.Commands;
using Transaction.Application.DTOs;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Application.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<AccountTransaction, TransactionResponseDto>();
            CreateMap<CreateTransactionRequestDto, AccountTransaction>();

            CreateMap<CreateTransactionCommand, AccountTransaction>();
            CreateMap<CreateTransactionRequestDto, CreateTransactionCommand>();
        }
    }
}
