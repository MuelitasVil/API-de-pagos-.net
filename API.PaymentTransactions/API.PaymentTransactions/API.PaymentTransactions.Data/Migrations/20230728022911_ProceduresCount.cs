using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PaymentTransactions.Data.Migrations
{
    public partial class ProceduresCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)

        {
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE dbo.InsertCount
                @currency int, -- Ingresa la visa en la que se va a pagar
                @Total bigint, -- Cuanto es el total que se va a pagar
                @allowPartial bit, -- Si se va a pagar a una cuota
		        @suscribe bit, -- Es un pago mensual
		        @PayerId bigint, -- Id de la persona que esta generando la deuda
		        @Resultado int output -- Variable para contar ese id
                AS
                    BEGIN 
                        SELECT @Resultado = COUNT(*) FROM Payers
			            WHERE PayerId = @PayerId;
                    
                    IF @Resultado > 0
                        BEGIN
                            INSERT INTO Counts(currency, Total, allowPartial, suscribe, payerId, paid)
                            VALUES(@currency, @Total, @allowPartial, @suscribe, @payerId, 0);
                        END
		            SELECT @Resultado = SCOPE_IDENTITY()
		        END");

            migrationBuilder.Sql(
                @"  
                    CREATE PROCEDURE dbo.GetCountsByPayers
	                @payerId bigInt
	                AS 
	                BEGIN 
		                SELECT * 
		                FROM Counts
		                WHERE payerId = @payerId;
	                END");
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.insertcount");
            migrationBuilder.Sql("DROP PROCEDURE dbo.GetCountsByPayers");
        }
    }
}
