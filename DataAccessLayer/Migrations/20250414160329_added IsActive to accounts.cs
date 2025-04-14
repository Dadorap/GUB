using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addedIsActivetoaccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'Accounts' AND COLUMN_NAME = 'IsActive'
                )
                BEGIN
                    ALTER TABLE Accounts ADD IsActive BIT NOT NULL DEFAULT 1
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'Accounts' AND COLUMN_NAME = 'IsActive'
                )
                BEGIN
                    ALTER TABLE Accounts DROP COLUMN IsActive
                END
            ");
        }
    }
}
