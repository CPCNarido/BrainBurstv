using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddReviewTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Reviews",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"), // Use SQL Server identity annotation
                UserName = table.Column<string>(nullable: false),
                UserRole = table.Column<string>(nullable: false),
                Rating = table.Column<int>(nullable: false),
                Feedback = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reviews", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Reviews");
    }
}