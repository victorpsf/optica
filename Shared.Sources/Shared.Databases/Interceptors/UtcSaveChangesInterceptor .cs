using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Shared.Databases.Interceptors;

public class UtcSaveChangesInterceptor : SaveChangesInterceptor
{
    //public override InterceptionResult<int> SavingChanges(
    //    DbContextEventData eventData, 
    //    InterceptionResult<int> result
    //) {
    //    var context = eventData.Context;
    //    if (context == null) return base.SavingChanges(eventData, result);

    //    foreach (EntityEntry entry in context.ChangeTracker.Entries())
    //    {
    //        if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
    //        {
    //            foreach (PropertyEntry property in entry.Properties)
    //            {
    //                if (property.Metadata.ClrType == typeof(DateTime) || property.Metadata.ClrType == typeof(DateTime?))
    //                {
    //                    Console.WriteLine(property.CurrentValue);

    //                    if (property.CurrentValue is DateTime dateTime && dateTime.Kind != DateTimeKind.Utc)
    //                    {
    //                        dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    //                        property.CurrentValue = dateTime;
    //                        //entry.Property(property.Metadata.Name) = true;
    //                    }

    //                    Console.WriteLine(property.CurrentValue);
    //                }
    //            }
    //        }
    //    }

    //    return base.SavingChanges(eventData, result);
    //}

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChanges(eventData, result);

        foreach (EntityEntry entry in context.ChangeTracker.Entries())
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                foreach (PropertyEntry property in entry.Properties)
                    if (property.Metadata.ClrType == typeof(DateTime) || property.Metadata.ClrType == typeof(DateTime?))
                        if (property.CurrentValue is DateTime dateTime && dateTime.Kind != DateTimeKind.Utc)
                            property.CurrentValue = dateTime.ToUniversalTime();

        return result;
    }
}