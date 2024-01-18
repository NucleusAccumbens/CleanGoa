using Bot.Models;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types;

namespace Bot.Common.Services;

public class MemoryCachService : IMemoryCachService
{
    private readonly IMemoryCache _memoryCach;

    public MemoryCachService(IMemoryCache memoryCache)
    {
        _memoryCach = memoryCache;
    }

    public string? GetCommandStateFromMemoryCach(long chatId)
    {
        var result = _memoryCach.Get(chatId);

        if (result is not null and string)
        {
            return (string)result;
        }

        else return null;
    }

    public Event GetEventFromMemoryCach(long chatId)
    {
        var result = _memoryCach.Get(chatId + 3);

        if (result is not null and Event)
        {
            return (Event)result;
        }

        else throw new Exception();
    }

    public Mail GetMailFromMemoryCach(long chatId)
    {
        var result = _memoryCach.Get(chatId + 2);

        if (result is not null and Mail)
        {
            return (Mail)result;
        }

        else throw new Exception();
    }

    public int GetMessageIdFromMemoryCach(long chatId)
    {
        var result = _memoryCach.Get(chatId + 1);

        if (result is not null and int)
        {
            return (int)result;
        }

        else return 0;
    }

    public void SetMemoryCach(long chatId, string commandState)
    {
        _memoryCach.Set(chatId, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
    }

    public void SetMemoryCach(long chatId, int messageId)
    {
        _memoryCach.Set(chatId + 1, messageId,
           new MemoryCacheEntryOptions
           {
               AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
           });
    }

    public void SetMemoryCach(long chatId, Mail mail)
    {
        _memoryCach.Set(chatId + 2, mail,
           new MemoryCacheEntryOptions
           {
               AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
           });
    }

    public void SetMemoryCach(long chatId, Event eve)
    {
        _memoryCach.Set(chatId + 3, eve,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
    }
}
