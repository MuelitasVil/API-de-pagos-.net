

Use PaymentDatabase;

DROP PROCEDURE IF EXISTS InsertPayment;
DROP PROCEDURE IF EXISTS getPayments;
-- Tener en cuenta que este

GO
CREATE PROCEDURE dbo.InsertPayment
		
		-- Insetart el monto : 
        @fromTotal bigint, 
		@fromCurrency int,
		@countId bigint, 
		@factor int,

		-- Variables de pago : 
		@description nvarchar(max),

		-- variables del recibo : 
		@franchise int,
		@reference nvarchar(max),
		@issuerName int, 
		@authorization bigint, 
		@paymentMethod int,
		@payerId bigInt,

		-- Definir el OUTPUT del procedimiento almacenado 
		@Resultado INT OUTPUT

		-- variables del Request : 
        AS

		Declare @toTotal bigint;
        Declare @toCurrency int;
		Declare @ExistCount int;
		Declare @mountId bigint;
		Declare @statusId bigint;

		-- Varibales para el status.
		Declare @allowPartial bit;  -- Por el momento solo se puede rechazar por esta razon

		-- Variables para count.
		Declare @SumOfPayments bigint;
		Declare	@fieldsId bigint;
		Declare @paymentId bigint;

        BEGIN 
			-- Insercion y creacion del monto a pagar : 

			SELECT @ExistCount = COUNT(*), @toTotal = MAX(Total), @toCurrency = MAX(currency)
			FROM Counts 
			WHERE countId = @countId;

			IF @ExistCount <= 0 
			BEGIN 
				PRINT 'No existe la cuenta ...';
				set @Resultado = 1;
				Select @Resultado;
				RETURN @Resultado;
			END

			-- Creacion de status 

			select @allowPartial = allowPartial from Counts where countId = @countId;

			IF @allowPartial = 0 AND @toTotal != (@fromTotal * @factor)
			BEGIN
				INSERT INTO statuses([status], reason, [message], [Date])
				VALUES(1, 'The payment have to be complete', 'try again', GETDATE());
			END

			INSERT INTO Mounts(toTotal, toCurrency, fromTotal, fromCurrency, countId, factor)
            VALUES(@toTotal, @toCurrency, @fromTotal, @fromCurrency, @countId, @factor);

			
			INSERT INTO statuses([status], reason, [message], [Date])
				VALUES(0, 'Correct payment', 'thanks', GETDATE());

			-- Modificar count si ya esta pago :

			SELECT @SumOfPayments = sum(fromTotal * factor) from Mounts where MountId = @mountId; 

			IF @SumOfPayments >= @fromTotal
			BEGIN
				UPDATE Counts
				SET paid = 1
				FROM Counts
				WHERE countId = @countId; 
			END
			
			-- Creacion del payment
			select @mountId = MAX(MountId)
			from Mounts;

			select @statusId = MAX(statusId)
			from statuses;
			
			INSERT INTO Payments([description], mountId, statusId, countId)
				VALUES(@description, @mountId, @statusId, @countId);

			select @paymentId = MAX(paymentId)
			from Payments;

			-- Creacion de los campos
			INSERT INTO ListOfFields DEFAULT VALUES;

			select @fieldsId = MAX(FieldsId)
			from ListOfFields;


			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('merchantCode','890900841',0,@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('terminalNumber','6065',0,@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('trazabilyCode','51959527',0,@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('transactionCycle','1',0,@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('id','1c5be20e0fb0kjas78f7f49fffa790f1',0,@fieldsId);

  			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('000','b24',0,@fieldsId);           
                
  			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('paymentURL','https://reg.paytest.com.co/index.html?id=PRUoQxINr%2bt9PZmhEZq5iEW8guznEFj5Co1QiJgmOdg%3d',0,@fieldsId);      
			

			INSERT INTO Receipts(franchise, reference, issuerName, [authorization], paymentMethod, payerId, fieldsId, paymentId)
			Values(@franchise, @reference, @issuerName, @authorization, @paymentMethod, @payerId, @fieldsId, @paymentId);
		END
GO

GO 
CREATE PROCEDURE dbo.getPayments
	AS
	BEGIN
		SELECT * 
		FROM Payments
	END
GO

/*

Declare @Resultado INT;

EXEC InsertPayment
        @fromTotal = 50, 
		@fromCurrency = 1,
		@countId = 2, 
		@factor = 1, 
		@description = 'Pago realizado ...',
		@franchise = 0,
		@reference = '123456',
		@issuerName = 0, 
		@authorization = 12345, 
		@paymentMethod = 0,
		@payerId = 1,
		@Resultado = 0 OUTPUT;

SELECT @Resultado;

*/

/*
Select * from Counts;
Select * from statuses;
select * from Mounts; 
select * from Fields;
select * from ListOfFields;
select * from Payments;
select * from Payers;

Use PaymentDatabase;

Select * 
from Payers INNER JOIN Counts 
ON Counts.payerId = Payers.PayerId;
*/


