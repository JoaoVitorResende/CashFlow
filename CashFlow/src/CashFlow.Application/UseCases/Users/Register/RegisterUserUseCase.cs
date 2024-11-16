using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Infrastucture.Tokens;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IPasswordEncripter _passwordEncripeter;
    private readonly IMapper _mapper;
    private readonly IUsersReadOnlyRepository _userReadOnlyRepository;
    private readonly IUsersWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAcessTokenGenarator _tokenGenarator;
    
    public RegisterUserUseCase(IPasswordEncripter passwordEncripeter,IMapper mapper, IUsersReadOnlyRepository userReadOnlyRepository
        , IUsersWriteOnlyRepository userWriteOnlyRepository, IUnitOfWork unitOfWork, IAcessTokenGenarator tokenGenarator)
    {
        _passwordEncripeter = passwordEncripeter;
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _tokenGenarator = tokenGenarator;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripeter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _tokenGenarator.Generate(user)
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
