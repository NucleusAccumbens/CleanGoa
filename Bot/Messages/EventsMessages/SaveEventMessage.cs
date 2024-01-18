using Bot.Common.Abstractions;
using Domain.Entities;
using Domain.Enums;

namespace Bot.Messages.EventsMessages;

public class SaveEventMessage : BaseMessage
{
    private readonly string _englishMessage =
        "Click \"✅ Save\" and the event will be available for viewing by other users. " +
        "To correct information, go back to start.";

    private readonly string _russianMessage =
        "Нажми \"✅ Сохранить\", чтобы опубликовать событие. " +
        "Чтобы отредактировать информацию вернись к началу.";

    private InlineKeyboardMarkup _englishKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✅ Save", callbackData: "aComplain"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "bCreate"),
        },
    });

    private InlineKeyboardMarkup _russianKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✅ Сохранить", callbackData: "bSave"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "bCreate"),
        },
    });

    private readonly Language? _language;

    private readonly Event _eve;

    public SaveEventMessage(Language? language, Event eve)
    {
        _language = language;
        _eve = eve;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private string GetMessage()
    {
        if (_language == Language.English) return
                 $"Event date: {_eve.Date.ToShortDateString()}\n" +
                 $"Description: {_eve.Description}" +
                 $"\n\n{_englishMessage}";

        else return $"Дата мероприятия: {_eve.Date.ToShortDateString()}\n" +
                    $"Описание: {_eve.Description}" +
                    $"\n\n{_russianMessage}";
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        if (_language == Language.English) return _englishKeyboardMarkup;

        else return _russianKeyboardMarkup;
    }
}
