using Application.Events.Interfaces;
using Domain.Entities;

namespace Application.Events.Queries;

public class GetEventsQuery : IGetEventsQuery
{
    private readonly IBotDbContext _context;

    public GetEventsQuery(IBotDbContext context)
    {
        _context = context;
    }

    public Task<List<Event>> GetEventsAsync()
    {               
        return _context.Events
            .Where(e => e.Date.CompareTo(DateTime.UtcNow) >= 0)
            .ToListAsync();
    }
}
