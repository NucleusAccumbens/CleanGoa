using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.StopBurningMessages;

internal class StopBurningMessage : BaseMessage
{
    private readonly string _russianMessage =
        "Законы Гоа предусматривают штраф за сжигание мусора. " +
        "Покажи \"<b>Стоп картинки</b>\" с информацией индийцам, нарушающим закон, и возможно их это остановит.\n\n" +
        "Если сжигание мусора повторяется и информация о штрафе не помогла, " +
        "необходимо написать жалобу в государственные органы. " +
        "Нажми \"<b>Пожаловаться</b>\" и я разошлю письма сразу в 6 инстанций.";

    private readonly string _englishMessage =
        "Goan laws provide for a fine for burning garbage. " +
        "Show \"<b>Stop pictures</b>\" of information to indians who break the law and most likely it will stop them.\n\n" +
        "If the burning of garbage is repeated and information about the fine did not stop them, " +
        "it is necessary to write a complaint to the state authorities. " +
        "Press \"<b>Complain</b>\" and i will help you send six emails at once in just a few steps.";

    private InlineKeyboardMarkup _englishKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🚫 Stop pictures 🚫", callbackData: "aStopPictures"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "📮 Complain 📮", callbackData: "aComplain"),
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
            InlineKeyboardButton.WithCallbackData(text: "🚫 Стоп картинки 🚫", callbackData: "aStopPictures"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "📮 Пожаловаться 📮", callbackData: "aComplain"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "<"),
        },
    });

    private readonly Language? _language;

    public StopBurningMessage(Language? language)
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
