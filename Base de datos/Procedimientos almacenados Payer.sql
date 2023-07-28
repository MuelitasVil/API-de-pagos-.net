
-- Insertar Payer a la base de datos : 

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
				
