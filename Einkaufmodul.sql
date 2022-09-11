USE ERPforAll
GO

-- Create a new table called 'Purchases'
-- Drop the table if it already exists
IF OBJECT_ID('Purchases', 'U') IS NOT NULL
DROP TABLE Purchases
GO
-- Create the table in the specified schema
CREATE TABLE Purchases
(
    [Id] INT NOT NULL PRIMARY KEY, -- Primary Key column
    [Amount] INT NOT NULL,
    [Date] DATETIME NOT NULL,
    [Price] FLOAT NOT NULL,
    [FullfilledDate] DATETIME,
    [TradeId] UNIQUEIDENTIFIER NOT NULL,
    [ItemId] INT NOT NULL FOREIGN KEY REFERENCES Items(Id),
    [VendorId] INT NOT NULL FOREIGN KEY REFERENCES Vendors(Id),
);
GO