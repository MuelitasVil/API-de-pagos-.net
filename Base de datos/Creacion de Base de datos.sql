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
