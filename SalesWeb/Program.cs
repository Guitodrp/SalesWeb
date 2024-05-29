using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionStr = "server=localhost;userid=developer;password=admin;database=saleswebdb";

builder.Services.AddDbContext<SalesWebContext>(options =>
    options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr)));
//options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebContext") ?? throw new InvalidOperationException("Connection string 'SalesWebContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SeedingService>();

var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

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
