CREATE PROCEDURE dbo.InsertPayment
		
		-- Variables para crear el monto : 
		@toTotal bigint,
        @toCurrency int,
        @fromTotal bigint, 
		@fromCurrency int,
		@factor int,
		@countId bigint, 
		@ExistCount int OUTPUT,

		-- Varibales para el status.
		@allowPartial bit,  -- Por el momento solo se puede rechazar por esta razon

		-- Variables para count.
		@SumOfPayments bigint,

		-- Variables de pago : 
		@description nvarchar,
		@mountId bigint,
		@statusId bigint,

		-- variables del recibo : 
		@franchise int,
		@reference nvarchar,
		@issuerName int, 
		@authorization bigint, 
		@paymentMethod int,
		@payerId bigInt,
		@fieldsId bigint,
		@paymentId bigint
		-- variables del Request : 
        AS
        BEGIN 
			-- Insercion y creacion del monto a pagar : 

			SELECT @ExistCount = COUNT(*), @toTotal = MAX(Total), @toCurrency = MAX(currency)
			FROM Counts 
			WHERE countId = @countId;
			
			IF @ExistCount <= 0 
			BEGIN 
				SELECT @ExistCount = SCOPE_IDENTITY()
				RETURN;
			END

			INSERT INTO Mounts(toTotal, toCurrency, fromTotal, fromCurrency, countId)
            VALUES(@toTotal, @toCurrency, @fromTotal, @fromCurrency, @countId);

			-- Creacion de status 

			select @allowPartial = allowPartial from Counts where countId = @countId;

			IF @allowPartial = 0 AND @toTotal !=  @fromTotal * @factor
			BEGIN
				INSERT INTO statuses([status], reason, [message], [Date])
				VALUES(1, 'The payment have to be complete', 'try again', GETDATE());
			END

			INSERT INTO statuses([status], reason, [message], [Date])
				VALUES(0, 'Correct payment', 'thanks', GETDATE());


			-- Modificar count si ya esta pago :

			SELECT @SumOfPayments = sum(fromTotal * factor) from Mounts where MountId = @mountId; 

			IF @SumOfPayments >= @fromTotal
			BEGIN
				UPDATE Counts
				SET paid = 1

				WHERE countId = 1 
			END
			

			-- Creacion del payment
			select top 1 @mountId = (MountId)
			from Mounts

			select top 1 @statusId = (statusId)
			from statuses
			
			INSERT INTO Payments([description], mountId, statusId, countId)
				VALUES(@description, @mountId, @statusId, @countId);

			-- Creacion de los campos
			INSERT INTO ListOfFields DEFAULT VALUES;

			select top 1  @fieldsId = (FieldsId)
			from ListOfFields

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('merchantCode','890900841','none',@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('terminalNumber','6065','none',@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('trazabilyCode','51959527','none',@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('transactionCycle','1','none',@fieldsId);

			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('id','1c5be20e0fb0kjas78f7f49fffa790f1','none',@fieldsId);

  			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('000','b24','none',@fieldsId);           
                
  			INSERT INTO Fields(keyWord, [Value], displayOn, FieldsId) 
				VALUES('"paymentURL','https://reg.paytest.com.co/index.html?id=PRUoQxINr%2bt9PZmhEZq5iEW8guznEFj5Co1QiJgmOdg%3d','none',@fieldsId);          
			
			
			INSERT INTO Receipts(franchise, reference, issuerName, [authorization], paymentMethod, payerId, fieldsId, paymentId)
				Values(@franchise, @reference, @issuerName, @authorization, @paymentMethod, @payerId, @fieldsId, @paymentId);
		END


	CREATE PROCEDURE dbo.GetInsertedInformation
    @countId bigint
	AS
	BEGIN
    -- Tabla Counts
    SELECT *
    FROM Counts
    WHERE countId = @countId;

    -- Tabla Mounts
    SELECT *
    FROM Mounts
    WHERE countId = @countId;

    -- Tabla statuses
    SELECT *
    FROM statuses
    WHERE countId = @countId;

    -- Tabla Payments
    SELECT *
    FROM Payments
    WHERE countId = @countId;

    -- Tabla ListOfFields
    SELECT *
    FROM ListOfFields
    WHERE countId = @countId;

    -- Tabla Fields
    SELECT *
    FROM Fields
    WHERE countId = @countId;

    -- Tabla Receipts
    SELECT *
    FROM Receipts
    WHERE countId = @countId;
END

