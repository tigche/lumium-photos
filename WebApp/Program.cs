using Lumium.Photos.WebApp.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lumium.Photos.Models.Db.Context;
using Lumium.Photos.Models.Db.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string applicationConnectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationContextConnection' not found.");

// Data
builder.Services
    .AddDbContextFactory<ApplicationContext>(options =>
    {
        options.UseSqlServer(applicationConnectionString, builder =>
        {
            builder.MigrationsAssembly("Lumium.Photos.WebApp");
        });
    })
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<ApplicationContext>(implementationFactory =>
{
    return implementationFactory
        .GetRequiredService<IDbContextFactory<ApplicationContext>>()
        .CreateDbContext();
});

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
