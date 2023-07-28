-- Insercion de procedimiento para ingresar una cuenta asociada a un usuario

use PaymentDatabase;

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
		END

-- Prueba de procedimiento almacenado 
-- Select * from Payers;

Select * from Counts;
EXEC InsertCount
	 @currency = 0,
     @Total = 100,
     @allowPartial = 0,
     @suscribe = 0,
     @PayerId = 1,
     @Resultado = 0 

-- use PaymentDatabase;