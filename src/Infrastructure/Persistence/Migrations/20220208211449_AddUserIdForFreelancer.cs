using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBet.Infrastructure.Persistence.Migrations
{
    public partial class AddUserIdForFreelancer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Freelancers_FreelancerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "FreelancerId",
                table: "Projects",
                newName: "ExecutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_FreelancerId",
                table: "Projects",
                newName: "IX_Projects_ExecutorId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Freelancers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancers_UserId",
                table: "Freelancers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Freelancers_ExecutorId",
                table: "Projects",
                column: "ExecutorId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Freelancers_ExecutorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Freelancers_UserId",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Freelancers");

            migrationBuilder.RenameColumn(
                name: "ExecutorId",
                table: "Projects",
                newName: "FreelancerId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ExecutorId",
                table: "Projects",
                newName: "IX_Projects_FreelancerId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Projects",
                type: "NUMERIC(10,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Freelancers_FreelancerId",
                table: "Projects",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
