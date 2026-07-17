using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SyncTodoStatusEnumColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Items",
                type: "boolean",
                nullable: true);
        }
    }
}
