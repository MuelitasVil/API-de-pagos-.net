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

            migrationBuilder.Sql(
                @"
	            CREATE PROCEDURE dbo.GetPayerById
		        @Id Bigint
		        AS
	    	        BEGIN 
			        Select * 
			        FROM Payers 
			        WHERE PayerId = @Id;
		        END
                ");

            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE dbo.GetPayers
                AS
                    BEGIN
                    Select*
                    FROM Payers
                END");

            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE dbo.EditPayerById 
	            @payerId Bigint,
	            @email varchar(max),
	            @number varchar(max),
	            @name varchar(max),
	            @response int OUTPUT
	            AS 
	                BEGIN
		                IF NOT EXISTS (SELECT * FROM Payers WHERE PayerId = @payerId)
		                BEGIN
		                PRINT 'USUARIO NO EXISTE';
		                SET @response = 1;
		                RETURN;
		                END

		                UPDATE Payers
		                SET email = @email, number = @number, [name] = @name
		                Where PayerId = @payerId 

		                PRINT 'El usuario ha cambiado correctamente sus datos';
		                SET @response = 0;
		                RETURN;

	            END ");

        }
            

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.InsertPayer");
            migrationBuilder.Sql("DROP PROCEDURE dbo.GetPayerById");
            migrationBuilder.Sql("DROP PROCEDURE dbo.GetPayers");
            migrationBuilder.Sql("DROP PROCEDURE dbo.dbo.EditPayerById");

        }
    }
}
