using core.Entities;
using infrastructure.Config;
using Microsoft.EntityFrameworkCore;


namespace infrastructure.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    // Define your database tables as DbSets
    public DbSet<Product> Products {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
                                    
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PoductConfiguration).Assembly);
    }

}
