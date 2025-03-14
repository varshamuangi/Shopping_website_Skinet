using Microsoft.EntityFrameworkCore;
using infrastructure.Data;
using core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register StoreContext with Dependency Injection
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository,ProductRepository>();     

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
try{
   using var scope = app.Services.CreateScope();
   var services = scope.ServiceProvider;
   var context = services.GetRequiredService<StoreContext>();
   await context.Database.MigrateAsync();
   await StoreContextSeed.SeedAsync(context);
}
catch(Exception ex)
{
   Console.WriteLine(ex);
   throw;
}
app.Run();
