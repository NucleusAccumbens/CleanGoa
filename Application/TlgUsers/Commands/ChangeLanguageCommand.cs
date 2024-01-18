using Domain.Enums;

namespace Application.TlgUsers.Commands;

public class ChangeLanguageCommand : IChangeLanguageCommand
{
    private readonly IBotDbContext _context;

    public ChangeLanguageCommand(IBotDbContext context)
    {
        _context = context;
    }

    public async Task ChangeLanguageAsync(long chatId, Language language)
    {
        var entity = await _context.TlgUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (entity != null)
        {
            entity.Language = language;

            await _context.SaveChangesAsync();
        }
    }
}
