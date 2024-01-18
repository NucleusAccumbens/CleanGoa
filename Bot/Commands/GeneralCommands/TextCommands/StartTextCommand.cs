using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Messages.GeneralMessage;

namespace Bot.Commands.GeneralCommands.TextCommands;

internal class StartTextCommand : BaseTextCommand
{
    private StartMessage _startMessage;
    
    private readonly LanguageMessage _languageMessage = new();

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly ICreateTlgUserCommand _createTlgUserCommand;

    public StartTextCommand(ICheckUserIsInDbQuery checkUserIsInDbQuery, 
        IGetUserLanguageQuery getUserLanguageQuery, ICreateTlgUserCommand createTlgUserCommand)
    {
        _checkUserIsInDbQuery= checkUserIsInDbQuery;
        _getUserLanguageQuery = getUserLanguageQuery;
        _createTlgUserCommand = createTlgUserCommand;
    }
    
    public override string Name => "/start";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {         
            long chatId = update.Message.Chat.Id;

            bool oldUser = await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId);

            if (!oldUser) { await _languageMessage.GetMessage(chatId, client); }

            else
            {
                var language = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

                _startMessage = new(language);

                await _startMessage.SendMessage(chatId, client);
            }
        }
    }
}
