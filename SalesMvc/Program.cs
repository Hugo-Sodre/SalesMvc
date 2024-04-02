using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesMvc.Data;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Altere "MySqlServerVersion" para a versão correta do seu servidor MySQL
var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 22)); // Exemplo da versão 8.0.22

builder.Services.AddDbContext<SalesMvcContext>(options =>
    options.UseMySql(configuration.GetConnectionString("SalesMvcContext"), mySqlServerVersion, builder =>
        builder.MigrationsAssembly("SalesMvc")));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
