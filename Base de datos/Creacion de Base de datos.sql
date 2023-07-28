-- Verificar si la base de datos existe
IF DB_ID('PaymentDatabase') IS NOT NULL
BEGIN
    -- Si la base de datos existe, eliminarla
    USE master;
    ALTER DATABASE PaymentDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PaymentDatabase;
END

-- Crear la base de datos
CREATE DATABASE PaymentDatabase;
use PaymentDatabase;

-- Revisar migraciones a la base de datos :
-- SELECT * from dbo.__EFMigrationsHistory;

-- Revisar procedimientos almacenados de la base de datos :
-- SELECT name FROM sys.procedures;