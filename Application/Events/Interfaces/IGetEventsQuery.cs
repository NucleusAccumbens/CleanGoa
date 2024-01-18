using Domain.Entities;

namespace Application.Events.Interfaces;

public interface IGetEventsQuery
{
    Task<List<Event>> GetEventsAsync();
}
