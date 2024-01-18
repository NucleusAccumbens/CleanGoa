using Bot.Common.Abstractions;
using Domain.Enums;

namespace Bot.Messages.GeneralMessage;

public class StartMessage : BaseMessage
{
    private readonly string _englishMessage =
        "✨ I was created to help you make Goa cleaner ✨\n\n" +
        "✊🏼 I will help inform burning garbage to legislators, " +
        "I'll tell you about recycling points, upcoming eco-activities and" +
        "I will share other useful information on the topic of cleanliness in Goa.\n\n" +
        "Feel free to send me complaints about the garbage burning.This is the only way." +
        "which could make Goa cleaner and  could help us start enjoying fresh ocean air 🌊\n\n" +
        "Main menu 👇🏻";

    private readonly string _russianMessage =
        "✨ Я создан, чтобы помочь тебе сделать Гоа чище ✨\n\n" +
        "✊🏼 Я помогу сообщить о сжигании мусора в соответствующие органы, " +
        "расскажу о пунктах приёма вторсырья, предстоящих эко-активностях и " +
        "поделюсь другой полезной информацией по теме чистоты в Гоа.\n\n" +
        "Не стесняйся присылать мне жалобы о сжигании мусора. Только так " +
        "мы сможем сделать Гоа чище и начнём наслаждаться свежим океанским воздухом 🌊, " +
        "а не вдыхать продукты горения пластика 😷\n\n" +
        "Главное меню 👇🏻";

    private InlineKeyboardMarkup _englishKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "♻️ Recycling points ♻️", url: "https://goo.gl/maps/5DDTEh1mrezGXBF39"),
        },        
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛔️ Stop burning ⛔️", callbackData: "a"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🤸 Eco activities 🤸", callbackData: "b"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "ℹ️ info", callbackData: "i"),
        },
    });

    private InlineKeyboardMarkup _russianKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "♻️ Пункты приёма вторсырья ♻️", url: "https://goo.gl/maps/5DDTEh1mrezGXBF39"),
        },        
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "⛔️ Останови сжигание ⛔️", callbackData: "a"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🤸 Эко-активности 🤸", callbackData: "b"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "ℹ️ info", callbackData: "i"),
        },
    });

    private readonly Language? _language;

    public StartMessage(Language? language)
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
