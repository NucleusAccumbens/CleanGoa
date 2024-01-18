using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Bot.Messages.EventsMessages;
using Bot.Messages.StopBurningMessages;
using Bot.Models;
using Domain.Entities;

namespace Bot.Commands.EventsCommands.EventsTextCommands;

public class DateTextCommand : BaseTextCommand
{
    private DateMessage _dateMessage;
    
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    public DateTextCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public override string Name => "date";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            int messageId = _memoryCachService.GetMessageIdFromMemoryCach(chatId);

            string date = update.Message.Text;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            _memoryCachService.SetMemoryCach(chatId, new Event() { Date = Convert.ToDateTime(date) });

            _memoryCachService.SetMemoryCach(chatId, "descript");

            await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

            _dateMessage = new(lang, date);

            await _dateMessage.EditMessage(chatId, messageId, client);
        }
    }
}
