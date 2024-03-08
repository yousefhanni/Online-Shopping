using Microsoft.EntityFrameworkCore;
using Myshop.DAL.Data.Context;

var builder = WebApplication.CreateBuilder(args);
//Add the DbContext as a service to the dependency injection container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // sets the Connection string for the SQL Server by retrieving it from the application's configuration  
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();
///Database migrations enhance the development process by automating database schema updates,
///improving version control, and providing mechanisms for effective error handling and logging
using var Scope = app.Services.CreateScope();

var Services = Scope.ServiceProvider;

var _dbContext = Services.GetRequiredService<ApplicationDbContext>();

var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
try
{
    await _dbContext.Database.MigrateAsync();

}

catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has occured during apply the migration");

}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
