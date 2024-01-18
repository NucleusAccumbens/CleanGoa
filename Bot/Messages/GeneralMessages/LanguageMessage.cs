using Bot.Common.Services;

namespace Bot.Messages.GeneralMessage;

public class LanguageMessage
{
    private readonly string _messageText = "<i><b>Choose language</b></i>";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "RU 🇷🇺", callbackData: "~🇷🇺"),
            InlineKeyboardButton.WithCallbackData(text: "EN 🇬🇧", callbackData: "~🇬🇧"),
        },
    });


    public async Task GetMessage(long chatId, ITelegramBotClient client)
    {
        await MessageService.SendMessage(chatId, client, _messageText, _inlineKeyboardMarkup);
    }
}
