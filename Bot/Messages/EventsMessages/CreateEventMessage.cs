using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.EventsMessages;

public class CreateEventMessage : BaseMessage
{
    private readonly string _englishMessage =
         "Send the date of the event in the format <b>dd.mm.yyyy</b> " +
        "by message to this chat.";

    private readonly string _russianMessage =
        "Отправь дату мероприятия в формате <b>дд.мм.гггг</b> сообщением в этот чат.";

    private readonly Language? _language;

    public CreateEventMessage(Language? language)
    {
        _language = language;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "b"),
        },
    });

    private string GetMessage()
    {
        if (_language == Language.English) return _englishMessage;

        else return _russianMessage;
    }
}
