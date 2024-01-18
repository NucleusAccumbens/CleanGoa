using Bot.Models;
using Domain.Entities;

namespace Bot.Common.Interfaces;

public interface IMemoryCachService
{
    void SetMemoryCach(long chatId, string commandState);

    void SetMemoryCach(long chatId, int messageId);

    void SetMemoryCach(long chatId, Mail mail);

    void SetMemoryCach(long chatId, Event eve);

    string? GetCommandStateFromMemoryCach(long chatId);

    int GetMessageIdFromMemoryCach(long chatId);

    Mail GetMailFromMemoryCach(long chatId);

    Event GetEventFromMemoryCach(long chatId);
    
}
