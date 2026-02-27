using Microsoft.EntityFrameworkCore;
using mission8.Data;

var builder = WebApplication.CreateBuilder(args);

// Add MVC (controllers + views) to the service container
builder.Services.AddControllersWithViews();

// Register EF Core with SQLite -- connection string comes from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the repository using the interface (Repository Pattern)
// AddScoped means a new instance is created per HTTP request
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Default route: HomeController -> Index action
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
