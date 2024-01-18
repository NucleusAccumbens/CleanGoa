using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Bot.Messages.GeneralMessage;
using Domain.Enums;

namespace Bot.Commands.GeneralCommands.CallbackCommands;

public class LanguageCallbackCommand : BaseCallbackCommand
{
    private StartMessage _startMessage;

    private readonly ICreateTlgUserCommand _createTlgUserCommand;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    private readonly IChangeLanguageCommand _changeLanguageCommand;

    public LanguageCallbackCommand(ICreateTlgUserCommand createTlgUserCommand, 
        ICheckUserIsInDbQuery checkUserIsInDbQuery, IChangeLanguageCommand changeLanguageCommand)
    {
        _createTlgUserCommand = createTlgUserCommand;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
        _changeLanguageCommand = changeLanguageCommand;
    }

    public override char CallbackDataCode => '~';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string? username = update.CallbackQuery.Message.Chat.Username;

            string callbackId = update.CallbackQuery.Id;

            bool oldUser = await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId);

            if (update.CallbackQuery.Data == "~🇷🇺")
            {
                if (!oldUser)
                {
                    await CreateTlgUser(chatId, username, Language.Russian);

                    _startMessage = new(Language.Russian);

                    await _startMessage.EditMessage(chatId, messageId, client);
                }
                if (oldUser)
                {
                    await _changeLanguageCommand.ChangeLanguageAsync(chatId, Language.Russian);

                    await ShowAllert(callbackId, client, Language.Russian);
                }

                return;
            }
            if (update.CallbackQuery.Data == "~🇬🇧")
            {
                if (!oldUser)
                {
                    await CreateTlgUser(chatId, username, Language.English);

                    _startMessage = new(Language.English);

                    await _startMessage.EditMessage(chatId, messageId, client);
                }
                if (oldUser)
                {
                    await _changeLanguageCommand.ChangeLanguageAsync(chatId, Language.English);

                    await ShowAllert(callbackId, client, Language.English);
                }
            }
        }
    }

    private async Task CreateTlgUser(long chatId, string username, Language language)
    {
        await _createTlgUserCommand.CreateTlgUserAsync(new Domain.Entities.TlgUser()
        {
            ChatId = chatId,
            Username = username,
            Language = language
        });
    }

    private static async Task ShowAllert(string callbackId, ITelegramBotClient client, Language language)
    {
        if (language == Language.Russian)
        {
            await MessageService.ShowAllert(callbackId, client, "Язык изменён 👌");
        }
        if (language == Language.English)
        {
            await MessageService.ShowAllert(callbackId, client, "Language changed 👌");
        }
    }
}
