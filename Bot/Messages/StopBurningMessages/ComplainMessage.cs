using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.StopBurningMessages;

public class ComplainMessage : BaseMessage
{
    private readonly string _russianMessage =
        "Чтобы сообщение о нарушении было рассмотрено, нужно указать некоторые данные " +
        "и прикрепить фото, фиксирующее факт сжигания.\n\n" +
        "Для начала отправь в чат с ботом сообщение с указанием где и когда происходило сжигание:  " +
        "село, гестхаус или район рядом с гестхаусом, время, число.\n\n" +
        "<i>(Письмо должно быть на английском и не должно привышать 250 сиволов)</i>";

    private readonly string _englishMessage =
        "In order for the report of the violation to be considered, " +
        "you need to specify some data and attach a photo fixing the fact of burning.\n\n" +
        "To start, send a message to the chat with the bot indicating where " +
        "and when the burning took place: village, guesthouse, " +
        "or area next to the guesthouse, time, date.\n\n" +
        "(Letter must not exceed 200 characters)";

    private readonly Language? _language;

    public ComplainMessage(Language? language)
    {
        _language = language;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "a"),
        },
    });

    private string GetMessage()
    {
        if (_language == Language.English) return _englishMessage;

        else return _russianMessage;
    }
}
