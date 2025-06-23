using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetalPeel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstructionsToMocktail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Mocktails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Mocktails",
                keyColumn: "Id",
                keyValue: 2,
                column: "Instructions",
                value: "1. Mix raspberry puree with lime juice, then add to Sprite.\n2. Crush two mint leaves into cup.\n3. Add liquid and remaining leaves to cup.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Mocktails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Mocktails",
                keyColumn: "Id",
                keyValue: 2,
                column: "Instructions",
                value: "2. Mix raspberry puree with lime juice, then add to Sprite.\n2. Crush two mint leaves into cup.\n3. Add liquid and remaining leaves to cup.");
        }
    }
}
