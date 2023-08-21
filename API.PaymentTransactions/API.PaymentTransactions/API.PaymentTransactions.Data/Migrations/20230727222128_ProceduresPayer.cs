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

            migrationBuilder.Sql
                (
                @"
                CREATE PROCEDURE dbo.InsertPayer
                    @documentType int,
                    @name varchar(max),
                    @email varchar(max),
                    @number varchar(max),
				    @password varchar(max),
				    @salt varchar(max),
				    @result int OUTPUT
                    AS
                    BEGIN 
				    IF EXISTS (SELECT * FROM Payers WHERE email = @email) 
				    BEGIN 
					    PRINT 'El correo ingresado ya existe ... ';
					    set @result = 1;
					    RETURN 
				    END 

				    IF EXISTS (SELECT * FROM Payers WHERE number = @number) 
				    BEGIN 
					    PRINT 'El numero ingresado ya existe ... ';
					    set @result = 2;
					    RETURN;
				    END 

                    INSERT INTO Payers(documentType, name, email, number, password, salt)
                    VALUES(@documentType, @name, @email, @number, @password, @salt)
				
				    PRINT 'El usuario ingresado ha sido registrado ...';
				    SET @result = 0;
				    RETURN;                    
                    END "
                );

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
