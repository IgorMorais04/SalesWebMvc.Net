using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using System.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Acessando a string de conexão
var connectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");

// Usando o PostgreSQL, não MySQL
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsAssembly("SalesWebMvc")));

// Adiciona os serviços ao contêiner
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
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