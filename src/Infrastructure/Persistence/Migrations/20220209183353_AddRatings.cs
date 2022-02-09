using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBet.Infrastructure.Persistence.Migrations
{
    public partial class AddRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Freelancers_ExecutorId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ExecutorId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialPrice",
                table: "Auction",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "ProjectRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientScore = table.Column<int>(type: "int", nullable: true),
                    FreelancerScore = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRatings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRatings_ProjectId",
                table: "ProjectRatings",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Freelancers_ExecutorId",
                table: "Projects",
                column: "ExecutorId",
                principalTable: "Freelancers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Freelancers_ExecutorId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectRatings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ExecutorId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialPrice",
                table: "Auction",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Freelancers_ExecutorId",
                table: "Projects",
                column: "ExecutorId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
