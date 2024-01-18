using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.EventsMessages;

public class EventMessage : BaseMessage
{
    private readonly string _englishMessage =
         "In this section you can learn about upcoming eco-activities " +
        "or create your own event.";

    private readonly string _russianMessage =
        "В этом разделе ты можешь узнать о предстоящих эко-активностях " +
        "или добавить новое событие.";

    private InlineKeyboardMarkup _englishKeyboardMarkup = new(new[]
    {
   
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌱 See events 🌱", callbackData: "bSee"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "➕ Create an event ➕", callbackData: "bCreate"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "<"),
        },
    });

    private InlineKeyboardMarkup _russianKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🌱 Предстоящие активности 🌱", callbackData: "bSee"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "➕ Добавить событие ➕", callbackData: "bCreate"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "<"),
        },
    });

    private readonly Language? _language;

    public EventMessage(Language? language)
    {
        _language = language;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private string GetMessage()
    {
        if (_language == Language.English) return _englishMessage;

        else return _russianMessage;
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        if (_language == Language.English) return _englishKeyboardMarkup;

        else return _russianKeyboardMarkup;
    }
}
