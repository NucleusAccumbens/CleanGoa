using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.EventsMessages;

public class DateMessage : BaseMessage
{
    private readonly string _englishMessage =
        "If the date is correct, send a message describing the event " +
        "to this chat. don't forget to include the time and place, " +
        "as well as other useful information, such as contact details.";

    private readonly string _russianMessage =
        "Если дата указана верно, отправь сообщение с описанием события в этот чат. " +
        "Не забудь указать время и место, а также другую полезную информацию, например контакты для связи.";

    private readonly Language? _language;

    private readonly string _date;

    public DateMessage(Language? language, string date)
    {
        _language = language;
        _date = date;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "bCreate"),
        },
    });

    private string GetMessage()
    {
        if (_language == Language.English) return
                 $"Event date: {_date}\n\n{_englishMessage}";

        else return $"Дата мероприятия: {_date}\n\n{_russianMessage}"; 
    }
}
