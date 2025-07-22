using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemoApi.Models
{

    public partial class WebLineIndiaBackup15nov2024Context : DbContext
    {
        public WebLineIndiaBackup15nov2024Context()
        {
        }

        public WebLineIndiaBackup15nov2024Context(DbContextOptions<WebLineIndiaBackup15nov2024Context> options)
            : base(options)
        {
        }

        public virtual DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Name=DefaultConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_User");

                entity.ToTable("UserLogin");

                entity.Property(e => e.CounselorCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.NumberOfEcontracts)
                    .HasDefaultValue(25)
                    .HasColumnName("NumberOfEContracts");
                entity.Property(e => e.Password).IsUnicode(false);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.UserKeyHash).IsUnicode(false);
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}