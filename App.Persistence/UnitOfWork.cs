using App.Application.Contracts.Persistence;

namespace App.Persistence;

public class UnitOfWork(Repository.AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
