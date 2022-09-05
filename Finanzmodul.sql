USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'ERPforAll'
)
CREATE DATABASE ERPforAll
GO

-- Tables

USE ERPforAll
GO
-- Create a new table called '[Vendors]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Vendors]', 'U') IS NOT NULL
DROP TABLE [dbo].[Vendors]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Vendors]
(
    [Id] INT NOT NULL PRIMARY KEY, -- Primary Key column
    [Name] NVARCHAR(50) NOT NULL,
);
GO

-- Create a new table called '[Customers]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Customers]', 'U') IS NOT NULL
DROP TABLE [dbo].[Customers]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Customers]
(
    [Id] INT NOT NULL PRIMARY KEY, -- Primary Key column
    [Name] NVARCHAR(50) NOT NULL,
);
GO


-- Create a new table called '[Items]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Items]', 'U') IS NOT NULL
DROP TABLE [dbo].[Items]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Items]
(
    [Id] INT NOT NULL PRIMARY KEY, -- Primary Key column
    [Name] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(500),
    [Price] FLOAT NOT NULL
);
GO

-- Create a new table called '[Sells]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Sells]', 'U') IS NOT NULL
DROP TABLE [dbo].[Sells]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Sells]
(
    [Id] INT NOT NULL PRIMARY KEY, -- Primary Key column
    [Amount] INT NOT NULL,
    [Date] DATETIME NOT NULL,
    [Price] FLOAT NOT NULL,
    [FullfilledDate] DATETIME,
    [TradeId] UNIQUEIDENTIFIER NOT NULL,
    [ItemId] INT NOT NULL FOREIGN KEY REFERENCES Items(Id),
    [CustomerId] INT NOT NULL FOREIGN KEY REFERENCES Customers(Id),
);
GO




