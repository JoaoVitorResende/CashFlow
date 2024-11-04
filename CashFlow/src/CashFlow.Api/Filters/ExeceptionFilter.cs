using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace CashFlow.Api.Filters;
public class ExeceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectExcepetion(context);
        }
        else
        {
            ThrowUnknowError(context);
        }
    }
    private void HandleProjectExcepetion(ExceptionContext context)
    {
        var cashFlowException = context.Exception as CashFlowException;
        var errorResponse = new ResponseErrorJson(cashFlowException!.GetErros());
        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }
    private void ThrowUnknowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
