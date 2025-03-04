﻿// <auto-generated />
using System;
using Database.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Migrations.SqlServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("devtips_multiple_migrations")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.Core.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_authors");

                    b.HasIndex("Name")
                        .HasDatabaseName("ix_authors_name");

                    b.ToTable("authors", "devtips_multiple_migrations");
                });

            modelBuilder.Entity("Database.Core.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at_utc");

                    b.Property<int>("Year")
                        .HasColumnType("int")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_books_author_id");

                    b.HasIndex("Title")
                        .HasDatabaseName("ix_books_title");

                    b.ToTable("books", "devtips_multiple_migrations");
                });

            modelBuilder.Entity("Database.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", "devtips_multiple_migrations");
                });

            modelBuilder.Entity("Database.Core.Entities.Book", b =>
                {
                    b.HasOne("Database.Core.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_authors_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Database.Core.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
