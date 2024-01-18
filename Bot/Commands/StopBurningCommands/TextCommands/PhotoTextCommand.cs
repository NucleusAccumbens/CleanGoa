using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Domain.Enums;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

namespace Bot.Commands.StopBurningCommands.TextCommands;

internal class PhotoTextCommand : BaseTextCommand
{
    private readonly InlineKeyboardMarkup _englishMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "📤 Send", callbackData: ">"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: ">!"),
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
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: ">!"),
        },
    });

    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public PhotoTextCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
    }
    public override string Name => "photo";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Photo != null)
        {
            long chatId = update.Message.Chat.Id;

            int lastMessageId = update.Message.MessageId;

            int previosMessageId = _memoryCachService.GetMessageIdFromMemoryCach(chatId);

            string body = _memoryCachService.GetMailFromMemoryCach(chatId).Body;

            string fileId = update.Message.Photo[2].FileId;

            string photoPath = $@"C:\Users\User\Desktop\CGPhotos\{fileId}.jpg";

            Language? lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            await MessageService.DeleteMessage(chatId, lastMessageId, client);

            await MessageService.DeleteMessage(chatId, previosMessageId, client);

            await SendPhoto(chatId, fileId, client, lang, body);

            SetMemoryCach(chatId, photoPath);
        }
    }

    private void SetMemoryCach(long chatId, string photoPath)
    {

        var mail = _memoryCachService.GetMailFromMemoryCach(chatId);

        mail.PhotoPath = photoPath;

        _memoryCachService.SetMemoryCach(chatId, mail);
    }

    private async Task SendPhoto(long chatId, string photoId,
        ITelegramBotClient client, Language? language, string body)
    {
        if (language == Language.English)
        {
            await MessageService.SendMessage(chatId, client,
                "Check the information in the letter and confirm sending. " +
                "To make changes go back to the sending form start.\n\n" +
                $"{body}", photoId, _englishMarkup);
        }
        else
        {
            await MessageService.SendMessage(chatId, client,
                "Проверь информацию в письме и подтверди отправку. " +
                "Чтобы внести изменения, вернись к началу.\n\n" +
                $"{body}", photoId, _russianMarkup);
        }       
    }
}
