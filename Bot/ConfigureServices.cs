using Bot.Commands.EventsCommands.EventsCallbackCommands;
using Bot.Commands.EventsCommands.EventsTextCommands;
using Bot.Commands.GeneralCommands.CallbackCommands;
using Bot.Commands.GeneralCommands.TextCommands;
using Bot.Commands.StopBurningCommands.CallbackCommands;
using Bot.Commands.StopBurningCommands.TextCommands;
using Bot.Common;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureService
{
    public static IServiceCollection AddTelegramBotServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<TelegramBot>();
        services.AddScoped<IMemoryCachService, MemoryCachService>();
        services.AddScoped<ICommandAnalyzer, CommandAnalyzer>();
        services.AddScoped<BaseTextCommand, StartTextCommand>();
        services.AddScoped<BaseTextCommand, ComplainTextCommand>();
        services.AddScoped<BaseTextCommand, LanguageTextCommand>();
        services.AddScoped<BaseTextCommand, PhotoTextCommand>();
        services.AddScoped<BaseCallbackCommand, LanguageCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, StopBurningCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, MainMenuCallbackCommand>(); 
        services.AddScoped<BaseCallbackCommand, HideCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, SendCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, EventCallbackCommand>();
        services.AddScoped<BaseTextCommand, DateTextCommand>();
        services.AddScoped<BaseTextCommand, DescriptTextCommand>();
        services.AddScoped<BaseTextCommand, DescriptTextCommand>();
        services.AddScoped<BaseCallbackCommand, InfoCallbackCommand>();

        return services;
    }
}
