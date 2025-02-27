﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "devtips_multiple_migrations");

            migrationBuilder.CreateTable(
                name: "authors",
                schema: "devtips_multiple_migrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "devtips_multiple_migrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                schema: "devtips_multiple_migrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                    table.ForeignKey(
                        name: "fk_books_authors_author_id",
                        column: x => x.author_id,
                        principalSchema: "devtips_multiple_migrations",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_authors_name",
                schema: "devtips_multiple_migrations",
                table: "authors",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_books_author_id",
                schema: "devtips_multiple_migrations",
                table: "books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_title",
                schema: "devtips_multiple_migrations",
                table: "books",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "devtips_multiple_migrations",
                table: "users",
                column: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books",
                schema: "devtips_multiple_migrations");

            migrationBuilder.DropTable(
                name: "users",
                schema: "devtips_multiple_migrations");

            migrationBuilder.DropTable(
                name: "authors",
                schema: "devtips_multiple_migrations");
        }
    }
}
