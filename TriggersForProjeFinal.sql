CREATE TRIGGER TriggerForMemories
ON Memories
AFTER INSERT,DELETE
AS
	BEGIN
  

		INSERT INTO Products(ProductID, ProductType)
		SELECT ID, 'Memory'
		FROM INSERTED;

		DELETE P FROM Products P INNER JOIN DELETED D ON P.ProductID = D.ID WHERE P.ProductType = 'Memory'
	END

CREATE TRIGGER TriggerForGraphicsCards
ON GraphicsCards
AFTER INSERT,DELETE
AS
	BEGIN	
		INSERT INTO Products(ProductID, ProductType)
		SELECT ID, 'GraphicsCard'
		FROM INSERTED;

		DELETE P FROM Products P INNER JOIN DELETED D ON P.ProductID = D.ID WHERE P.ProductType = 'GraphicsCard'
	END

CREATE TRIGGER TriggerForMotherboards
ON Motherboards
AFTER INSERT,DELETE
AS
	BEGIN	
		INSERT INTO Products(ProductID, ProductType)
		SELECT ID, 'Motherboard'
		FROM INSERTED;

		DELETE P FROM Products P INNER JOIN DELETED D ON P.ProductID = D.ID WHERE P.ProductType = 'Motherboard'
	END


CREATE TRIGGER TriggerForProcessor
ON Processors
AFTER INSERT,DELETE
AS
	BEGIN	
		INSERT INTO Products(ProductID, ProductType)
		SELECT ID, 'Processor'
		FROM INSERTED;

		DELETE P FROM Products P INNER JOIN DELETED D ON P.ProductID = D.ID WHERE P.ProductType = 'Processor'
	END

CREATE TRIGGER TriggerForStorage
ON Storages
AFTER INSERT,DELETE
AS
	BEGIN	
		INSERT INTO Products(ProductID, ProductType)
		SELECT ID, 'Storage'
		FROM INSERTED;

		DELETE P FROM Products P INNER JOIN DELETED D ON P.ProductID = D.ID WHERE P.ProductType = 'Storage'
	END