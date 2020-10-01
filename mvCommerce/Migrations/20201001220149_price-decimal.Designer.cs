﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mvCommerce.Database;

namespace mvCommerce.Migrations
{
    [DbContext(typeof(mvCommerceContext))]
    [Migration("20201001220149_price-decimal")]
    partial class pricedecimal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("mvCommerce.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryFatherId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Slug")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryFatherId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("mvCommerce.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("CPF")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("Sex")
                        .IsRequired();

                    b.Property<string>("Situation");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("mvCommerce.Models.Collaborator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("mvCommerce.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Path");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("mvCommerce.Models.NewsletterEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("NewsletterEmails");
                });

            modelBuilder.Entity("mvCommerce.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(240);

                    b.Property<int>("Height");

                    b.Property<int>("Length");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<double>("Weight");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("mvCommerce.Models.Category", b =>
                {
                    b.HasOne("mvCommerce.Models.Category", "CategoryFather")
                        .WithMany()
                        .HasForeignKey("CategoryFatherId");
                });

            modelBuilder.Entity("mvCommerce.Models.Image", b =>
                {
                    b.HasOne("mvCommerce.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("mvCommerce.Models.Product", b =>
                {
                    b.HasOne("mvCommerce.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
