using AutoMapper;
using Customer.Application.Commands;
using Customer.Application.DTOs;
using Customer.Domain.Aggregates.BankCustomerAggregate;

namespace Customer.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<BankCustomer, BankCustomerResponseDto>();
            CreateMap<CreateBankCustomerRequest, BankCustomer>();
            CreateMap<UpdateBankCustomerRequest, BankCustomer>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateBankCustomerCommand, BankCustomer>();
            CreateMap<UpdateBankCustomerCommand, BankCustomer>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateBankCustomerRequest, CreateBankCustomerCommand>();
            CreateMap<UpdateBankCustomerRequest, UpdateBankCustomerCommand>();
        }
    }
}
