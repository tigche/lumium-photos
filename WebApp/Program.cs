using Lumium.Photos.WebApp.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lumium.Photos.WebApp.Data;
using Lumium.Photos.WebApp.Areas.Identity.Data;
using Lumium.Photos.Models.Db.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string applicationConnectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationContextConnection' not found.");
string dataConnectionString = builder.Configuration.GetConnectionString("DataContextConnection") ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.");

// Data
builder.Services
    .AddDbContext<ApplicationContext>(options => options.UseSqlServer(applicationConnectionString))
    .AddDbContext<DataContext>(options => options.UseSqlServer(dataConnectionString))
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationContext>();

// Components
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app
    .UseHttpsRedirection()
    .UseAntiforgery();

app.MapControllers();
app.MapRazorPages();

app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
