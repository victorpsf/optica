
using Microsoft.EntityFrameworkCore;
using Shared.Databases.DbContexts;
using Shared.Databases.Entities;
using Shared.Extensions;
using Shared.Interfaces.Databases;

namespace Shared.Databases.Services;

public class AuthenticationService: AbstractService<AuthenticationDbContext, User>, IAuthenticationService
{
    public AuthenticationService(AuthenticationDbContext context): base(context, context.Users) { }

    public User? FindById(long id)
        => this.DbSet.AsQueryable()
            .Where(x => x.Id == id)
            .ToArray()
            .FirstOrDefault();

    public User? FindByEmailOrName(
        string? email, 
        string? name
    ) {
        if (email.IsNullOrEmpty() && name.IsNullOrEmpty())
            return null;
        
        var queryable = this.DbSet.AsQueryable();

        if (!email.IsNullOrEmpty())
            queryable = queryable.Where(x => x.Email == email);
        if (!name.IsNullOrEmpty())
            queryable = queryable.Where(x => x.Name == name);
        
        return queryable.ToList()
            .FirstOrDefault();
    }
}