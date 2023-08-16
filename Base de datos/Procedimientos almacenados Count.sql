use PaymentDatabase;

DROP PROCEDURE IF EXISTS InsertCount;
DROP PROCEDURE IF EXISTS GetCountsByPayers;
DROP PROCEDURE IF EXISTS EditPayerById;

GO
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
GO

Go
  CREATE PROCEDURE dbo.GetCountsByPayers
	 @payerId bigInt
	 AS 
	 BEGIN 
		SELECT * 
		FROM Counts
		WHERE payerId = @payerId;
	END
GO

GO 
  CREATE PROCEDURE dbo.EditPayerById 
	@payerId Bigint,
	@email varchar(max),
	@number varchar(max),
	@name varchar(max),
	@response int OUTPUT
	AS 
	BEGIN
		
		Print @payerId;
		Select * from Payers Where PayerId = @payerId;

		IF NOT EXISTS (SELECT * FROM Payers WHERE PayerId = @payerId)
		BEGIN
		PRINT 'USUARIO NO EXISTE';
		SET @response = 1;
		RETURN;
		END

		PRINT 'El usuario ha cambiado correctamente sus datos';
		SET @response = 0;
		Select @response, @payerId;
		RETURN;

		-- UPDATE Payers
		-- SET email = @email, number = @number, [name] = @name
		-- Where PayerId = @payerId 
	END 
GO

Declare @A INT = 2; 
Exec dbo.EditPayerById @payerId = 1, @email = 'newCorreo', @number = 'newNumber', @name = 'newName', @response = 0 OUTPUT;
SELECT @response as Respuesta;

SELECT * FROM Payers;
-- Exec dbo.GetCountsByPayers @payerId = 1;

(Select * FROM PAYERS WHERE EXISTS (SELECT * FROM Payers WHERE PayerId = 2));