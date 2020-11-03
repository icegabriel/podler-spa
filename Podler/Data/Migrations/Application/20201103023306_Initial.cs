using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podler.Data.Migrations.Application
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 40, nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 40, nullable: false),
                    Summary = table.Column<string>(maxLength: 1000, nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CoverPath = table.Column<string>(nullable: false),
                    GenreId = table.Column<int>(nullable: true),
                    ProfileId = table.Column<int>(nullable: true),
                    StaffId = table.Column<int>(nullable: true),
                    ThemeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mangas_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mangas_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mangas_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mangas_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chapers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 40, nullable: false),
                    Number = table.Column<decimal>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    MangaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapers_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MangaGenre",
                columns: table => new
                {
                    MangaId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaGenre", x => new { x.MangaId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MangaGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaGenre_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaStaff",
                columns: table => new
                {
                    MangaId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaStaff", x => new { x.MangaId, x.StaffId });
                    table.ForeignKey(
                        name: "FK_MangaStaff_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaStaff_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaTheme",
                columns: table => new
                {
                    MangaId = table.Column<int>(nullable: false),
                    ThemeId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaTheme", x => new { x.MangaId, x.ThemeId });
                    table.ForeignKey(
                        name: "FK_MangaTheme_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaTheme_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagePages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    ChapterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagePages_Chapers_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapers_MangaId",
                table: "Chapers",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePages_ChapterId",
                table: "ImagePages",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaGenre_GenreId",
                table: "MangaGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_GenreId",
                table: "Mangas",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_ProfileId",
                table: "Mangas",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_StaffId",
                table: "Mangas",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_ThemeId",
                table: "Mangas",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaStaff_StaffId",
                table: "MangaStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaTheme_ThemeId",
                table: "MangaTheme",
                column: "ThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagePages");

            migrationBuilder.DropTable(
                name: "MangaGenre");

            migrationBuilder.DropTable(
                name: "MangaStaff");

            migrationBuilder.DropTable(
                name: "MangaTheme");

            migrationBuilder.DropTable(
                name: "Chapers");

            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
