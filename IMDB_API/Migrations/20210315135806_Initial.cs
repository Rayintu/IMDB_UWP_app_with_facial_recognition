using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoviePoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieSynopsis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePublished = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
