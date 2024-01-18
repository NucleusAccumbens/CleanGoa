using Application.Events.Interfaces;
using Domain.Entities;

namespace Application.Events.Commands;

public class CreateEventCommand : ICreateEventCommand
{
    private readonly IBotDbContext _context;

    public CreateEventCommand(IBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateEventAsync(Event eve)
    {
        await _context.Events.AddAsync(eve);

        await _context.SaveChangesAsync();
    }
}
