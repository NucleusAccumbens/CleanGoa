using Domain.Entities;
using System;

namespace Application.TlgUsers.Interfaces;

public interface ICreateTlgUserCommand
{
    Task CreateTlgUserAsync(TlgUser tlgUser);
}
