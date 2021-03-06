﻿// <auto-generated />
using System;
using Krustan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Krustan.Migrations
{
    [DbContext(typeof(KrustanDbContext))]
    partial class KrustanDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Krustan.Models.AppUser", b =>
                {
                    b.Property<int>("AppUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Nickname");

                    b.Property<string>("ProfileImage");

                    b.Property<string>("UniqueId")
                        .IsRequired();

                    b.HasKey("AppUserId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Krustan.Models.Dog", b =>
                {
                    b.Property<int>("DogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<int>("AppUserId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("DogPicture")
                        .IsRequired();

                    b.Property<float>("Height");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.Property<string>("Sex")
                        .IsRequired();

                    b.Property<float>("Weight");

                    b.HasKey("DogId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Dogs");
                });

            modelBuilder.Entity("Krustan.Models.Dog", b =>
                {
                    b.HasOne("Krustan.Models.AppUser", "AppUser")
                        .WithMany("Dogs")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
