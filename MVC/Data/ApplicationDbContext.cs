using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(e => {
                e.HasKey(m => m.Id);
            });

            modelBuilder.Entity<Person>().HasData(new Person[] {
                new Person { Id = 1,  PhoneNumber = "+46725180302", City = "Stockholm", Name = "Johan Svensson" },
                new Person { Id = 2,  PhoneNumber = "+46737470353", City = "Göteborg", Name = "Nils Kristiansson" },
                new Person { Id = 3,  PhoneNumber = "+46736395900", City = "Malmö", Name = "Christoffer Nilsson" },
                new Person { Id = 4,  PhoneNumber = "+46725180305", City = "Helsingfors", Name = "Pekka Heino" },
                new Person { Id = 5,  PhoneNumber = "+46733080322", City = "Köpenhamn", Name = "Peter Rohde" },
                new Person { Id = 6,  PhoneNumber = "+46718180309", City = "Berlin", Name = "Lisa Braun" },
                new Person { Id = 7,  PhoneNumber = "+46739470303", City = "Paris", Name = "Blanche Berthelot" },
                new Person { Id = 8,  PhoneNumber = "+46739165309", City = "Madrid", Name = "Diego Garcia" },
                new Person { Id = 9,  PhoneNumber = "+46739145209", City = "Stockholm", Name = "Per Persson" }
            });
        }
    }
}