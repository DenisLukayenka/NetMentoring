CREATE PROCEDURE [dbo].[2_3_2_SelectCustomers]
AS
	SELECT c.CustomerID, c.ContactName, COUNT(o.OrderID) as 'Orders count'
	FROM Customers as c

	LEFT JOIN Orders as o ON o.CustomerID = c.CustomerID

	GROUP BY c.CustomerID, c.ContactName
	ORDER BY COUNT(o.OrderID)
