using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinalHerr.Data;
using FinalHerr.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// Add Razor Pages services
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IClaseService, ClaseService>();
builder.Services.AddScoped<IAlumnoService, AlumnoService>();

var app = builder.Build();

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    SeedRoles(roleManager).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Add this line to enable authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();  // Add this line to map Razor Pages

app.Run();

async Task SeedRoles(RoleManager<IdentityRole> roleManager)
{
    // Seed roles if they don't exist
    if (!await roleManager.RoleExistsAsync("Administracion"))
    {
        await roleManager.CreateAsync(new IdentityRole("Administracion"));
    }

    if (!await roleManager.RoleExistsAsync("Secretaria"))
    {
        await roleManager.CreateAsync(new IdentityRole("Secretaria"));
    }

    if (!await roleManager.RoleExistsAsync("SysAdmin"))
    {
        await roleManager.CreateAsync(new IdentityRole("SysAdmin"));
    }
}
