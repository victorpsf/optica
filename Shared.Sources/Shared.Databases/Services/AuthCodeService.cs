using Microsoft.EntityFrameworkCore;
using Shared.Databases.DbContexts;
using Shared.Databases.Entities;
using Shared.Interfaces.Databases;
using Shared.Libraries;

namespace Shared.Databases.Services;

public class AuthCodeService: AbstractService<AuthenticationDbContext, AuthCode>, IAuthCodeService
{
    public AuthCodeService(AuthenticationDbContext context): base(context, context.AuthCodes) { }
    
    public AuthCode? FindById(long id)
        => this.DbSet.AsQueryable()
            .Where(x => x.Id == id)
            .ToArray()
            .FirstOrDefault();
    
    public AuthCode? FindByUser(User user)
        => this.DbSet.AsQueryable()
            .Include(a => a.User)
            .Where(x => x.User.Id == user.Id)
            .ToArray()
            .FirstOrDefault();

    public AuthCode Create(User user)
    {
        var code = this.FindByUser(user);

        if (code is null)
        {
            code = new AuthCode()
            {
                Code = RandomUtil.RandomString(9),
                ExpireIn = DateTime.UtcNow.AddMinutes(4),
                User = user
            };            
            
            this.DbSet.Add(code);
            this.Context.SaveChanges();
            return code;
        }

        if (code.ExpireIn < DateTime.UtcNow)
        {
            code.ExpireIn = DateTime.UtcNow.AddMinutes(4);
            code.Code = RandomUtil.RandomString(9);
            this.DbSet.Update(code);
            this.Context.SaveChanges();
        }
        
        return code;
    }

    public void Delete(User user)
    {
        var code = this.FindByUser(user);

        if (code is null) return;

        this.DbSet.Remove(code);
        this.Context.SaveChanges();
    }
}