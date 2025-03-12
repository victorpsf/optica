
using Microsoft.EntityFrameworkCore;
using Shared.Databases.DbContexts;
using Shared.Databases.Entities;
using Shared.Extensions;
using Shared.Interfaces.Databases;
using Shared.Libraries;

namespace Shared.Databases.Services;

public class UserService: AbstractService<AuthenticationDbContext, User>, IUserService
{
    public UserService(AuthenticationDbContext context): base(context, context.Users) { }

    public User? FindById(long id)
        => this.DbSet.AsQueryable()
            .Where(x => x.UserId == id)
             .Include(a => a.Roles)
                .ThenInclude(a => a.Role)
                .ThenInclude(a => a.RolePermissions)
                .ThenInclude(a => a.Permission)
            .Include(a => a.UserEnterprises)
                .ThenInclude(a => a.Enterprise)
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

        queryable = queryable.Where(x => x.UserEnterprises.Any(b => b.Enterprise.Active == true));
        queryable = queryable.Where(x => x.UserEnterprises.Any(b => b.Enterprise.DeletedAt == null));

        return queryable
            .Include(a => a.Roles)
                .ThenInclude(a => a.Role)
                .ThenInclude(a => a.RolePermissions)
                .ThenInclude(a => a.Permission)
            .Include(a => a.UserEnterprises)
                .ThenInclude(a => a.Enterprise)
            .ToList()
            .FirstOrDefault();
    }
}