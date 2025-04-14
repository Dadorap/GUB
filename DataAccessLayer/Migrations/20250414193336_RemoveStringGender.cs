using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStringGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the check constraint first
            migrationBuilder.Sql(@"
        IF EXISTS (
            SELECT * FROM sys.check_constraints 
            WHERE name = 'CK_Customers'
        )
        ALTER TABLE Customers DROP CONSTRAINT CK_Customers
    ");

            // Now drop the Gender column
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
