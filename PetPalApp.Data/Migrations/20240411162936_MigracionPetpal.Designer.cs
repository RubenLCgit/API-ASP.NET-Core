﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetPalApp.Data;

#nullable disable

namespace PetPalApp.Data.Migrations
{
    [DbContext(typeof(PetPalAppContext))]
    [Migration("20240411162936_MigracionPetpal")]
    partial class MigracionPetpal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PetPalApp.Domain.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<DateTime>("ProductAvailability")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductOnline")
                        .HasColumnType("bit");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("ProductRating")
                        .HasColumnType("float");

                    b.Property<int>("ProductStock")
                        .HasColumnType("int");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            ProductAvailability = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductDescription = "Dog food for dogs",
                            ProductName = "Dog food",
                            ProductOnline = true,
                            ProductPrice = 10.0m,
                            ProductRating = 4.5,
                            ProductStock = 10,
                            ProductType = "Food",
                            UserId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            ProductAvailability = new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductDescription = "Interactive cat toy",
                            ProductName = "Cat toy",
                            ProductOnline = true,
                            ProductPrice = 15.0m,
                            ProductRating = 4.7999999999999998,
                            ProductStock = 20,
                            ProductType = "Toy",
                            UserId = 2
                        },
                        new
                        {
                            ProductId = 3,
                            ProductAvailability = new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductDescription = "Durable dog leash",
                            ProductName = "Leash",
                            ProductOnline = true,
                            ProductPrice = 20.0m,
                            ProductRating = 4.7000000000000002,
                            ProductStock = 30,
                            ProductType = "Accessory",
                            UserId = 3
                        },
                        new
                        {
                            ProductId = 4,
                            ProductAvailability = new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductDescription = "Nutritional food for parrots",
                            ProductName = "Parrot food",
                            ProductOnline = true,
                            ProductPrice = 25.0m,
                            ProductRating = 4.9000000000000004,
                            ProductStock = 40,
                            ProductType = "Food",
                            UserId = 4
                        },
                        new
                        {
                            ProductId = 5,
                            ProductAvailability = new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductDescription = "Organic pet shampoo",
                            ProductName = "Shampoo",
                            ProductOnline = true,
                            ProductPrice = 30.0m,
                            ProductRating = 4.5999999999999996,
                            ProductStock = 50,
                            ProductType = "Grooming",
                            UserId = 5
                        });
                });

            modelBuilder.Entity("PetPalApp.Domain.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<DateTime>("ServiceAvailability")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ServiceOnline")
                        .HasColumnType("bit");

                    b.Property<decimal>("ServicePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("ServiceRating")
                        .HasColumnType("float");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            ServiceId = 1,
                            ServiceAvailability = new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "Basic pet grooming service",
                            ServiceName = "Basic Grooming",
                            ServiceOnline = true,
                            ServicePrice = 50.0m,
                            ServiceRating = 4.5,
                            ServiceType = "Grooming",
                            UserId = 1
                        },
                        new
                        {
                            ServiceId = 2,
                            ServiceAvailability = new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "Basic obedience training for dogs",
                            ServiceName = "Obedience Training",
                            ServiceOnline = true,
                            ServicePrice = 200.0m,
                            ServiceRating = 4.7999999999999998,
                            ServiceType = "Training",
                            UserId = 2
                        },
                        new
                        {
                            ServiceId = 3,
                            ServiceAvailability = new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "Pet sitting for all kinds of pets",
                            ServiceName = "Pet Sitting",
                            ServiceOnline = true,
                            ServicePrice = 30.0m,
                            ServiceRating = 4.7000000000000002,
                            ServiceType = "Sitting",
                            UserId = 3
                        },
                        new
                        {
                            ServiceId = 4,
                            ServiceAvailability = new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "Daily dog walking service",
                            ServiceName = "Dog Walking",
                            ServiceOnline = true,
                            ServicePrice = 15.0m,
                            ServiceRating = 4.9000000000000004,
                            ServiceType = "Walking",
                            UserId = 4
                        });
                });

            modelBuilder.Entity("PetPalApp.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UserRating")
                        .HasColumnType("float");

                    b.Property<DateTime>("UserRegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserSupplier")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            UserEmail = "ruben@gmail.com.com",
                            UserName = "Ruben",
                            UserPassword = "patatas1",
                            UserRating = 0.0,
                            UserRegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserRole = "Admin",
                            UserSupplier = true
                        },
                        new
                        {
                            UserId = 2,
                            UserEmail = "xio@gmail.com",
                            UserName = "Xio",
                            UserPassword = "patatas2",
                            UserRating = 0.0,
                            UserRegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserRole = "Client",
                            UserSupplier = false
                        },
                        new
                        {
                            UserId = 3,
                            UserEmail = "carlota@gmail.com",
                            UserName = "Carlota",
                            UserPassword = "patatas3",
                            UserRating = 0.0,
                            UserRegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserRole = "Client",
                            UserSupplier = false
                        },
                        new
                        {
                            UserId = 4,
                            UserEmail = "alberto@gmail.com",
                            UserName = "Alberto",
                            UserPassword = "patatas4",
                            UserRating = 0.0,
                            UserRegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserRole = "Client",
                            UserSupplier = true
                        },
                        new
                        {
                            UserId = 5,
                            UserEmail = "alejandro@gmail.com",
                            UserName = "Alejandro",
                            UserPassword = "patatas5",
                            UserRating = 0.0,
                            UserRegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserRole = "Client",
                            UserSupplier = true
                        });
                });

            modelBuilder.Entity("PetPalApp.Domain.Product", b =>
                {
                    b.HasOne("PetPalApp.Domain.User", null)
                        .WithMany("ListProducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PetPalApp.Domain.Service", b =>
                {
                    b.HasOne("PetPalApp.Domain.User", null)
                        .WithMany("ListServices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PetPalApp.Domain.User", b =>
                {
                    b.Navigation("ListProducts");

                    b.Navigation("ListServices");
                });
#pragma warning restore 612, 618
        }
    }
}
