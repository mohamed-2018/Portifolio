using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
   public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        // Now Create override to edit on Model created and let Id in all tables is new not repeatable 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortifolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    Id = Guid.NewGuid(),
                    Avatar ="avatar.jpg",
                    FullName = "Mohamed Abo El-Magd",
                    Profil ="FullStack .Net Developer",

                }

                ) ; 

        }
        // now create tables of classes
        public DbSet<Owner> Owner { get; set; }
        public DbSet<PortifolioItem> portifolioItems { get; set; }



    }
}
