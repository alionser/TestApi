using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data.Entities;

namespace TestAPI.Web.Data;

public sealed class DataContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(x => x.Name)
                .IsRequired();

            entity.Property(x => x.Surname)
                .IsRequired();

            entity.Property(x => x.Salary)
                .HasPrecision(8, 2);

            entity.Property(x => x.Position)
                .HasDefaultValue(Position.None);

            entity.HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(x => x.Name)
                .IsRequired();
        });
    }
}