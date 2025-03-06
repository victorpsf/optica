using Microsoft.EntityFrameworkCore;
using Shared.Databases.Entities;

namespace Shared.Interfaces.Databases;

public interface IUserService
{
    User? FindById(long Id);
    User? FindByEmailOrName(string? email, string? name);
}