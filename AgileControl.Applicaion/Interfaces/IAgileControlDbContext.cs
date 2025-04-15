using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Interfaces;

public interface IAgileControlDbContext
{ 
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task SaveChangesAsync(CancellationToken cancellationToken);
}