using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PaymentTransactions.Data.Migrations
{
    public partial class ProceduresPayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE dbo.InsertPayer
                @documentType int,
                @name varchar(max) OUTPUT,
                @email varchar(max),
                @number varchar(max)
                AS
                BEGIN 
                
                INSERT INTO Payers(documentType, name, email, number)
                VALUES(@documentType, @name, @email, @number)
                
                SELECT @name = SCOPE_IDENTITY()
                END
                ");
        }
            

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.InsertPayer");
            
        }
    }
}
