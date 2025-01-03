﻿using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Infrastucture.DataAccess;
using CashFlow.Infrastucture.DataAccess.Repositories;
using CashFlow.Infrastucture.Security.CryptographyPassWord;
using CashFlow.Infrastucture.Security.Token;
using CashFlow.Infrastucture.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastucture;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddToken(services, configuration);
        services.AddScoped<IPasswordEncripter, Cryptography>();
    }
    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingkey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        services.AddScoped<IAcessTokenGenarator>(config => new JwtTokenGenarator(expirationTimeMinutes, signingkey!));
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IUsersReadOnlyRepository, UserRepository>();
        services.AddScoped<IUsersWriteOnlyRepository, UserRepository>();
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var version = new Version(8, 0, 35);
        var serverVersion = new MySqlServerVersion(version);
        services.AddDbContext<CashFlowDbContex>(config => config.UseMySql(connectionString, serverVersion));
    }
}
