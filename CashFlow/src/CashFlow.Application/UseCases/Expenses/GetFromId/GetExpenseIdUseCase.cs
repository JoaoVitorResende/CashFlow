using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetFromId;
public class GetExpenseIdUseCase: IGetExpenseIdUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IMapper _mapper;
    public GetExpenseIdUseCase(IExpensesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpensesJson> Execute(long id)
    {
        var result = await _repository.GetById(id);
        if (result is null)
        {
            throw new NotFoundExeception(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        return _mapper.Map<ResponseExpensesJson>(result);
    }
}
