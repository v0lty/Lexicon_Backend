﻿// <auto-generated />
using MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVC.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 1,
                            Name = "Stockholm"
                        },
                        new
                        {
                            Id = 2,
                            CountryId = 1,
                            Name = "Göteborg"
                        },
                        new
                        {
                            Id = 3,
                            CountryId = 1,
                            Name = "Malmö"
                        },
                        new
                        {
                            Id = 4,
                            CountryId = 2,
                            Name = "Helsingfors"
                        },
                        new
                        {
                            Id = 5,
                            CountryId = 3,
                            Name = "Köpenhamn"
                        },
                        new
                        {
                            Id = 6,
                            CountryId = 4,
                            Name = "Berlin"
                        },
                        new
                        {
                            Id = 7,
                            CountryId = 5,
                            Name = "Paris"
                        },
                        new
                        {
                            Id = 8,
                            CountryId = 6,
                            Name = "Madrid"
                        });
                });

            modelBuilder.Entity("MVC.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sweden"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Finland"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Danmark"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Germany"
                        },
                        new
                        {
                            Id = 5,
                            Name = "France"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Spain"
                        });
                });

            modelBuilder.Entity("MVC.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Name = "Johan Svensson",
                            PhoneNumber = "+46725180302"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 2,
                            Name = "Nils Kristiansson",
                            PhoneNumber = "+46737470353"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 3,
                            Name = "Christoffer Nilsson",
                            PhoneNumber = "+46736395900"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 4,
                            Name = "Pekka Heino",
                            PhoneNumber = "+46725180305"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 5,
                            Name = "Peter Rohde",
                            PhoneNumber = "+46733080322"
                        },
                        new
                        {
                            Id = 6,
                            CityId = 6,
                            Name = "Lisa Braun",
                            PhoneNumber = "+46718180309"
                        },
                        new
                        {
                            Id = 7,
                            CityId = 7,
                            Name = "Blanche Berthelot",
                            PhoneNumber = "+46739470303"
                        },
                        new
                        {
                            Id = 8,
                            CityId = 8,
                            Name = "Diego Garcia",
                            PhoneNumber = "+46739165309"
                        },
                        new
                        {
                            Id = 9,
                            CityId = 1,
                            Name = "Per Persson",
                            PhoneNumber = "+46739145209"
                        });
                });

            modelBuilder.Entity("MVC.Models.City", b =>
                {
                    b.HasOne("MVC.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVC.Models.Person", b =>
                {
                    b.HasOne("MVC.Models.City", "City")
                        .WithMany("People")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
