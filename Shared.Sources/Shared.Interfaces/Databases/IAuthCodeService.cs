using Shared.Databases.Entities;

namespace Shared.Interfaces.Databases;

public interface IAuthCodeService
{
    AuthCode? FindById(long Id);
    AuthCode? FindByUser(User user);
    AuthCode Create(User user);
    void Delete(User user);
}