using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddAssetType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "org_assets",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "org_assets");
        }
    }
}
