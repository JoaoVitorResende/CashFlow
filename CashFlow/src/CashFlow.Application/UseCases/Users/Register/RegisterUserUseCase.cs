using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IPasswordEncripter _passwordEncripeter;
    private readonly IMapper _mapper;
    private readonly IUsersReadOnlyRepository _userReadOnlyRepository;
    public RegisterUserUseCase(IPasswordEncripter passwordEncripeter,IMapper mapper, IUsersReadOnlyRepository userReadOnlyRepository)
    {
        _passwordEncripeter = passwordEncripeter;
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripeter.Encrypt(request.Password);
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }
    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);
        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if(emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty,ResourceErrorMessages.EMAILS_EXISTS));
        }
        if (!result.IsValid)
        {
            var errosMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errosMessages);
        }
    }
}
