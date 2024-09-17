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

app.Run();