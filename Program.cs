using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");

builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
        npgsqlOptions.MigrationsAssembly("SalesWebMvc")));

builder.Services.AddScoped<SeedingService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ Aplique as migrações e rode o seed ANTES do pipeline
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    try {
        var context = services.GetRequiredService<SalesWebMvcContext>();

        // Aplica todas as migrações pendentes
        context.Database.Migrate();

        // Executa o seed
        var seeder = services.GetRequiredService<SeedingService>();
        seeder.Seed();
    }
    catch (Exception ex) {
        // Log mínimo para você enxergar erros de seed
        Console.WriteLine("Erro durante migração/seed: " + ex.Message);
        throw;
    }
}

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