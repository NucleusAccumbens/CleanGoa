using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.StopBurningMessages;

public class PhotoMessage : BaseMessage
{
    private readonly string _russianMessage =
        "Проверь информацию в письмe. Если всё верно, отправь фото " +
        "фиксирующее нарушение сообщением в этот чат. " +
        "Чтобы внести изменения, вернись к предыдущему шагу.\n\n";

    private readonly string _englishMessage =
        "Check the information in the letter. If everything is correct, send a photo " +
        "fixing the violation with a message in this chat. " +
        "To make changes go back to the previous step.\n\n";

    private string _body;

    private readonly Language? _language;

    public PhotoMessage(Language? language, string body)
    {
        _language = language;
        _body = body;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "aComplain"),
        },
    });

    private string GetMessage()
    {
        if (_language == Language.English) return _englishMessage + _body;

        else return _russianMessage + _body;
    }
}
