using AuthServiceIN6BM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuthServiceIN6BM.Persistence.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<UserEmail> UserEmails => Set<UserEmail>();
    public DbSet<UserPasswordReset> UserPasswordResets => Set<UserPasswordReset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ===== USER =====
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
            entity.Property(e => e.Surname).IsRequired().HasMaxLength(25);
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Password).IsRequired().HasMaxLength(255);

            entity.Property(e => e.Status).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdateAt).IsRequired();

            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();

            // Relaciones
            entity.HasOne(e => e.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);

            entity.HasMany(e => e.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            entity.HasOne(e => e.UserEmail)
                .WithOne(ue => ue.User)
                .HasForeignKey<UserEmail>(ue => ue.UserId);

            // 🔥 CORRECCIÓN CLAVE
            entity.HasOne(e => e.UserPasswordReset)
                .WithOne(upr => upr.User)
                .HasForeignKey<UserPasswordReset>(upr => upr.UserId);
        });

        // ===== USER PROFILE =====
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).HasMaxLength(16);
            entity.Property(e => e.ProfilePicture).HasDefaultValue("");
            entity.Property(e => e.Phone).HasMaxLength(8);
        });

        // ===== ROLE =====
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });

        // ===== USER ROLE =====
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).HasMaxLength(16);
            entity.Property(e => e.RoleId).HasMaxLength(16);

            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        });

        // ===== USER EMAIL =====
        modelBuilder.Entity<UserEmail>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).HasMaxLength(16);
            entity.Property(e => e.EmailVerified).HasDefaultValue(false);
            entity.Property(e => e.EmailVerificationToken).HasMaxLength(256);
        });

        // ===== PASSWORD RESET =====
        modelBuilder.Entity<UserPasswordReset>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).HasMaxLength(16);
            entity.Property(e => e.PasswordResetToken).HasMaxLength(256);
        });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e =>
                (e.Entity is User || e.Entity is Role || e.Entity is UserRole) &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;

            switch (entry.Entity)
            {
                case User user:
                    if (entry.State == EntityState.Added)
                        user.CreatedAt = now;
                    user.UpdateAt = now;
                    break;

                case Role role:
                    if (entry.State == EntityState.Added)
                        role.CreatedAt = now;
                    role.UpdatedAt = now;
                    break;

                case UserRole ur:
                    if (entry.State == EntityState.Added)
                        ur.CreatedAt = now;
                    ur.UpdatedAt = now;
                    break;
            }
        }
    }
}
