using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Messages.GeneralMessage;

namespace Bot.Commands.GeneralCommands.CallbackCommands;

public class MainMenuCallbackCommand : BaseCallbackCommand
{
    private StartMessage _startMessage;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public MainMenuCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public override char CallbackDataCode => '<';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            _startMessage = new(lang);

            await _startMessage.EditMessage(chatId, messageId, client);
        }
    }
}
