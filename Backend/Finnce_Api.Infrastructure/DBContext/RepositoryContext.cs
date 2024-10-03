

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Finnce_Api.Infrastructure.DBContext;

public class RepositoryContext : IdentityDbContext<IdentityUser>, IRepositoryContext
{

    public RepositoryContext(DbContextOptions options) : base(options)
    {

    }


    public DbSet<User>? Users { get; set; }
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Category>? Categories { get; set; }

    public DbSet<Notification>? Notifications { get; set; }
    public DbSet<EntityCore>? Entities { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }
    public DbSet<TinkAcessApi>? ThinkAcessApis { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
       .HasDiscriminator<string>("User");

        modelBuilder.Entity<Account>()
        .HasOne(p => p.User)
        .WithMany(u => u.ListAccounts)
        .HasPrincipalKey(x => x.Id)
        .HasForeignKey(p => p.IdUser)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
        .HasOne(p => p.User)
        .WithMany(u => u.ListNotifications)
        .HasPrincipalKey(x => x.Id)
        .HasForeignKey(p => p.IdUser)
        .OnDelete(DeleteBehavior.Restrict);



        modelBuilder.Entity<Category>()
        .HasOne(p => p.User)
        .WithMany(u => u.ListCategories)
        .HasPrincipalKey(x => x.Id)
        .HasForeignKey(p => p.IdUser)
        .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Transaction>()
        .HasOne(p => p.User)
        .WithMany(u => u.ListTransactions)
        .HasPrincipalKey(x => x.Id)
        .HasForeignKey(p => p.IdUser)
        .OnDelete(DeleteBehavior.Restrict);


        //Add default Admin
        Guid randomId = Guid.NewGuid();
        Guid randomId1 = Guid.NewGuid();
        string ROLE_ID = randomId.ToString();
        string ROLE_IDUser = randomId1.ToString();
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = "admin",
            NormalizedName = "admin"
        },
        new IdentityRole
        {
            Id = ROLE_IDUser,
            Name = "user",
            NormalizedName = "user"
        }



        );
        var hasher = new PasswordHasher<IdentityUser>();

        IdentityUser AdminUser = new IdentityUser
        {

            UserName = "admin",
            NormalizedUserName = "admin",
            Email = "admin@gmail.com",
            NormalizedEmail = "admin@gmail.com",
            EmailConfirmed = false,
            PasswordHash = hasher.HashPassword(null, "Admin123#"),
            SecurityStamp = string.Empty
        };



        modelBuilder.Entity<IdentityUser>().HasData(AdminUser);


        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
       new IdentityUserRole<string>
       {
           UserId = AdminUser.Id,
           RoleId = ROLE_ID
       }
   );
        base.OnModelCreating(modelBuilder);
    }



}

