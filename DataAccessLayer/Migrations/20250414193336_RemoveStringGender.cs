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
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * FROM sys.check_constraints 
                    WHERE name = 'CK_Customers'
                )
                BEGIN
                    ALTER TABLE Customers DROP CONSTRAINT CK_Customers
                END
            ");

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

