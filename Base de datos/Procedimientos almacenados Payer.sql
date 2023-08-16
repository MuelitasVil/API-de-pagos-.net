
Use PaymentDatabase;

DROP PROCEDURE IF EXISTS InsertPayer;
DROP PROCEDURE IF EXISTS GetPayerById;
DROP PROCEDURE IF EXISTS GetPayers;

-- Insertar Payer a la base de datos : 

GO
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

-- exec dbo.GetPayerById
--	@Id = 1;
