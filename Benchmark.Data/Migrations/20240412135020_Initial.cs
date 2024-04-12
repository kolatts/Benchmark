using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Benchmark.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BytePrimaryKeyEntities",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BytePrimaryKeyEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuidPrimaryKeyEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuidPrimaryKeyEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntPrimaryKeyEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntPrimaryKeyEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LongPrimaryKeyEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LongPrimaryKeyEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortPrimaryKeyEntities",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortPrimaryKeyEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StringPrimaryKeyEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringPrimaryKeyEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BytePrimaryKeyChildEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BytePrimaryKeyChildEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BytePrimaryKeyChildEntities_BytePrimaryKeyEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BytePrimaryKeyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuidPrimaryKeyChildEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuidPrimaryKeyChildEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuidPrimaryKeyChildEntities_GuidPrimaryKeyEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "GuidPrimaryKeyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntPrimaryKeyChildEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntPrimaryKeyChildEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntPrimaryKeyChildEntities_IntPrimaryKeyEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "IntPrimaryKeyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LongPrimaryKeyChildEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LongPrimaryKeyChildEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LongPrimaryKeyChildEntities_LongPrimaryKeyEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LongPrimaryKeyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortPrimaryKeyChildEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortPrimaryKeyChildEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortPrimaryKeyChildEntities_ShortPrimaryKeyEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ShortPrimaryKeyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StringPrimaryKeyChildEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringPrimaryKeyChildEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringPrimaryKeyChildEntities_StringPrimaryKeyEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "StringPrimaryKeyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BytePrimaryKeyChildEntities_ParentId",
                table: "BytePrimaryKeyChildEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GuidPrimaryKeyChildEntities_ParentId",
                table: "GuidPrimaryKeyChildEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_IntPrimaryKeyChildEntities_ParentId",
                table: "IntPrimaryKeyChildEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_LongPrimaryKeyChildEntities_ParentId",
                table: "LongPrimaryKeyChildEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShortPrimaryKeyChildEntities_ParentId",
                table: "ShortPrimaryKeyChildEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StringPrimaryKeyChildEntities_ParentId",
                table: "StringPrimaryKeyChildEntities",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BytePrimaryKeyChildEntities");

            migrationBuilder.DropTable(
                name: "GuidPrimaryKeyChildEntities");

            migrationBuilder.DropTable(
                name: "IntPrimaryKeyChildEntities");

            migrationBuilder.DropTable(
                name: "LongPrimaryKeyChildEntities");

            migrationBuilder.DropTable(
                name: "ShortPrimaryKeyChildEntities");

            migrationBuilder.DropTable(
                name: "StringPrimaryKeyChildEntities");

            migrationBuilder.DropTable(
                name: "BytePrimaryKeyEntities");

            migrationBuilder.DropTable(
                name: "GuidPrimaryKeyEntities");

            migrationBuilder.DropTable(
                name: "IntPrimaryKeyEntities");

            migrationBuilder.DropTable(
                name: "LongPrimaryKeyEntities");

            migrationBuilder.DropTable(
                name: "ShortPrimaryKeyEntities");

            migrationBuilder.DropTable(
                name: "StringPrimaryKeyEntities");
        }
    }
}
