using ECommerce.SharedFramework;

namespace ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}