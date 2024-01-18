using Application.Events.Commands;
using Application.Events.Interfaces;
using Application.Events.Queries;
using Application.TlgUsers.Commands;
using Application.TlgUsers.Queries;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IKickTlgUserCommand, KickTlgUserCommand>();
        services.AddScoped<ICheckUserIsInDbQuery, CheckUserIsInDbQuery>();
        services.AddScoped<IGetUserLanguageQuery, GetUserLanguageQuery>();
        services.AddScoped<ICreateTlgUserCommand, CreateTlgUserCommand>();
        services.AddScoped<IChangeLanguageCommand, ChangeLanguageCommand>();
        services.AddScoped<ICreateEventCommand, CreateEventCommand>();
        services.AddScoped<IGetEventsQuery, GetEventsQuery>(); 
        services.AddScoped<IGetTlgUsersQuery, GetTlgUsersQuery>();
        
        return services;
    }
}

