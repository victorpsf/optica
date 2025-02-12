using Microsoft.EntityFrameworkCore;
using Shared.Interfaces.Databases;

namespace Shared.Databases.Services;

public class AbstractService<B, T>: IAbstractService<B, T>
    where B : DbContext
    where T : class, new()
{
    public B Context { get; private set; }
    public DbSet<T> DbSet { get; private set; }

    public AbstractService(B context, DbSet<T> dbSet)
    {
        this.Context = context;
        this.DbSet = dbSet;
    }

    public T Save(T entity)
    {
        this.DbSet.Add(entity);
        this.Context.SaveChanges();
        return entity;
    }

    public T Remove(T entity)
    {
        this.DbSet.Remove(entity);
        this.Context.SaveChanges();
        return entity;
    }
}