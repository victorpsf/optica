namespace Shared.Interfaces.Services;

public interface IHostCache
{
    T? Get<T>(string name);
    void Set<T>(string name, T value, int secconds);
    void Unset(string name);
}