using Microsoft.EntityFrameworkCore;

namespace Shared.Interfaces.Databases;

public interface IAbstractService<B, T>
    where B : DbContext
    where T : class, new()
{
    B Context { get; }
    DbSet<T> DbSet { get; }
    
    
    T Save(T entity);
    T Remove(T entity);
}