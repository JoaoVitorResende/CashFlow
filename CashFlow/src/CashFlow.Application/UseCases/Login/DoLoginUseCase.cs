using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Infrastucture.Tokens;
using CashFlow.Communication.Requests;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Login;
public class DoLoginUseCase :IDoLoginUseCase
{
    private readonly IUsersReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAcessTokenGenarator _accessTokenGenerator;

    public DoLoginUseCase(IUsersReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        IAcessTokenGenarator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmail(request.Email);
        if( user is null)
            throw new InvalidLoginException();

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if(!passwordMatch)
            throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}
