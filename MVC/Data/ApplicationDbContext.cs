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

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(c => c.People)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<City>()
                .HasOne(p => p.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Country>().HasData(new Country[] {
                new Country { Id = 1, Name = "Sweden" },
                new Country { Id = 2, Name = "Finland" },
                new Country { Id = 3, Name = "Danmark" },
                new Country { Id = 4, Name = "Germany" },
                new Country { Id = 5, Name = "France" },
                new Country { Id = 6, Name = "Spain" }
            });

            modelBuilder.Entity<City>().HasData(new City[] {
                new City { Id = 1, CountryId= 1, Name = "Stockholm" },
                new City { Id = 2, CountryId= 1, Name = "Göteborg" },
                new City { Id = 3, CountryId= 1, Name = "Malmö" },
                new City { Id = 4, CountryId= 2, Name = "Helsingfors" },
                new City { Id = 5, CountryId= 3, Name = "Köpenhamn" },
                new City { Id = 6, CountryId= 4, Name = "Berlin" },
                new City { Id = 7, CountryId= 5, Name = "Paris" },
                new City { Id = 8, CountryId= 6, Name = "Madrid"}
            });
                        
            modelBuilder.Entity<Person>().HasData(new Person[] {
                new Person { Id = 1, CityId = 1, PhoneNumber = "+46725180302", Name = "Johan Svensson" },
                new Person { Id = 2, CityId = 2, PhoneNumber = "+46737470353", Name = "Nils Kristiansson" },
                new Person { Id = 3, CityId = 3, PhoneNumber = "+46736395900", Name = "Christoffer Nilsson" },
                new Person { Id = 4, CityId = 4, PhoneNumber = "+46725180305", Name = "Pekka Heino" },
                new Person { Id = 5, CityId = 5, PhoneNumber = "+46733080322", Name = "Peter Rohde" },
                new Person { Id = 6, CityId = 6, PhoneNumber = "+46718180309", Name = "Lisa Braun" },
                new Person { Id = 7, CityId = 7, PhoneNumber = "+46739470303", Name = "Blanche Berthelot" },
                new Person { Id = 8, CityId = 8, PhoneNumber = "+46739165309", Name = "Diego Garcia" },
                new Person { Id = 9, CityId = 1, PhoneNumber = "+46739145209", Name = "Per Persson" }
            });
        }
    }
}