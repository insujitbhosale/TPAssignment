using DataEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ApplicationContext(DbContextOptions options)
             : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureDepartment(modelBuilder);
            ConfigureEmployee(modelBuilder);
        }

        private void ConfigureEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Employee");
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.DeptID).IsRequired();
            });

            modelBuilder.Entity<Employee>()
                .HasOne<Department>(e => e.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(p => p.DeptID).OnDelete(DeleteBehavior.Restrict);

        }

        private void ConfigureDepartment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptID);
                entity.ToTable("Department");
                entity.Property(e => e.DeptName).IsRequired();
            });

            modelBuilder.Entity<Department>()
              .HasMany<Employee>(e => e.Employees)
              .WithOne().OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
