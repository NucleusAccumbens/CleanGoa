using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Bot.Messages.GeneralMessage;
using Bot.Services;

namespace Bot.Commands.StopBurningCommands.CallbackCommands;

public class SendCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private StartMessage _startMessage;

    public SendCallbackCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public override char CallbackDataCode => '>';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            var mail = _memoryCachService.GetMailFromMemoryCach(chatId);

            if (update.CallbackQuery.Data == ">!")
            {
                _memoryCachService.SetMemoryCach(chatId, "body");
                
                await MessageService.DeleteMessage(chatId, messageId, client);

                _startMessage = new(lang);

                await _startMessage.SendMessage(chatId, client);

                return;
            }
            if (update.CallbackQuery.Data == ">")
            {
                await MailService.SendEmailAsync(mail);

                if (lang == Domain.Enums.Language.English)
                {
                    await MessageService.ShowAllert(callbackId, client, "Message sended 👌");
                }
                else await MessageService.ShowAllert(callbackId, client, "Сообщение отправлено 👌");

                await MessageService.DeleteMessage(chatId, messageId, client);

                _startMessage = new(lang);

                await _startMessage.SendMessage(chatId, client);
            }

        }
    }
}
