using Account.Application.Commands;
using Account.Application.DTOs;
using AutoMapper;

namespace Account.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Domain.Aggregates.AccountAggregate.Account, AccountResponseDto>();
   
            CreateMap<CreateAccountRequestDto, CreateAccountCommand>();
            CreateMap<UpdateAccountRequestDto, UpdateAccountCommand>();
        }
    }
}
