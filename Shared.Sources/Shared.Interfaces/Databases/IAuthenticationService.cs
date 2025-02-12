using Microsoft.EntityFrameworkCore;
using Shared.Databases.Entities;

namespace Shared.Interfaces.Databases;

public interface IAuthenticationService
{
    User? FindById(long Id);
    User? FindByEmailOrName(string? email, string? name);
}