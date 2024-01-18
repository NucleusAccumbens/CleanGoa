using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Bot.Messages.StopBurningMessages;
using Bot.Models;
using Domain.Enums;

namespace Bot.Commands.StopBurningCommands.TextCommands;

public class ComplainTextCommand : BaseTextCommand
{
    private readonly InlineKeyboardMarkup _englishMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "📤 Send", callbackData: ">"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "aComplain"),
        },
    });

    private readonly InlineKeyboardMarkup _russianMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "📤 Отправить", callbackData: ">"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "aComplain"),
        },
    });

    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private PhotoMessage _photoMessage;

    public ComplainTextCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
    }
    
    public override string Name => "body";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            int messageId = _memoryCachService.GetMessageIdFromMemoryCach(chatId);

            string body = update.Message.Text;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            _memoryCachService.SetMemoryCach(chatId, new Mail(body));

            _memoryCachService.SetMemoryCach(chatId, "photo");

            await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

            _photoMessage = new(lang, body);

            await _photoMessage.EditMessage(chatId, messageId, client);
        }
    }

    private async Task EditMessage(long chatId, int messageId, 
        ITelegramBotClient client, Language? language, string body)
    {
        if (language == Language.English) 
        {
            await MessageService.EditMessage(chatId, messageId, client, 
                "<b>check the information in the letter and confirm sending. " +
                "to make changes go back to the previous step</b>\n\n" +
                $"{body}", _englishMarkup);
        }
        else
        {
            await MessageService.EditMessage(chatId, messageId, client,
                "Проверь информацию в письме и подтверди отправку. " +
                "Чтобы внести изменения, вернись к предыдущему шагу.\n\n" +
                $"{body}", _russianMarkup);
        }
    }
}
