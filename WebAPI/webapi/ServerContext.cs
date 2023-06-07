using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace WebAPI
{
    public class AppServerContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Right> Rights { get; set; } = null!;
        public DbSet<Data> Datas { get; set; } = null!;
        public AppServerContext(DbContextOptions<AppServerContext>
            options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(c => c.Rights)
                .WithMany(s => s.Users)
                .UsingEntity<UserRight>(
                    j => j
                        .HasOne(pt => pt.Right)
                        .WithMany(t => t.UserRights)
                        .HasForeignKey(pt => pt.RightId),
                    j => j
                        .HasOne(pt => pt.User)
                        .WithMany(p => p.UserRights)
                        .HasForeignKey(pt => pt.UserId),
                    j =>
                    {
                        j.HasKey(t => new { t.UserId, t.RightId });
                        j.ToTable("UserRight");
                    });
        }
    }
}