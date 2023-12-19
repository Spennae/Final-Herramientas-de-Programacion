using FinalHerr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalHerr.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<FinalHerr.Models.Profesor> Profesor { get; set; } = default!;

    public DbSet<FinalHerr.Models.Clase> Clase { get; set; } = default!;

    public DbSet<FinalHerr.Models.Alumno> Alumno { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clase>()
            .HasMany(c => c.Alumnos)
            .WithMany(a => a.Clases)
            .UsingEntity(j => j.ToTable("AlumnoClase"));
        // Explicitly configure the IdentityUser entity
        modelBuilder.Entity<IdentityUser>().HasKey(u => u.Id);

        // Explicitly configure the IdentityRole entity
        modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);

        // Explicitly configure the IdentityUserLogin entity
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });

        // Explicitly configure the IdentityUserRole entity
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });

        // Explicitly configure the IdentityUserClaim entity
        modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(c => c.Id);

        // Explicitly configure the IdentityRoleClaim entity
        modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(c => c.Id);

        // Explicitly configure the IdentityUserToken entity
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }
}

