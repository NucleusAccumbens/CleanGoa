using Application.TlgUsers.Interfaces;
using Bot.Common.Abstractions;
using Bot.Common.Services;
using Bot.Messages.EventsMessages;

namespace Bot.Commands.EventsCommands.EventsTextCommands;

public class DescriptTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetUserLanguageQuery _getUserLanguageQuery;

    private SaveEventMessage _saveEventMessage;

    public DescriptTextCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _memoryCachService = memoryCachService;
        _getUserLanguageQuery = getUserLanguageQuery;
    }

    public override string Name => "descript";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            int messageId = _memoryCachService.GetMessageIdFromMemoryCach(chatId);

            string descript = update.Message.Text;

            var lang = await _getUserLanguageQuery.GetUserLanguageAsync(chatId);

            var eve = _memoryCachService.GetEventFromMemoryCach(chatId);

            eve.Description = descript;

            _memoryCachService.SetMemoryCach(chatId, eve);

            await MessageService.DeleteMessage(chatId, update.Message.MessageId, client);

            _saveEventMessage = new(lang, eve);

            await _saveEventMessage.EditMessage(chatId, messageId, client);
        }
    }
}
