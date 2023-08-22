
Use PaymentDatabase;

DROP PROCEDURE IF EXISTS InsertPayer;
DROP PROCEDURE IF EXISTS GetPayerById;
DROP PROCEDURE IF EXISTS GetPayers;
DROP PROCEDURE IF EXISTS EditPayerById;

-- Insertar Payer a la base de datos : 

GO
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

                END
GO		

GO
	CREATE PROCEDURE dbo.GetPayerById
		@Id Bigint
		AS
		BEGIN 
			Select * 
			FROM Payers 
			WHERE PayerId = @Id;
		END
GO

GO
	CREATE PROCEDURE dbo.GetPayers
		AS
		BEGIN 
			Select * 
			FROM Payers 
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

	END 
GO

-- SELECT * FROM Payers;

/*
Declare @a int;
EXEC InsertPayer
     @documentType = 0,
	 @name = 'nANNUEKAS',
     @email= 'newCorreo@gmailNEW12new.cSom',
	 @number= '12312NEW23Wnew',
     @password = 'asds',
     @salt = 'asd',
     @result = @a OUT;
 SELECT @a as Respuesta;
*/

/*
 Declare @a int;
 Exec dbo.EditPayerById @payerId = 12, @email = 'newCorreo', @number = 'newNumber', @name = 'newName', @response = @a OUT;
 SELECT @a as Respuesta;
*/


-- exec dbo.GetPayerById
--	@Id = 1;
