CREATE PROCEDURE [dbo].[2_4_3_SelectCustomers]
AS
	SELECT c.CustomerID, c.ContactName
	FROM Customers as c
		WHERE NOT EXISTS(
			SELECT o.OrderID
			FROM Orders as o
				WHERE o.CustomerID = c.CustomerID
		)
