﻿using Bot.Common.Abstractions;
using Bot.Common.Services;

namespace Bot.Commands.GeneralCommands.CallbackCommands;

public class HideCallbackCommand : BaseCallbackCommand
{
    public override char CallbackDataCode => '-';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await MessageService.DeleteMessage(chatId, messageId, client);
        }
    }
}
