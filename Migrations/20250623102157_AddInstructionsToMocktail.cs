using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetalPeel.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructionsToMocktail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Mocktails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Mocktails",
                keyColumn: "Id",
                keyValue: 1,
                column: "Instructions",
                value: "1. Mix lime juice, simple syrup, and ginger ale.\n2. Shake or stir.\n3. Add mint leaves.");

            migrationBuilder.UpdateData(
                table: "Mocktails",
                keyColumn: "Id",
                keyValue: 2,
                column: "Instructions",
                value: "2. Mix raspberry puree with lime juice, then add to Sprite.\n2. Crush two mint leaves into cup.\n3. Add liquid and remaining leaves to cup.");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Mocktails");
        }
    }
}
