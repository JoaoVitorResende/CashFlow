using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }
    private void RequestToEntity()
    {
        CreateMap<RequestRegisterExpenseJson, Expense>();
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(entityUser => entityUser.Password, config => config.Ignore());
    }
    private void EntityToResponse()
    {
        CreateMap<Expense, RequestRegisterExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<User, ResponseRegisteredUserJson>();
    }
}
