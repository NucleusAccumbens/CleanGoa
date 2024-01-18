using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Bot.Messages.StopBurningMessages;
using Domain.Enums;

namespace Bot.Commands.StopBurningCommands.CallbackCommands;

public class StopBurningCallbackCommand : BaseCallbackCommand
{
    private readonly InlineKeyboardMarkup _englishMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✖️ hide", callbackData: "-"),
        },
    });

    private readonly InlineKeyboardMarkup _russianMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✖️ скрыть", callbackData: "-"),
        },
    });

    private StopBurningMessage _burningMessage;

    private ComplainMessage _complainMessage;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IMemoryCachService _memoryCachService;

    public StopBurningCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, IMemoryCachService memoryCachService)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'a';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            if (update.CallbackQuery.Data == "a")
            {
                _burningMessage = new(lang);

                await _burningMessage.EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "aStopPictures")
            {
                await SendPictures(chatId, client, lang);

                return;
            }
            if (update.CallbackQuery.Data == "aComplain")
            {
                _memoryCachService.SetMemoryCach(chatId, "body");

                _memoryCachService.SetMemoryCach(chatId, messageId);
                
                _complainMessage = new(lang);

                await _complainMessage.EditMessage(chatId, messageId, client);
            }
        }
    }

    private async Task SendPictures(long chatId, ITelegramBotClient client, Language? language)
    {
        if (language == Language.English) 
        {
            await MessageService.SendMessage(chatId, client, String.Empty, 
                "AgACAgIAAxkBAAM2Y7p0lu3ipPoNG_ceiqiA6EvdZy0AAm7BMRs3M9lJZApuKr4DgZ0BAAMCAAN4AAMtBA", 
                _englishMarkup);

            await MessageService.SendMessage(chatId, client, String.Empty,
                "AgACAgIAAxkBAAM4Y7p0tP54_oj4F8UVJhAEEhVkAAFuAAJvwTEbNzPZSYH3TrOu9KYFAQADAgADeAADLQQ",
                _englishMarkup);

            return;
        }
        if (language == Language.Russian)
        {
            await MessageService.SendMessage(chatId, client, String.Empty,
                "AgACAgIAAxkBAAM2Y7p0lu3ipPoNG_ceiqiA6EvdZy0AAm7BMRs3M9lJZApuKr4DgZ0BAAMCAAN4AAMtBA",
                _russianMarkup);

            await MessageService.SendMessage(chatId, client, String.Empty,
                "AgACAgIAAxkBAAM4Y7p0tP54_oj4F8UVJhAEEhVkAAFuAAJvwTEbNzPZSYH3TrOu9KYFAQADAgADeAADLQQ",
                _russianMarkup);
        }
    }
}
