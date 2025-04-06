using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repository.Interceptors;

public class AuditDbContextInterceptors : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

            Behaviours[entityEntry.State](eventData.Context, auditEntity);
            
            // switch (entityEntry.State)
            // {
            //     case EntityState.Added:
            //         AddBehaviour(eventData.Context, auditEntity);
            //         break;
            //     case EntityState.Detached:
            //         UpdateBehaviour(eventData.Context, auditEntity);
            //         break;
            // }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static readonly Dictionary<EntityState, Action<DbContext , IAuditEntity>> Behaviours = new()
    {
        { EntityState.Added, AddBehaviour },
        { EntityState.Modified, UpdateBehaviour }
    };

    private static void AddBehaviour(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.Now;
        context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
    }
    
    private static void UpdateBehaviour(DbContext context, IAuditEntity auditEntity)
    {
        context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
        auditEntity.Updated = DateTime.Now;
    }
}