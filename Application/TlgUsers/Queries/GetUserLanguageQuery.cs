using Domain.Enums;

namespace Application.TlgUsers.Queries;

public class GetUserLanguageQuery : IGetUserLanguageQuery
{
    private readonly IBotDbContext _context;

    public GetUserLanguageQuery(IBotDbContext context)
    {
        _context = context;
    }

    public async Task<Language?> GetUserLanguageAsync(long chatId)
    {
        var entity = await _context.TlgUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        return entity?.Language;
    }
}
