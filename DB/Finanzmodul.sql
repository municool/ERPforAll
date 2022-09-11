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

-- Create a new view called 'GetNotFullfilledSells' in schema 'dbo'
-- Drop the view if it already exists
IF EXISTS (
SELECT *
    FROM sys.views
    JOIN sys.schemas
    ON sys.views.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
    AND sys.views.name = N'GetNotFullfilledSells'
)
DROP VIEW dbo.GetNotFullfilledSells
GO
-- Create the view in the specified schema
CREATE VIEW dbo.GetNotFullfilledSells
AS
    -- body of the view
    SELECT s.Id AS Sell_Id,
        c.Name AS Customer_Name,
        i.Name AS Item,
        s.FullfilledDate AS Fullfilment_Date
    FROM dbo.Sells AS s
    JOIN dbo.Items AS i on s.ItemID = i.ItemID
    JOIN dbo.Customers AS c on s.CustomerID = c.CustomerID
    WHERE s.FullfilledDate IS NULL
        OR s.FullfilledDate > GETDATE()
GO

-- Create a new stored procedure called 'GetRevenueForItem' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GetRevenueForItem'
)
DROP PROCEDURE dbo.GetRevenueForItem
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.GetRevenueForItem
    @ItemId INT 
AS
    SELECT i.Name, SUM(s.Price) AS Revenue FROM Items AS i
        JOIN Sells AS s ON s.ItemId = i.ItemId
    ORDER BY i.Name
GO