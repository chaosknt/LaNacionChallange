using LaNacion.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LaNacion.Data
{
    public class LaNacionContext : DbContext
    {
        public LaNacionContext(DbContextOptions<LaNacionContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ContactDbEntity>()
                .Property(e => e.BirthDate)
                .HasConversion(
                    v => v.ToString("yyyy-MM-dd"),
                    v => DateTime.ParseExact(v, "yyyy-MM-dd", null));
        }

        public DbSet<ContactDbEntity> Contacts { get; set; }

        public DbSet<PhoneNumberDbEntity> PhoneNumbers { get; set; }
    }
}