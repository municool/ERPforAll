USE ERPforAll
GO

-- Create a new table called 'Warehouse'
-- Drop the table if it already exists
IF OBJECT_ID('Warehouse', 'U') IS NOT NULL
DROP TABLE Warehouse
GO
-- Create the table in the specified schema
CREATE TABLE Warehouse
(
    WarehouseId INT NOT NULL PRIMARY KEY, -- primary key column
    Name [NVARCHAR](50) NOT NULL,
    Location [NVARCHAR](50) NOT NULL,
    MaxRoom INT NOT NULL
);
GO

-- Create a new table called 'Stock'
-- Drop the table if it already exists
IF OBJECT_ID('Stock', 'U') IS NOT NULL
DROP TABLE Stock
GO
-- Create the table in the specified schema
CREATE TABLE Stock
(
    StockId INT NOT NULL PRIMARY KEY, -- primary key column
    Amount INT NOT NULL,
    WarehouseId INT NOT NULL FOREIGN KEY REFERENCES Warehouse(Id),
    ItemId INT NOT NULL FOREIGN KEY REFERENCES Item(Id)
);
GO