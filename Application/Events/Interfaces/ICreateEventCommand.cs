using Domain.Entities;

namespace Application.Events.Interfaces;

public interface ICreateEventCommand
{
    Task CreateEventAsync(Event eve);
}
