using Account.Application.Commands;
using Account.Application.DTOs;
using Account.Domain.Aggregates.BankAccountAggregate;
using AutoMapper;

namespace Account.Application.Mappings
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<BankAccount, BankAccountResponseDto>();

            CreateMap<CreateBankAccountRequestDto, CreateBankAccountCommand>();
            CreateMap<UpdateBankAccountRequestDto, UpdateBankAccountCommand>();
        }
    }
}
