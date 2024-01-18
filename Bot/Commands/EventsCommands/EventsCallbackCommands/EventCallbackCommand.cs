using Application.Events.Interfaces;
using Application.TlgUsers.Commands;
using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Bot.Messages.EventsMessages;
using Bot.Messages.GeneralMessage;
using Domain.Entities;
using Domain.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Bot.Commands.EventsCommands.EventsCallbackCommands;

internal class EventCallbackCommand : BaseCallbackCommand
{
    private readonly string _englishMessage =
        "🤷🏻‍♀️ there are no events yet... be the first, create your own!";

    private readonly string _russianMessage =
        "🤷🏻‍♀️ пока что нет ни одного события...будь первым, создай своё!";

    private readonly string _engMessage =
        "event published";

    private readonly string _rusMessage =
        "событие опубликовано";

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

    private EventMessage _eventMessage;

    private CreateEventMessage _createEventMessage;

    private StartMessage _startMessage;

    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private readonly IGetEventsQuery _getEventsQuery;

    private readonly ICreateEventCommand _createEventCommand;

    private readonly IGetTlgUsersQuery _getTlgUsersQuery;

    public EventCallbackCommand(IGetUserLanguageQuery getUserLanguageQuery, 
        IGetEventsQuery getEventsQuery, IMemoryCachService memoryCachService, 
        ICreateEventCommand createEventCommand, IGetTlgUsersQuery getTlgUsersQuery)
    {
        _getUserLanguageQuery = getUserLanguageQuery;
        _getEventsQuery = getEventsQuery;
        _memoryCachService = memoryCachService;
        _createEventCommand = createEventCommand;
        _getTlgUsersQuery = getTlgUsersQuery;
    }

    public override char CallbackDataCode => 'b';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            if (update.CallbackQuery.Data == "b")
            {
                _eventMessage = new(lang);

                await _eventMessage.EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "bSee")
            {
                var events = await _getEventsQuery.GetEventsAsync();

                if (events.Count == 0) await ShowAllert(update.CallbackQuery.Id, client, lang);

                else await SendEvents(chatId, client, events, lang);

                return;
            }
            if (update.CallbackQuery.Data == "bCreate")
            {
                _memoryCachService.SetMemoryCach(chatId, "date");

                _memoryCachService.SetMemoryCach(chatId, messageId);

                _createEventMessage = new(lang);

                await _createEventMessage.EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "bSave")
            {
                var eve = _memoryCachService.GetEventFromMemoryCach(chatId);

                await _createEventCommand.CreateEventAsync(eve);

                await ShowAllertForSave(update.CallbackQuery.Id, client, lang);

                _startMessage = new(lang);

                await _startMessage.EditMessage(chatId, messageId, client);

                await SendNotify(client, eve, lang, chatId);
            }
        }
    }

    private async Task ShowAllert(string callbackId, ITelegramBotClient client, Language? language)
    {
        if (language == Language.Russian)
        {
            await MessageService.ShowAllert(callbackId, client, _russianMessage);
        }
        else
        {
            await MessageService.ShowAllert(callbackId, client, _englishMessage);
        }
    }

    private async Task ShowAllertForSave(string callbackId, ITelegramBotClient client, Language? language)
    {
        if (language == Language.Russian)
        {
            await MessageService.ShowAllert(callbackId, client, _rusMessage);
        }
        else
        {
            await MessageService.ShowAllert(callbackId, client, _engMessage);
        }
    }

    private async Task SendEvents(long chatId, ITelegramBotClient client, List<Event> events, 
        Language? lang)
    {
        foreach (var eve in events)
        {
            if (lang == Language.English)
            {
                await MessageService.SendMessage(chatId, client,
                    $"<b>{eve.Date.ToLongDateString()}</b>\n\n" +
                    $"{eve.Description}", _englishMarkup);
            }

            else await MessageService.SendMessage(chatId, client,
                $"<b>{eve.Date.ToLongDateString()}</b>\n\n" +
                $"{eve.Description}", _russianMarkup);
        }
    }

    private async Task SendNotify(ITelegramBotClient client, Event eve, Language? lang, long excludedChatId)
    {
        var users = await _getTlgUsersQuery.GetAllTlgUsersAsync();

        foreach (var user in users) 
        {
            if (user.ChatId != excludedChatId)
            {
                try
                {
                    if (lang == Language.English)
                    {
                        await MessageService.SendMessage(user.ChatId, client,
                            $"<b>{eve.Date.ToLongDateString()}</b>\n\n" +
                            $"{eve.Description}", _englishMarkup);
                    }

                    else await MessageService.SendMessage(user.ChatId, client,
                        $"<b>{eve.Date.ToLongDateString()}</b>\n\n" +
                        $"{eve.Description}", _russianMarkup);
                }
                catch (ApiRequestException ex)
                {
                    await MessageService.SendMessage(444343256, client,
                        $"Ошибка рассылки опроса: {ex.Message}", null);
                }
                catch (Exception ex)
                {
                    await MessageService.SendMessage(444343256, client,
                        $"Ошибка рассылки опроса: {ex.Message}", null);
                }
            }                
        }
    }
}
