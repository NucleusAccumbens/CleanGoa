using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Messages.GeneralMessage;
using Bot.Messages.GeneralMessages;
using Domain.Enums;

namespace Bot.Commands.GeneralCommands.CallbackCommands;

public class InfoCallbackCommand : BaseCallbackCommand
{
    private InfoMessage _infoMessage;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public InfoCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public override char CallbackDataCode => 'i';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            _infoMessage = new(lang);

            await _infoMessage.EditMessage(chatId, messageId, client);
        }
    }
}
