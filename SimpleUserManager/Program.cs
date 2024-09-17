using Microsoft.EntityFrameworkCore;
using SimpleUserManager.DataContext;
using SimpleUserManager.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddUserManagerDbContext("DbConnection");
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.ConfigureAuthentication();
builder.Services.AddAutoMapper();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UserManagerDbContext>();

    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();