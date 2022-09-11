USE ERPforAll
GO

-- Create a new table called 'Warehouses'
-- Drop the table if it already exists
IF OBJECT_ID('Warehouses', 'U') IS NOT NULL
DROP TABLE Warehouses
GO
-- Create the table in the specified schema
CREATE TABLE Warehouses
(
    WarehouseId INT NOT NULL PRIMARY KEY, -- primary key column
    Name [NVARCHAR](50) NOT NULL,
    Location [NVARCHAR](50) NOT NULL,
    MaxRoom INT NOT NULL
);
GO

-- Create a new table called 'Stocks'
-- Drop the table if it already exists
IF OBJECT_ID('Stocks', 'U') IS NOT NULL
DROP TABLE Stocks
GO
-- Create the table in the specified schema
CREATE TABLE Stocks
(
    StockId INT NOT NULL PRIMARY KEY, -- primary key column
    Amount INT NOT NULL,
    WarehouseId INT NOT NULL FOREIGN KEY REFERENCES Warehouse(Id),
    ItemId INT NOT NULL FOREIGN KEY REFERENCES Item(Id)
);
GO

CREATE TRIGGER Update_Stock ON Sells
  FOR INSERT
AS
BEGIN
    DECLARE @amount INT, @itemId INT, @stock_amount INT, @stockId INT
    SELECT @amount = Amount, @itemId = ItemId FROM INSERTED

    SELECT TOP 1 @stockId = StockId, @stock_amount = Amount FROM Stocks
        WHERE ItemId = @itemId AND (Amount > @amount OR Amount = @amount)

    IF (@stockId IS NULL OR @stockId = 0)
    BEGIN
        RAISERROR('Item is not in stock', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END

    UPDATE STOCKS 
    SET Amount = @stock_amount - @amount
    WHERE StockId = @stockid

    RETURN
END
GO