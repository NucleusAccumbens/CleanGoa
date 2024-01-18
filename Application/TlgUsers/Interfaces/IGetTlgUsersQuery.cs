using Domain.Entities;

namespace Application.TlgUsers.Interfaces;

public interface IGetTlgUsersQuery
{
    Task<List<TlgUser>> GetAllTlgUsersAsync();
}
