using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.GeneralMessages;

public class InfoMessage : BaseMessage
{
    private readonly string _englishMessage =
         "<a href=\"https://www.facebook.com/groups/CleanUpAroundYou/permalink/730141818190539/?sfnsn=wiwspmo&ref=share\">Here</a>, " +
         "<a href=\"https://www.instagram.com/tv/CekkdxCpjah/?igshid=NjZiMGI4OTY=\">here</a> and " +
         "<a href=\"https://t.me/vozduh_goa_arambol\">here</a> " +
         "you can get more information about the eco " +
         "activities in Goa and what you can do now!";

    private readonly string _russianMessage =
        "<a href=\"https://www.facebook.com/groups/CleanUpAroundYou/permalink/730141818190539/?sfnsn=wiwspmo&ref=share\">Здесь</a>, " +
        " <a href=\"https://www.instagram.com/tv/CekkdxCpjah/?igshid=NjZiMGI4OTY=\">здесь</a> " +
        "и <a href=\"https://t.me/vozduh_goa_arambol\">здесь</a> " +
        "можно получить больше информации об эко-активистской " +
        "деятельности в Гоа, а также о том что можешь сделать ты уже сейчас!";

    private readonly Language? _language;

    public InfoMessage(Language? language)
    {
        _language = language;
    }

    public override string MessageText => GetMessage();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: "<"),
        },
    });

    private string GetMessage()
    {
        if (_language == Language.English) return _englishMessage;

        else return _russianMessage;
    }
}
