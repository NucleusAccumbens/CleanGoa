﻿namespace Bot.Common.Services;

public class MessageService
{
    public static async Task SendMessage(long chatId, ITelegramBotClient client, string text,
        InlineKeyboardMarkup? inlineKeyboardMarkup)
    {
        await client.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            parseMode: ParseMode.Html,
            replyMarkup: inlineKeyboardMarkup,
            disableWebPagePreview: true);
    }

    public static async Task SendMessage(long chatId, ITelegramBotClient client, string caption, string? path,
        InlineKeyboardMarkup? inlineKeyboardMarkup)
    {
        if (path != null)
        {
            await client.SendPhotoAsync(
                chatId: chatId,
                photo: path,
                caption: caption,
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboardMarkup);
        }
    }

    public static async Task SendMediaGroup(long chatId, ITelegramBotClient client, IEnumerable<IAlbumInputMedia> media)
    {
        await client.SendMediaGroupAsync(chatId, media);
    }

    public static async Task EditMessage(long chatId, int messageId, ITelegramBotClient client,
        string text, InlineKeyboardMarkup? inlineKeyboardMarkup)
    {
        await client.EditMessageTextAsync(
            chatId: chatId,
            messageId: messageId,
            text: text,
            parseMode: ParseMode.Html,
            replyMarkup: inlineKeyboardMarkup,
            disableWebPagePreview: true);
    }

    public static async Task EditMediaMessage(long chatId, int messageId, ITelegramBotClient client,
        string path, InlineKeyboardMarkup? inlineKeyboardMarkup)
    {
        await client.EditMessageMediaAsync(
            chatId: chatId,
            messageId: messageId,
            media: new InputMediaPhoto(new InputMedia(path)),
            replyMarkup: inlineKeyboardMarkup);
    }

    public static async Task DeleteMessage(long chatId, int messageId, ITelegramBotClient client)
    {
        await client.DeleteMessageAsync(
            chatId: chatId,
            messageId: messageId);
    }

    public static async Task ShowAllert(string callbackQueryId, ITelegramBotClient client, string message)
    {
        await client.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQueryId,
                text: message,
                showAlert: true);
    }
}
